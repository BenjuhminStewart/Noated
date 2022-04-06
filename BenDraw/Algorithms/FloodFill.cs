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

        unsafe Color GetPixel(BitmapData bmpD, int x, int y)
        {
            var intPtr = (byte*)bmpD.Scan0 + (y * bmpD.Stride) + (x * 4); // first ARGB
            return Color.FromArgb(intPtr[3], intPtr[2], intPtr[1], intPtr[0]);
        }

        unsafe void SetPixel(BitmapData bmpD, int x, int y, Color color)
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
    }
}
