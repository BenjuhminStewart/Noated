using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace BenDraw.Algorithms
{
    internal class FloodFill
    {

        unsafe static Color GetPixel(BitmapData bmpD, int x, int y)
        {
            var intPtr = (byte*)bmpD.Scan0 + (y * bmpD.Stride) + (x * 4); // first ARGB
            return Color.FromArgb(intPtr[3], intPtr[2], intPtr[1], intPtr[0]);
        }

        unsafe static void SetPixel(BitmapData bmpD, int x, int y, Color color)
        {
            var intPtr = (byte*)bmpD.Scan0 + (y * bmpD.Stride) + (x * 4); // first ARGB
            intPtr[0] = color.B;
            intPtr[1] = color.G;
            intPtr[2] = color.R;
            intPtr[3] = color.A;
        }

        public void Fill(Bitmap bm, int x, int y, Color fill)
        {
            int n = bm.Width;
            int m = bm.Height;
            var visited = new bool[n, m];
            var bmpData = bm.LockBits(new Rectangle(0, 0, n, m), ImageLockMode.ReadWrite, bm.PixelFormat);
            Color replacedColor = GetPixel(bmpData, x, y);
            if (fill == replacedColor)
            {
                bm.UnlockBits(bmpData);
                return;
            }
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(x, y));

            while (queue.Count > 0)
            {
                Tuple<int, int> coord = queue.Dequeue();
                int px = coord.Item1;
                int py = coord.Item2;
                if (visited[px, py])
                {
                    continue;
                }

                SetPixel(bmpData, px, py, fill);
                visited[px, py] = true;

                for (int i = Math.Max(0, py - 1); i <= Math.Min(m - 1, py + 1); i++)
                {
                    for (int j = Math.Max(0, px - 1); j <= Math.Min(n - 1, px + 1); j++)
                    {
                        if (!visited[j, i] && GetPixel(bmpData, j, i) == replacedColor)
                        {
                            queue.Enqueue(new Tuple<int, int>(j, i));
                        }
                    }
                }
            }
            bm.UnlockBits(bmpData);
        }

        public void ReplaceAll(Bitmap bm, int x, int y, Color fill)
        {
            int n = bm.Width;
            int m = bm.Height;
            var rect = new Rectangle(0, 0, n, m);
            var bmpData = bm.LockBits(rect, ImageLockMode.ReadWrite, bm.PixelFormat);
            Color replacedColor = GetPixel(bmpData, x, y);
            Parallel.For(0, m, (int i) => {
                for (int j = 0; j < n; j++)
                {
                    if (GetPixel(bmpData, j, i) == replacedColor)
                    {
                        SetPixel(bmpData, j, i, fill);
                    }
                }
            });
            bm.UnlockBits(bmpData);
        }
        public static void MyFill(Bitmap bm, int x, int y, Color fill)
        {
            int n = bm.Width;
            int m = bm.Height;
            var bmpData = bm.LockBits(new Rectangle(0, 0, n, m), ImageLockMode.ReadWrite, bm.PixelFormat);
            Color replacedColor = GetPixel(bmpData, x, y);
            if (fill == replacedColor)
            {
                bm.UnlockBits(bmpData);
                return;
            }
            _MyFill(bmpData, fill, replacedColor, x, y, n, m);
            bm.UnlockBits(bmpData);
        }

        static void _MyFill(BitmapData bmpData, Color fill, Color match, int x, int y, int width, int height)
        {
            // at this point, we know array[y,x] is clear, and we want to move as far as possible to the upper-left. moving
            // up is much more important than moving left, so we could try to make this smarter by sometimes moving to
            // the right if doing so would allow us to move further up, but it doesn't seem worth the complexity
            while (true)
            {
                int ox = x, oy = y;
                while (y != 0 && GetPixel(bmpData, x, y - 1) == match) y--;
                while (x != 0 && GetPixel(bmpData, x - 1, y) == match) x--;
                if (x == ox && y == oy) break;
            }
            MyFillCore(bmpData, fill, match, x, y, width, height);
        }

        static void MyFillCore(BitmapData bmpData, Color fill, Color match, int x, int y, int width, int height)
        {
            // at this point, we know that array[y,x] is clear, and array[y-1,x] and array[y,x-1] are set.
            // we'll begin scanning down and to the right, attempting to fill an entire rectangular block
            int lastRowLength = 0; // the number of cells that were clear in the last row we scanned
            do
            {
                int rowLength = 0, sx = x; // keep track of how long this row is. sx is the starting x for the main scan below
                                           // now we want to handle a case like |***|, where we fill 3 cells in the first row and then after we move to
                                           // the second row we find the first  | **| cell is filled, ending our rectangular scan. rather than handling
                                           // this via the recursion below, we'll increase the starting value of 'x' and reduce the last row length to
                                           // match. then we'll continue trying to set the narrower rectangular block
                if (lastRowLength != 0 && GetPixel(bmpData, x, y) == fill) // if this is not the first row and the leftmost cell is filled...
                {
                    do
                    {
                        if (--lastRowLength == 0) return; // shorten the row. if it's full, we're done
                    } while (GetPixel(bmpData, ++x, y) == fill); // otherwise, update the starting point of the main scan to match
                    sx = x;
                }
                // we also want to handle the opposite case, | **|, where we begin scanning a 2-wide rectangular block and
                // then find on the next row that it has     |***| gotten wider on the left. again, we could handle this
                // with recursion but we'd prefer to adjust x and lastRowLength instead
                else
                {
                    for (; x != 0 && GetPixel(bmpData, x - 1, y) == match; rowLength++, lastRowLength++)
                    {
                        SetPixel(bmpData, --x, y, fill);
                        // to avoid scanning the cells twice, we'll fill them and update rowLength here
                        // if there's something above the new starting point, handle that recursively. this deals with cases
                        // like |* **| when we begin filling from (2,0), move down to (2,1), and then move left to (0,1).
                        // the  |****| main scan assumes the portion of the previous row from x to x+lastRowLength has already
                        // been filled. adjusting x and lastRowLength breaks that assumption in this case, so we must fix it
                        if (y != 0 && GetPixel(bmpData, x, y - 1) == match) _MyFill(bmpData, fill, match, x, y - 1, width, height); // use _Fill since there may be more up and left
                    }
                }

                // now at this point we can begin to scan the current row in the rectangular block. the span of the previous
                // row from x (inclusive) to x+lastRowLength (exclusive) has already been filled, so we don't need to
                // check it. so scan across to the right in the current row
                for (; sx < width && GetPixel(bmpData, sx, y) == match; rowLength++, sx++) SetPixel(bmpData, sx, y, fill);
                // now we've scanned this row. if the block is rectangular, then the previous row has already been scanned,
                // so we don't need to look upwards and we're going to scan the next row in the next iteration so we don't
                // need to look downwards. however, if the block is not rectangular, we may need to look upwards or rightwards
                // for some portion of the row. if this row was shorter than the last row, we may need to look rightwards near
                // the end, as in the case of |*****|, where the first row is 5 cells long and the second row is 3 cells long.
                // we must look to the right  |*** *| of the single cell at the end of the second row, i.e. at (4,1)
                if (rowLength < lastRowLength)
                {
                    for (int end = x + lastRowLength; ++sx < end;) // 'end' is the end of the previous row, so scan the current row to
                    {                                          // there. any clear cells would have been connected to the previous
                        if (GetPixel(bmpData, sx, y) == match) MyFillCore(bmpData, fill, match, sx, y, width, height); // row. the cells up and left must be set so use FillCore
                    }
                }
                // alternately, if this row is longer than the previous row, as in the case |*** *| then we must look above
                // the end of the row, i.e at (4,0)                                         |*****|
                else if (rowLength > lastRowLength && y != 0) // if this row is longer and we're not already at the top...
                {
                    for (int ux = x + lastRowLength; ++ux < sx;) // sx is the end of the current row
                    {
                        if (GetPixel(bmpData, ux, y - 1) == match) _MyFill(bmpData, fill, match, ux, y - 1, width, height); // since there may be clear cells up and left, use _Fill
                    }
                }
                lastRowLength = rowLength; // record the new row length
            } while (lastRowLength != 0 && ++y < height); // if we get to a full row or to the bottom, we're done
        }

    }
}
