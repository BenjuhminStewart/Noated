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
        public ColorPicker(PictureBox pb, Panel colorDisplay)
        {
            pb.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            currentColor = Color.Black;
            colorDisplay.BackColor = currentColor;
            isMouseDown = false;
        }

        public void SelectColor(PictureBox pb, Panel colorDisplay, Point p, Pen pen)
        {
            Point pointSelected = set_point(pb, p);
            Color color = ((Bitmap)pb.Image).GetPixel(pointSelected.X, pointSelected.Y);
            pen.Color = color;
            currentColor = color;
            colorDisplay.BackColor = color;
            isMouseDown = true;
        }

        public void HandleSelectedColor(Pen p, Panel colorDisplay)
        {
            p.Color = colorDisplay.BackColor;
            isMouseDown = false;
        }

        public void FindingColor(PictureBox canvas,PictureBox pb, Panel colorDisplay, Point p)
        {
            try
            {
                if (isMouseDown)
                {
                    Point selectedColor = set_point(pb, p);
                    colorDisplay.BackColor = ((Bitmap)pb.Image).GetPixel(selectedColor.X, selectedColor.Y);
                }
            }
            catch
            {
            }

            canvas.Refresh();
        }

        static Point set_point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Image.Width / pb.Width;
            float py = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }
    }
}
