using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace BenDraw.Tools
{
    internal class BD_Pen
    {
        static int DEFUALT_THICKNESS = 5;
        static Color DEFAULT_COLOR = Color.Black;
        //private const int index = 1;
        Pen p;
        public BD_Pen()
        {
            p = new Pen(DEFAULT_COLOR, DEFUALT_THICKNESS);
            p.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
         
        }
        
        public void ChangePenWidth(int thickness)
        {
            p.Width = thickness;
        }

        public void ChangePenColor(Color new_color)
        {
            p.Color = new_color;
        }

        public Color getColor()
        {
            return p.Color;
        }

        public float getThickness()
        {
            return p.Width;
        }

        public int getIndex()
        {
            return index;
        }

        public Pen GetPen()
        {
            return p;
        }
    }
}
