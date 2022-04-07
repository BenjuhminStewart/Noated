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
        Point current = Point.Empty;
        Point old = Point.Empty;
        int mouseX, mouseY, startX, startY;
        Bitmap bm;
        Graphics g;
        PictureBox pic;
        readonly ColorPicker colorPicker;
        Stack<PictureBox> state = new Stack<PictureBox>();

        public Canvas(PictureBox p)
        {
            pic = p;
            bm = new Bitmap(p.Width, p.Height);
            g = Graphics.FromImage(bm);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            pic.Cursor = new Cursor(Application.StartupPath + "\\precise-dotted.cur");
            pic.Image = bm;
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
                if (paint && (index == 1 || index == 2) && current != Point.Empty)
                {
                    //g.DrawLine(pen, old, current);
                    var x = Math.Min(Math.Min(old.X, current.X), p.X);
                    var y = Math.Min(Math.Min(old.Y, current.Y), p.Y);
                    var xMax = Math.Max(Math.Max(old.X, current.X), p.X);
                    var yMax = Math.Max(Math.Max(old.Y, current.Y), p.Y);
                    var startAngle = (int) (Math.Atan((p.Y - current.Y) * 1.0/(p.X - current.X)) * 180 / Math.PI) + 15;
                    Console.WriteLine(startAngle);
                    g.DrawArc(pen, x, y, xMax - x + 1, yMax - y + 1, startAngle, 75);
                    // g.DrawRectangle(pen, x, y, xMax - x + 1, yMax - y + 1);
                    old = current; 
                   
                } 
                current = p;
                pic.Refresh();
                mouseX = p.X;
                mouseY = p.Y;
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
            old = Point.Empty;
            current = Point.Empty;
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
