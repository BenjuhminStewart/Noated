using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenDraw.Components
{
    internal class ColorPicker
    {
        public Color currentColor;
        bool isMouseDown;
        TrackBar a;
        TrackBar r;
        TrackBar g;
        TrackBar b;
        Label aTag;
        Label rTag;
        Label gTag;
        Label bTag;
        Panel colorDisplay;
        public ColorPicker(PictureBox pb, Panel colorDisplay, TrackBar a, TrackBar r, TrackBar g, TrackBar b, Label aTag, Label rTag, Label gTag, Label bTag)
        {
            pb.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            currentColor = Color.Black;
            this.colorDisplay = colorDisplay;
            this.colorDisplay.BackColor = currentColor;
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;

            this.aTag = aTag;
            this.rTag = rTag;
            this.gTag = gTag;
            this.bTag = bTag;

            InitializeARGB();
            isMouseDown = false;
        }

        private void InitializeARGB()
        {

            // Set Maximums
            this.a.Maximum = 255;
            this.r.Maximum = 255;
            this.g.Maximum = 255;
            this.b.Maximum = 255;

            // Set Minimums
            this.a.Minimum = 0;
            this.r.Minimum = 0;
            this.g.Minimum = 0;
            this.b.Minimum = 0;

            // Set Start Values;
            SetARGBValue(currentColor);
        }


        public void SelectColor(PictureBox pb, Point p, Pen pen)
        {
            Point pointSelected = set_point(pb, p);
            Color color = ((Bitmap)pb.Image).GetPixel(pointSelected.X, pointSelected.Y);
            pen.Color = color;
            currentColor = color;
            colorDisplay.BackColor = color;
            SetARGBValue(color);
            isMouseDown = true;
        }

        public void HandleSelectedColor(Pen p)
        {
            p.Color = colorDisplay.BackColor;
            SetARGBValue(colorDisplay.BackColor);
            isMouseDown = false;
        }

        public void FindingColor(PictureBox canvas, PictureBox pb, Point p)
        {
            try
            {
                if (isMouseDown)
                {
                    Point selectedColor = set_point(pb, p);
                    Color c = ((Bitmap)pb.Image).GetPixel(selectedColor.X, selectedColor.Y);
                    colorDisplay.BackColor = c;
                    SetARGBValue(c);
                }
            }
            catch
            {
            }

            canvas.Refresh();
        }

        public void FindingColor(PictureBox canvas, Point p)
        {
            try
            {
                if (isMouseDown)
                {
                    Point selectedColor = set_point(canvas, p);
                    Color c = ((Bitmap)canvas.Image).GetPixel(selectedColor.X, selectedColor.Y);
                    colorDisplay.BackColor = c;
                    SetARGBValue(c);
                }
            }
            catch
            {
            }
        }

        public void SetARGBValue(Color c)
        {
            a.Value = c.A;
            r.Value = c.R;
            g.Value = c.G;
            b.Value = c.B;

            aTag.Text = String.Format("A:   {0}", a.Value);
            rTag.Text = String.Format("R:   {0}", r.Value);
            gTag.Text = String.Format("G:   {0}", g.Value);
            bTag.Text = String.Format("B:   {0}", b.Value);
        }

        public void ChangeARGBValue(Pen p)
        {
            Color c = Color.FromArgb((int)a.Value, (int)r.Value, (int)g.Value, (int)b.Value);
            SetARGBValue(c);
            colorDisplay.BackColor = c;
            p.Color = c;
        }

        public void ChangeColorBlack(Pen p)
        {
            p.Color = Color.Black;
            colorDisplay.BackColor = Color.Black;
            SetARGBValue(Color.Black);
        }

        public void ChangeColorWhite(Pen p)
        {
            p.Color = Color.White;
            colorDisplay.BackColor = Color.White;
            SetARGBValue(Color.White);
        }

        static Point set_point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Image.Width / pb.Width;
            float py = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }
    }
}
