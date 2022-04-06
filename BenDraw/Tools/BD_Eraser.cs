using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace BenDraw.Tools
{
    internal class BD_Eraser
    {

        static int DEFUALT_THICKNESS = 100;
        static Color DEFAULT_COLOR = Color.Black;
        private const int index = 2;
        Pen eraser;
        public BD_Eraser()
        {
            eraser = new Pen(DEFAULT_COLOR, DEFUALT_THICKNESS);
            eraser.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

        }

        public void ChangeEraserWidth(int thickness)
        {
            eraser.Width = thickness;
        }

        public void ChangeEraserColor(Color new_color)
        {
            eraser.Color = new_color;
        }

        public Color getColor()
        {
            return eraser.Color;
        }

        public float getThickness()
        {
            return eraser.Width;
        }

        public int getIndex()
        {
            return index;
        }

        public Pen GetEraser()
        {
            return eraser;
        }

    }
}
