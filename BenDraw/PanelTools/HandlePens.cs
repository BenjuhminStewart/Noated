using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BenDraw.PanelTools
{
    internal class HandlePens
    {
        static int DEFAULT_PEN_THICKNESS = 5;
        static Color DEFAULT_PEN_COLOR = Color.Black;
        static int DEFAULT_ERASER_THICKNESS = 100;
        static Color DEFAULT_ERASER_COLOR = Color.White;
        public HandlePens()
        { 
            
        }

        public Pen[] initializePens ()
        {
            Pen[] pens = new Pen[2];
            Pen pen = new Pen(DEFAULT_PEN_COLOR, DEFAULT_PEN_THICKNESS);
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            Pen eraser = new Pen(DEFAULT_ERASER_COLOR, DEFAULT_ERASER_THICKNESS);
            eraser.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            pens[0] = pen;
            pens[1] = eraser;

            return pens;
        }

        public void ChangePenWidth(Pen p,TrackBar tb, NumericUpDown nud)
        {
            p.Width = tb.Value;
            nud.Value = tb.Value;
        }

        public void ChangeColorViaDialog(Pen p, Panel colorDisplay) 
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                p.Color = cd.Color;
                colorDisplay.BackColor = cd.Color;
            }

        }

        public void ChangeColorBlack(Pen p)
        {
            p.Color = Color.Black;
        }

        public void ChangeColorWhite(Pen p)
        {
            p.Color = Color.White;
        }

    }
}
