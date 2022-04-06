using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;

namespace BenDraw.PanelTools
{
    internal class HandlePens
    {
        static int DEFAULT_PEN_THICKNESS = 5;
        static Color DEFAULT_PEN_COLOR = Color.Black;
        static int DEFAULT_ERASER_THICKNESS = 5;
        static Color DEFAULT_ERASER_COLOR = Color.White;
        public HandlePens(Pen pen, Pen eraser)
        {

            pen = new Pen(DEFAULT_PEN_COLOR, DEFAULT_PEN_THICKNESS);
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            eraser = new Pen(DEFAULT_ERASER_COLOR, DEFAULT_ERASER_THICKNESS);
            eraser.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
            
        }

    }
}
