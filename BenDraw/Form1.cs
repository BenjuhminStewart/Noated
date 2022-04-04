using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        public Form1()
        {
            InitializeComponent();
            //this.Width = Screen.PrimaryScreen.Bounds.Width;
            //this.Height = Screen.PrimaryScreen.Bounds.Height;
        
            index = 1;
            btn_show_color.BackColor = Color.Black;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            erase.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            g.Clear(Color.White);
            pic.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            color_picker.Cursor = new Cursor(Application.StartupPath + "\\cursor-paint.cur");
            pic.Image = bm;
            highlighted = btn_pencil;
            SetHighlighted(highlighted);
            
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
                Fill(bm, pt.X, pt.Y, currColor);
            }

        }


        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (paint)
            {
                if (index == 1)
                {
                    current = e.Location;
                    g.DrawLine(p, old, current);
                    old = current;
                }

                if(index == 2)
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

            if(index == 3)
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
            if(paint)
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
            } catch
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
            if(sfd.ShowDialog() == DialogResult.OK)
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
            if(cd.ShowDialog() == DialogResult.OK)
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
            return new Point((int)(pt.X*px), (int)(pt.Y*py));
        }

        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)
        {
            Color cx = bm.GetPixel(x, y);
            if(cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            Color old_clr = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x,y));
            bm.SetPixel(x, y, new_clr);
            if (old_clr == new_clr) return;

            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if(pt.X >0 && pt.Y > 0 && pt.X < bm.Width-1 && pt.Y < bm.Height-1)
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_clr, new_clr);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_clr, new_clr);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_clr, new_clr);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_clr, new_clr);
                }
            }
        }
    }
}
