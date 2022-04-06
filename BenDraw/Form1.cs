using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenDraw
{
    public partial class Form1 : Form
    {
        static int DEFAULT_THICKNESS = 5;
        static Color DEFAULT_COLOR = Color.Black;
        Point selectedColor = new Point();
        Color currColor = DEFAULT_COLOR;
        int thickness = DEFAULT_THICKNESS;
        static int DEFAULT_ERASER_THICKNESS = 99;
        Bitmap bm;
        Graphics g;
        bool paint = false;
        public Point current = new Point();
        public Point old = new Point();
        Pen p = new Pen(DEFAULT_COLOR, DEFAULT_THICKNESS);
        Pen erase = new Pen(Color.White, DEFAULT_ERASER_THICKNESS);
        int index;
        int x, y, sX, sY, cX, cY;
        bool isMouseDown = false;
        Button highlighted;
        Stack<PictureBox> state = new Stack<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            index = 1;
            btn_show_color.BackColor = Color.Black;
            bm = new Bitmap(pic.Width, pic.Height);

            g = Graphics.FromImage(bm);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            p.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
            erase.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
            g.Clear(Color.White);
            pic.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            color_picker.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            color_picker_2.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            pic.Image = bm;
            state.Push(pic);
            highlighted = btn_pencil;
            SetHighlighted(highlighted);

        }

        private void Clear()
        {
            g.Clear(Color.White);
            pic.Image = bm;
        }

        private void Undo()
        {
            try
            {
                state.Pop();
                pic = state.Peek();
                Debug.WriteLine("{0}", pic);

            }
            catch (Exception ex)
            {
                Clear();
                Debug.WriteLine("{0}", pic);
            }

        }



        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            old = e.Location;

            cX = e.X;
            cY = e.Y;


            if (index == 7)
            {

                Point pt = set_point(pic, e.Location);
                //FloodFill(bm, pt, currColor);
                // FloodFillDP(bm, pt.X, pt.Y, currColor);
                // FloodFillFastDP(bm, pt.X, pt.Y, currColor);
                MyFill(bm, pt.X, pt.Y, currColor);
                pic.Refresh();
            }


        }

public static void MyFill(Bitmap bm, int x, int y, Color fill)
{
    int n = bm.Width;
    int m = bm.Height;
    var bmpData = bm.LockBits(new Rectangle(0, 0, n, m), ImageLockMode.ReadWrite, bm.PixelFormat);
    Color replacedColor = GetPixel(bmpData, x, y);
    if(fill == replacedColor){
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
  while(true)
  {
    int ox = x, oy = y;
    while(y != 0 && GetPixel(bmpData,x, y-1) == match) y--;
    while(x != 0 && GetPixel(bmpData,x-1,y) == match) x--;
    if(x == ox && y == oy) break;
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
    if(lastRowLength != 0 && GetPixel(bmpData,x,y) == fill) // if this is not the first row and the leftmost cell is filled...
    {
      do
      {
        if(--lastRowLength == 0) return; // shorten the row. if it's full, we're done
      } while(GetPixel(bmpData,++x,y) == fill); // otherwise, update the starting point of the main scan to match
      sx = x;
    }
    // we also want to handle the opposite case, | **|, where we begin scanning a 2-wide rectangular block and
    // then find on the next row that it has     |***| gotten wider on the left. again, we could handle this
    // with recursion but we'd prefer to adjust x and lastRowLength instead
    else
    {
      for(; x != 0 && GetPixel(bmpData,x-1,y) == match; rowLength++, lastRowLength++)
      {
        SetPixel(bmpData,--x,y, fill);
         // to avoid scanning the cells twice, we'll fill them and update rowLength here
        // if there's something above the new starting point, handle that recursively. this deals with cases
        // like |* **| when we begin filling from (2,0), move down to (2,1), and then move left to (0,1).
        // the  |****| main scan assumes the portion of the previous row from x to x+lastRowLength has already
        // been filled. adjusting x and lastRowLength breaks that assumption in this case, so we must fix it
        if(y != 0 && GetPixel(bmpData,x,y-1) == match) _MyFill(bmpData, fill, match, x, y-1, width, height); // use _Fill since there may be more up and left
      }
    }

    // now at this point we can begin to scan the current row in the rectangular block. the span of the previous
    // row from x (inclusive) to x+lastRowLength (exclusive) has already been filled, so we don't need to
    // check it. so scan across to the right in the current row
    for(; sx < width && GetPixel(bmpData,sx,y) == match; rowLength++, sx++) SetPixel(bmpData,sx,y, fill);
    // now we've scanned this row. if the block is rectangular, then the previous row has already been scanned,
    // so we don't need to look upwards and we're going to scan the next row in the next iteration so we don't
    // need to look downwards. however, if the block is not rectangular, we may need to look upwards or rightwards
    // for some portion of the row. if this row was shorter than the last row, we may need to look rightwards near
    // the end, as in the case of |*****|, where the first row is 5 cells long and the second row is 3 cells long.
    // we must look to the right  |*** *| of the single cell at the end of the second row, i.e. at (4,1)
    if(rowLength < lastRowLength)
    {
      for(int end=x+lastRowLength; ++sx < end; ) // 'end' is the end of the previous row, so scan the current row to
      {                                          // there. any clear cells would have been connected to the previous
        if(GetPixel(bmpData,sx,y) == match) MyFillCore(bmpData, fill, match, sx, y, width, height); // row. the cells up and left must be set so use FillCore
      }
    }
    // alternately, if this row is longer than the previous row, as in the case |*** *| then we must look above
    // the end of the row, i.e at (4,0)                                         |*****|
    else if(rowLength > lastRowLength && y != 0) // if this row is longer and we're not already at the top...
    {
      for(int ux=x+lastRowLength; ++ux<sx; ) // sx is the end of the current row
      {
        if(GetPixel(bmpData,ux,y-1) == match) _MyFill(bmpData, fill, match, ux, y-1, width, height); // since there may be clear cells up and left, use _Fill
      }
    }
    lastRowLength = rowLength; // record the new row length
  } while(lastRowLength != 0 && ++y < height); // if we get to a full row or to the bottom, we're done
}


        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (bm.GetPixel(e.Location.X, e.Location.Y) == Color.Black)
                {
                    pic.Cursor = new Cursor(Application.StartupPath + "\\cursor-white.cur");
                }
                else
                {
                    pic.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
                }
                if (paint)
                {
                    if (index == 1)
                    {
                        current = e.Location;
                        g.DrawLine(p, old, current);
                        old = current;
                    }

                    if (index == 2)
                    {
                        current = e.Location;
                        g.DrawLine(erase, old, current);
                        old = current;
                    }
                }
                pic.Refresh();
                x = e.X;
                y = e.Y;
                sX = e.X - cX;
                sY = e.Y - cY;
            }
            catch
            {

            }

        }

        private void SetHighlighted(Button selected)
        {

            Button lastHighlighted = highlighted;
            highlighted = selected;
            lastHighlighted.BackColor = Color.FromArgb(64, 64, 64);
            highlighted.BackColor = Color.DarkGray;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            sX = x - cX;
            sY = y - cY;

            if (index == 3)
            {
                g.DrawEllipse(p, cX, cY, sX, sY);
            }

            if (index == 4)
            {

                g.DrawRectangle(p, cX, cY, sX, sY);
            }

            if (index == 5)
            {
                g.DrawLine(p, cX, cY, x, y);
            }

            Debug.WriteLine("{0}", bm);
            pic.Refresh();
            state.Push(pic);
        }



        private void Thickness_ValueChanged(object sender, EventArgs e)
        {
            thickness = (int)numericUpDown.Value;
            trackBar1.Value = thickness;
            p.Width = (float)numericUpDown.Value;


        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            index = 4;
            SetHighlighted(btn_rect);
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
            SetHighlighted(btn_line);
        }

        private void pic_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }

                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }

                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }

        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            index = 3;
            SetHighlighted(btn_ellipse);
        }


        private void color_picker_MouseDown(object sender, MouseEventArgs e)
        {
            selectedColor = set_point(color_picker, e.Location);
            btn_show_color.BackColor = ((Bitmap)color_picker.Image).GetPixel(selectedColor.X, selectedColor.Y);
            currColor = btn_show_color.BackColor;

            isMouseDown = true;
        }

        private void color_picker_MouseUp(object sender, MouseEventArgs e)
        {
            p.Color = btn_show_color.BackColor;
            currColor = p.Color;
            if (index == 0)
            {
                index = 1;
            }
            isMouseDown = false;
        }

        private void color_picker_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isMouseDown)
                {
                    selectedColor = set_point(color_picker, e.Location);
                    btn_show_color.BackColor = ((Bitmap)color_picker.Image).GetPixel(selectedColor.X, selectedColor.Y);
                }
            }
            catch
            {
                btn_show_color.BackColor = Color.Black;
                currColor = btn_show_color.BackColor;
            }



            pic.Refresh();
        }

        private void cp_MouseDown(object sender, MouseEventArgs e)
        {
            selectedColor = set_point(color_picker_2, e.Location);
            btn_show_color.BackColor = ((Bitmap)color_picker_2.Image).GetPixel(selectedColor.X, selectedColor.Y);
            currColor = btn_show_color.BackColor;

            isMouseDown = true;
        }

        private void cp_MouseMove(object sender, MouseEventArgs e)
        {
            p.Color = btn_show_color.BackColor;
            currColor = p.Color;
            if (index == 0)
            {
                index = 1;
            }
            isMouseDown = false;
        }

        private void cp_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (isMouseDown)
                {
                    selectedColor = set_point(color_picker_2, e.Location);
                    btn_show_color.BackColor = ((Bitmap)color_picker_2.Image).GetPixel(selectedColor.X, selectedColor.Y);
                }
            }
            catch
            {
                btn_show_color.BackColor = Color.Black;
                currColor = btn_show_color.BackColor;
            }



            pic.Refresh();
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
            SetHighlighted(btn_eraser);
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
            SetHighlighted(btn_pencil);
        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7;
            SetHighlighted(btn_fill);
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*| *.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image Saved Successfully...");
            }
        }

        private void trackbar_thickness(object sender, EventArgs e)
        {
            thickness = trackBar1.Value;
            numericUpDown.Value = trackBar1.Value;
        }

        private void App_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down

                //this.doSomething();

                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.C)
            {

                Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left)
            {

                Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.P)
            {
                //enter key is down

                index = 1;
                SetHighlighted(btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.E)
            {
                //enter key is down

                index = 2;
                SetHighlighted(btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }



        private void Numeric_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down

                //this.doSomething();

                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.P)
            {
                //enter key is down

                index = 1;
                SetHighlighted(btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.C)
            {

                Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left)
            {

                Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.E)
            {
                //enter key is down

                index = 2;
                SetHighlighted(btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void panel_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down

                //this.doSomething();



            }

            if (e.KeyCode == Keys.P)
            {
                //enter key is down

                index = 1;
                SetHighlighted(btn_pencil);


            }

            if (e.KeyCode == Keys.C)
            {

                Clear();

            }

            if (e.KeyCode == Keys.Left)
            {

                Undo();

            }

            if (e.KeyCode == Keys.E)
            {
                //enter key is down

                index = 2;
                SetHighlighted(btn_eraser);

            }
        }

        private void AnyButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down

                //this.doSomething();

                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.P)
            {
                //enter key is down

                index = 1;
                SetHighlighted(btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            if (e.KeyCode == Keys.C)
            {

                Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left)
            {

                Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.E)
            {
                //enter key is down

                index = 2;
                SetHighlighted(btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                currColor = cd.Color;
                p.Color = cd.Color;
                btn_show_color.BackColor = cd.Color;

            }
        }

        private void btn_trash_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
        }

        static Point set_point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Image.Width / pb.Width;
            float py = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }

        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }

        static void FloodFill(Bitmap bmp, Point pt, Color replacementColor)
        {
            Queue<Point> q = new Queue<Point>();
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            q.Enqueue(pt);
            while (q.Count > 0)
            {
                Point n = q.Dequeue();
                if (!ColorMatch(bmp.GetPixel(n.X, n.Y), targetColor))
                    continue;
                Point w = n, e = new Point(n.X + 1, n.Y);
                while ((w.X >= 0) && ColorMatch(bmp.GetPixel(w.X, w.Y), targetColor))
                {
                    bmp.SetPixel(w.X, w.Y, replacementColor);
                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X <= bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    bmp.SetPixel(e.X, e.Y, replacementColor);
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
            }
        }

        int validate(int x, int y, int n, int m)
        {
            // Less than bounds
            if (x < 0 || y < 0)
            {
                return 0;
            }
            // Greater than
            if (x >= n || y >= m)
            {
                return 0;
            }

            return 1;
        }

        void FloodFillDP(Bitmap bm, int x, int y, Color fill)
        {
            int n = bm.Width;
            int m = bm.Height;
            var rect = new Rectangle(0, 0, n, m);
            var bmpData = bm.LockBits(rect, ImageLockMode.ReadWrite, bm.PixelFormat);
            Color replacedColor = GetPixel(bmpData, x, y);
            int[,] visited = new int[n, m];

            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(x, y));

            while (queue.Count > 0)
            {
                Tuple<int, int> coord = queue.Dequeue();
                int px = coord.Item1;
                int py = coord.Item2;
                if (visited[px, py] == 1)
                {
                    continue;
                }

                SetPixel(bmpData, px, py, fill);
                visited[px, py] = 1;

                // Fill Above

                if (validate(px + 1, py, n, m) == 1
                    && visited[px + 1, py] == 0
                    && GetPixel(bmpData, px + 1, py) == replacedColor)
                {
                    queue.Enqueue(new Tuple<int, int>(px + 1, py));

                }

                // Fill Right
                if (validate(px, py + 1, n, m) == 1
                    && visited[px, py + 1] == 0
                    && GetPixel(bmpData, px, py + 1) == replacedColor)
                {
                    queue.Enqueue(new Tuple<int, int>(px, py + 1));

                }

                // Fill Left
                if (validate(px, py - 1, n, m) == 1
                    && visited[px, py - 1] == 0
                    && GetPixel(bmpData, px, py - 1) == replacedColor)
                {
                    queue.Enqueue(new Tuple<int, int>(px, py - 1));

                }

                // Fill Below
                if (validate(px - 1, py, n, m) == 1
                    && visited[px - 1, py] == 0
                    && GetPixel(bmpData, px - 1, py) == replacedColor)
                {
                    queue.Enqueue(new Tuple<int, int>(px - 1, py));

                }
            }
        }

      unsafe static Color GetPixel(BitmapData bmpD, int x, int y)
      {
        var intPtr = (byte*)bmpD.Scan0 +(y * bmpD.Stride) + (x * 4);// first ARGB
        return Color.FromArgb(intPtr[3], intPtr[2], intPtr[1], intPtr[0]);
      }

      unsafe static void SetPixel(BitmapData bmpD, int x, int y, Color color)
      {
        var intPtr = (byte*) bmpD.Scan0 +(y * bmpD.Stride) + (x*4);// first ARGB
        intPtr[0] = color.B;
        intPtr[1] = color.G;
        intPtr[2] = color.R;
        intPtr[3] = color.A;
      }
      void ReplaceAll(Bitmap bm, int x, int y, Color fill){
        int n = bm.Width;
        int m = bm.Height;
        var rect = new Rectangle(0, 0, n, m);
        var bmpData = bm.LockBits(rect, ImageLockMode.ReadWrite, bm.PixelFormat);
        Color replacedColor = GetPixel(bmpData, x, y);
        Parallel.For(0, m, (int i) => {
            for(int j = 0; j < n; j++){
              if(GetPixel(bmpData, j, i) == replacedColor){
                SetPixel(bmpData, j, i, fill);
              }
          }
        });
        bm.UnlockBits(bmpData);
      }
      void FloodFillFastDP(Bitmap bm, int x, int y, Color fill)
      {
        int n = bm.Width;
        int m = bm.Height;
        var visited = new bool[n, m];
        var bmpData = bm.LockBits(new Rectangle(0, 0, n, m), ImageLockMode.ReadWrite, bm.PixelFormat);
        Color replacedColor = GetPixel(bmpData, x, y);
        if(fill == replacedColor){
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
                for (int j = Math.Max(0, px - 1); j <= Math.Min(n -1, px + 1); j++)
                {
                    if(!visited[j, i] && GetPixel(bmpData, j, i) == replacedColor){
                        queue.Enqueue(new Tuple<int, int>(j, i));
                    }
                }
            }
        }
        bm.UnlockBits(bmpData);
      }
    }
}
