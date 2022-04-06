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
        SaveSystem saveSystem;
        Pen p;
        Pen eraser;

        public Form1()
        {
            InitializeComponent();

            // Set Color Pickers
            colorPickerRGB = new ColorPicker(color_picker, btn_show_color, A_Value, R_Value, G_Value, B_Value, A_Label, R_Label, G_Label, B_Label);

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
            penThicknessLabel.Text = String.Format("{0} px", p.Width);

            // Set PictureBox, Bitmap, and Grpahics
            canvas = new Canvas(pic);



        }
        private void pic_KeyDown(object sender, KeyEventArgs e)
        {
            Commands(sender, e);
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (toolbar.GetState() == 2)
            {
                canvas.StartPaint(eraser, e.Location, toolbar.GetState());

            }
            else if (toolbar.GetState() == 6)
            {
                colorPickerRGB.SelectColor(pic, e.Location, p);
            }
            else
            {
                canvas.StartPaint(p, e.Location, toolbar.GetState());
            }

            pic.Focus();
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolbar.GetState() == 2)
            {
                canvas.Painting(eraser, e.Location, toolbar.GetState());

            }
            else if (toolbar.GetState() == 6)
            {
                colorPickerRGB.FindingColor(pic, e.Location);
            }
            else
            {
                canvas.Painting(p, e.Location, toolbar.GetState());
            }

        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (toolbar.GetState() == 2)
            {
                canvas.StopPaint(eraser, toolbar.GetState());

            }
            else if (toolbar.GetState() == 6)
            {
                colorPickerRGB.HandleSelectedColor(p);
            }
            else
            {
                canvas.StopPaint(p, toolbar.GetState());
            }

        }

        private void Thickness_ValueChanged(object sender, EventArgs e)
        {
            handlePens.ChangePenWidth(p, trackBar1, penThicknessLabel);
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
            colorPickerRGB.SelectColor(color_picker, e.Location, p);
        }

        private void color_picker_MouseUp(object sender, MouseEventArgs e)
        {
            colorPickerRGB.HandleSelectedColor(p);
        }

        private void color_picker_MouseMove(object sender, MouseEventArgs e)
        {
            colorPickerRGB.FindingColor(pic, color_picker, e.Location);
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
            handlePens.ChangePenWidth(p, trackBar1, penThicknessLabel);
        }

        private void Numeric_KeyDown(object sender, KeyEventArgs e)
        {
            Commands(sender, e);
        }

        private void AnyButton_KeyDown(object sender, KeyEventArgs e)
        {
            Commands(sender, e);
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

        private void BlackCircleMouseDown(object sender, MouseEventArgs e)
        {
            colorPickerRGB.ChangeColorBlack(p);
        }

        private void WhiteCircleMouseDown(object sender, MouseEventArgs e)
        {
            colorPickerRGB.ChangeColorWhite(p);
        }

        private void Commands(object sender, KeyEventArgs e)
        {
            // Silence Enter Command
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Command For Pen
            if (e.KeyCode == Keys.D)
            {
                toolbar.SetState(1, btn_pencil);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Command For Clear
            if (e.KeyCode == Keys.C)
            {
                canvas.ClearScreen();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Command for Undo
            if (e.KeyCode == Keys.Left)
            {
                canvas.Undo();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Command For Eraser
            if (e.KeyCode == Keys.E)
            {
                toolbar.SetState(2, btn_eraser);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

            // Command For Fill
            if (e.KeyCode == Keys.F)
            {
                toolbar.SetState(7, btn_fill);
            }


            // Command For Color Dropper
            if (e.KeyCode == Keys.S)
            {
                toolbar.SetState(6, colorDropper);
            }
        }

        private void SelectColorDropper(object sender, EventArgs e)
        {
            toolbar.SetState(6, colorDropper);
        }



        private void ARGB_Change(object sender, KeyEventArgs e)
        {
            Commands(sender, e);
        }

        private void ARGB_Changed(object sender, EventArgs e)
        {
            if(colorPickerRGB != null)
            {
                colorPickerRGB.ChangeARGBValue(p);
            }
        }

        private void R_Label_Click(object sender, EventArgs e)
        {

        }
    }
}
