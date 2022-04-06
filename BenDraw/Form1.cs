using BenDraw.Components;
using BenDraw.Tools;
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
        Point selectedColor = new Point();
        static int DEFAULT_ERASER_THICKNESS = 100;
        Canvas canvas;
        bool paint = false;
        public Point current = new Point();
        public Point old = new Point();
        BD_Pen p = new BD_Pen();
        BD_Eraser erase = new BD_Eraser();
        int state;
        bool isMouseDown = false;
        Button highlighted;

        public Form1()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            canvas = new Canvas(pic);
            state = 1;
            btn_show_color.BackColor = Color.Black;
            pic.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            color_picker.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            color_picker_2.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            highlighted = btn_pencil;
            SetHighlighted(highlighted);

        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            int index = 1;
            canvas.StartPaint(p.GetPen(), e.Location, index);
        }


        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            canvas.Painting(p.GetPen(), e.Location);
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            int index = 1;
            canvas.StopPaint(p.GetPen(), index);
        }

        private void SetHighlighted(Button selected)
        {

            Button lastHighlighted = highlighted;
            highlighted = selected;
            lastHighlighted.BackColor = Color.FromArgb(64, 64, 64);
            highlighted.BackColor = Color.DarkGray;
        }



        private void Thickness_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown.Value;
            p.ChangePenWidth((int)numericUpDown.Value);


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

      unsafe Color GetPixel(BitmapData bmpD, int x, int y)
      {
        var intPtr = (byte*)bmpD.Scan0 +(y * bmpD.Stride) + (x * 4);// first ARGB
        return Color.FromArgb(intPtr[3], intPtr[2], intPtr[1], intPtr[0]);
      }

      unsafe void SetPixel(BitmapData bmpD, int x, int y, Color color)
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
