using BenDraw.Components;
using BenDraw.PanelTools;
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

        Canvas canvas;
        Toolbar toolbar;
        HandlePens handlePens;
        ColorPicker colorPickerRGB;
        ColorPicker colorPickerBW;
        SaveSystem saveSystem;
        Pen p;
        Pen eraser;
        Button highlighted;

        public Form1()
        {
            InitializeComponent();

            // Set Application Dimensions
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            // Load tools
            toolbar = new Toolbar(btn_pencil);
            toolbar.LoadTools();

            // Create Save System
            saveSystem = new SaveSystem();

            // Handle Pens
            handlePens = new HandlePens();
            Pen[] pens = handlePens.initializePens();
            p = pens[0];
            eraser = pens[1];

            // Set PictureBox, Bitmap, and Grpahics
            canvas = new Canvas(pic);

            // Set Color Pickers
            colorPickerRGB = new ColorPicker(color_picker, btn_show_color);
            colorPickerBW = new ColorPicker(color_picker_2, btn_show_color);

        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if(toolbar.GetState() == 2)
            {
                canvas.StartPaint(eraser, e.Location, toolbar.GetState());
                
            } else
            {
                canvas.StartPaint(p, e.Location, toolbar.GetState());
            }
            
        }


        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolbar.GetState() == 2) 
            {
                canvas.Painting(eraser, e.Location, toolbar.GetState());
                
            }  else
            {
                canvas.Painting(p, e.Location, toolbar.GetState());
            }
           
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if( toolbar.GetState() == 2)
            {
                canvas.StopPaint(eraser, toolbar.GetState());
                
            } else
            {
                canvas.StopPaint(p, toolbar.GetState());
            }
            
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
            handlePens.ChangePenWidth(p, trackBar1, numericUpDown);
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            toolbar.SetState(4, btn_rect);
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            toolbar.SetState(5, btn_line);
        }


        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            toolbar.SetState(3, btn_ellipse);
        }


        private void color_picker_MouseDown(object sender, MouseEventArgs e)
        {
            colorPickerRGB.SelectColor(color_picker, btn_show_color, e.Location, p);
        }

        private void color_picker_MouseUp(object sender, MouseEventArgs e)
        {
            colorPickerRGB.HandleSelectedColor(p, btn_show_color);
        }

        private void color_picker_MouseMove(object sender, MouseEventArgs e)
        {
            colorPickerRGB.FindingColor(pic, color_picker, btn_show_color, e.Location);
        }

        private void cp_MouseDown(object sender, MouseEventArgs e)
        {
            colorPickerBW.SelectColor(color_picker_2, btn_show_color, e.Location, p);
        }

        private void cp_MouseMove(object sender, MouseEventArgs e)
        {
            colorPickerBW.HandleSelectedColor(p, btn_show_color);
        }

        private void cp_MouseUp(object sender, MouseEventArgs e)
        {
            colorPickerBW.FindingColor(pic, color_picker_2, btn_show_color, e.Location);
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            toolbar.SetState(2, btn_eraser);
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            toolbar.SetState(1, btn_pencil);
        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            toolbar.SetState(7, btn_fill);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saveSystem.Save(canvas.GetBitmap(), pic);
        }

        private void trackbar_thickness(object sender, EventArgs e)
        {
            handlePens.ChangePenWidth(p, trackBar1, numericUpDown);
        }

        private void Numeric_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.P)
            {
                toolbar.SetState(1, btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.C)
            {
                canvas.ClearScreen();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left)
            {
                canvas.Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.E)
            {
                toolbar.SetState(2, btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }


        private void AnyButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.P)
            {
                toolbar.SetState(1, btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.C)
            {
                canvas.ClearScreen();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left)
            {
                canvas.Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.E)
            {
                toolbar.SetState(2, btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            handlePens.ChangeColorViaDialog(p, btn_show_color);  
        }

        private void btn_trash_Click(object sender, EventArgs e)
        {
            canvas.ClearScreen();
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            canvas.ShowShapeDrawing(p, toolbar.GetState(), e);
        }
    }
}
