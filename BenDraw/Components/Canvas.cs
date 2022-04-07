using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using BenDraw.Algorithms;
using System.Windows.Forms;

namespace BenDraw.Components
{
    internal class Canvas
    {
        bool paint = false;
        Point current = new Point();
        Point old = new Point();
        int mouseX, mouseY, startX, startY;
        Bitmap bm;
        Graphics g;
        TextBox cursorLocation;
        PictureBox pic;
        readonly ColorPicker colorPicker;
        Stack<PictureBox> state = new Stack<PictureBox>();

        public Canvas(PictureBox p, TextBox cursorLocation)
        {
            pic = p;
            bm = new Bitmap(p.Width, p.Height);
            g = Graphics.FromImage(bm);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            pic.Cursor = new Cursor(Application.StartupPath + "\\precise-dotted.cur");
            pic.Image = bm;
            this.cursorLocation = cursorLocation;
        }

        static Point set_point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Image.Width / pb.Width;
            float py = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }

        public void StartPaint(Pen pen, Point p, int index)
        {
            paint = true;        
            old = p;

            startX = p.X;
            startY = p.Y;

            Point pt = set_point(pic, p);

            // 6: Color Dropper
            if (index == 6)
            {
                colorPicker.SelectColor(pic, pt, pen);
            }

            // 7 : Fill
            else if(index == 7)
            {
                FloodFill.Fill(bm, p.X, p.Y, pen.Color);
                pic.Refresh();
            }
            pic.Refresh();

        }

        public void Painting(Pen pen, Point p, int index)
        {
            SetLocationLabel(p);
            if (paint && (index == 1 || index == 2))
            {
                current = p;
                g.DrawLine(pen, old, current);
                old = current;
            }
                
            pic.Refresh();
            mouseX = p.X;
            mouseY = p.Y;
        }

        private void SetLocationLabel(Point p)
        {
            cursorLocation.Text = String.Format("(X: {0}, Y: {1})", p.X, p.Y);
        }

        public void StopPaint(Pen p, int index)
        {
            paint = false;
            int shapeWidth = mouseX - startX;
            int shapeHeight = mouseY - startY;

            // 3 : Ellipse
            if(index == 3)
            {
                DrawEllipse(p, startX, startY, shapeWidth, shapeHeight);
            }

            // 4 : Rectangle
            if (index == 4)
            {
                DrawRectangle(p, startX, startY, shapeWidth, shapeHeight);
            }

            // 5 : Line
            if (index == 5)
            {
                DrawLine(p, startX, startY, mouseX, mouseY);
            }

            pic.Refresh();
        }

        public void ShowShapeDrawing(Pen p, int index, PaintEventArgs e)
        {
            int shapeWidth = mouseX - startX;
            int shapeHeight = mouseY - startY;
            Graphics g = e.Graphics; 
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, startX, startY, shapeWidth, shapeHeight);
                }

                // 4 : Rectangle
                else if (index == 4)
                {
                    g.DrawRectangle(p, startX, startY, shapeWidth, shapeHeight);
                }

                // 5 : Line
                else if (index == 5)
                {
                    g.DrawLine(p, startX, startY, mouseX, mouseY);
                }
            }

        }


        void DrawEllipse(Pen p, int locationX, int locationY, int width, int height)
        {
            g.DrawEllipse(p, locationX, locationY, width, height);
            pic.Refresh();
            //state.Push(pic);
        }

        void DrawRectangle(Pen p, int locationX, int locationY, int width, int height)
        {
            g.DrawRectangle(p, locationX, locationY, width, height);
            pic.Refresh();
            //state.Push(pic);
        }

        void DrawLine(Pen p, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(p, x1, y1, x2, y2);
            pic.Refresh();
            //state.Push(pic);
        }



        public void Undo()
        {
            try
            {
                state.Pop();
                pic = state.Peek();
                //Debug.WriteLine("{0}", pic);

            }
            catch
            {
                ClearScreen();
                //Debug.WriteLine("{0}", pic);
            }

        }

        public void ClearScreen()
        {
            g.Clear(Color.White);
            pic.Image = bm;
        }

        public Bitmap GetBitmap()
        {
            return bm;
        }

        public Graphics GetGraphics()
        {
            return g;
        }

        public PictureBox GetPictureBox()
        {
            return pic;
        }

    }
}
