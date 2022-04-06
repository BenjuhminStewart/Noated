
namespace BenDraw
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pic_color = new System.Windows.Forms.Panel();
            this.panel_backdrop = new System.Windows.Forms.Panel();
            this.btn_WhiteCircle = new BenDraw.Components.RoundButton();
            this.btn_BlackCircle = new BenDraw.Components.RoundButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_pencil = new System.Windows.Forms.Button();
            this.btn_trash = new System.Windows.Forms.Button();
            this.btn_rect = new System.Windows.Forms.Button();
            this.btn_color = new System.Windows.Forms.Button();
            this.color_picker = new System.Windows.Forms.PictureBox();
            this.btn_fill = new System.Windows.Forms.Button();
            this.btn_show_color = new System.Windows.Forms.Panel();
            this.btn_ellipse = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.btn_eraser = new System.Windows.Forms.Button();
            this.panel_lower = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.pic_color.SuspendLayout();
            this.panel_backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_picker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pic_color.Controls.Add(this.panel_backdrop);
            this.pic_color.Dock = System.Windows.Forms.DockStyle.Top;
            this.pic_color.Location = new System.Drawing.Point(0, 0);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(1554, 128);
            this.pic_color.TabIndex = 0;
            // 
            // panel_backdrop
            // 
            this.panel_backdrop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_backdrop.Controls.Add(this.btn_WhiteCircle);
            this.panel_backdrop.Controls.Add(this.btn_BlackCircle);
            this.panel_backdrop.Controls.Add(this.trackBar1);
            this.panel_backdrop.Controls.Add(this.btn_line);
            this.panel_backdrop.Controls.Add(this.btn_save);
            this.panel_backdrop.Controls.Add(this.btn_pencil);
            this.panel_backdrop.Controls.Add(this.btn_trash);
            this.panel_backdrop.Controls.Add(this.btn_rect);
            this.panel_backdrop.Controls.Add(this.btn_color);
            this.panel_backdrop.Controls.Add(this.color_picker);
            this.panel_backdrop.Controls.Add(this.btn_fill);
            this.panel_backdrop.Controls.Add(this.btn_show_color);
            this.panel_backdrop.Controls.Add(this.btn_ellipse);
            this.panel_backdrop.Controls.Add(this.numericUpDown);
            this.panel_backdrop.Controls.Add(this.btn_eraser);
            this.panel_backdrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_backdrop.Location = new System.Drawing.Point(0, 0);
            this.panel_backdrop.Name = "panel_backdrop";
            this.panel_backdrop.Size = new System.Drawing.Size(1554, 128);
            this.panel_backdrop.TabIndex = 8;
            // 
            // btn_WhiteCircle
            // 
            this.btn_WhiteCircle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_WhiteCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_WhiteCircle.FlatAppearance.BorderSize = 0;
            this.btn_WhiteCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_WhiteCircle.Image = ((System.Drawing.Image)(resources.GetObject("btn_WhiteCircle.Image")));
            this.btn_WhiteCircle.Location = new System.Drawing.Point(161, 61);
            this.btn_WhiteCircle.Name = "btn_WhiteCircle";
            this.btn_WhiteCircle.Size = new System.Drawing.Size(55, 55);
            this.btn_WhiteCircle.TabIndex = 10;
            this.btn_WhiteCircle.UseVisualStyleBackColor = false;
            this.btn_WhiteCircle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WhiteCircleMouseDown);
            // 
            // btn_BlackCircle
            // 
            this.btn_BlackCircle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_BlackCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_BlackCircle.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_BlackCircle.FlatAppearance.BorderSize = 0;
            this.btn_BlackCircle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_BlackCircle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_BlackCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BlackCircle.Image = ((System.Drawing.Image)(resources.GetObject("btn_BlackCircle.Image")));
            this.btn_BlackCircle.Location = new System.Drawing.Point(161, 4);
            this.btn_BlackCircle.Name = "btn_BlackCircle";
            this.btn_BlackCircle.Size = new System.Drawing.Size(55, 55);
            this.btn_BlackCircle.TabIndex = 9;
            this.btn_BlackCircle.UseVisualStyleBackColor = false;
            this.btn_BlackCircle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlackCircleMouseDown);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(222, 71);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(555, 45);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackbar_thickness);
            this.trackBar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_line
            // 
            this.btn_line.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_line.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_line.FlatAppearance.BorderSize = 0;
            this.btn_line.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_line.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_line.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_line.ForeColor = System.Drawing.Color.Black;
            this.btn_line.Image = ((System.Drawing.Image)(resources.GetObject("btn_line.Image")));
            this.btn_line.Location = new System.Drawing.Point(601, 10);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(50, 50);
            this.btn_line.TabIndex = 7;
            this.btn_line.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_line.UseVisualStyleBackColor = false;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            this.btn_line.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSeaGreen;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_save.ForeColor = System.Drawing.Color.Black;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(664, 10);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(50, 50);
            this.btn_save.TabIndex = 6;
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            this.btn_save.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_pencil
            // 
            this.btn_pencil.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_pencil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_pencil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pencil.FlatAppearance.BorderSize = 0;
            this.btn_pencil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_pencil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_pencil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pencil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_pencil.ForeColor = System.Drawing.Color.Black;
            this.btn_pencil.Image = ((System.Drawing.Image)(resources.GetObject("btn_pencil.Image")));
            this.btn_pencil.Location = new System.Drawing.Point(349, 9);
            this.btn_pencil.Name = "btn_pencil";
            this.btn_pencil.Size = new System.Drawing.Size(50, 50);
            this.btn_pencil.TabIndex = 3;
            this.btn_pencil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_pencil.UseVisualStyleBackColor = false;
            this.btn_pencil.Click += new System.EventHandler(this.btn_pencil_Click);
            this.btn_pencil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_trash
            // 
            this.btn_trash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_trash.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_trash.FlatAppearance.BorderSize = 0;
            this.btn_trash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_trash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_trash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_trash.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_trash.ForeColor = System.Drawing.Color.Black;
            this.btn_trash.Image = ((System.Drawing.Image)(resources.GetObject("btn_trash.Image")));
            this.btn_trash.Location = new System.Drawing.Point(727, 11);
            this.btn_trash.Name = "btn_trash";
            this.btn_trash.Size = new System.Drawing.Size(50, 50);
            this.btn_trash.TabIndex = 5;
            this.btn_trash.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_trash.UseVisualStyleBackColor = true;
            this.btn_trash.Click += new System.EventHandler(this.btn_trash_Click);
            this.btn_trash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_rect
            // 
            this.btn_rect.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_rect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_rect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rect.FlatAppearance.BorderSize = 0;
            this.btn_rect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_rect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_rect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_rect.ForeColor = System.Drawing.Color.Black;
            this.btn_rect.Image = ((System.Drawing.Image)(resources.GetObject("btn_rect.Image")));
            this.btn_rect.Location = new System.Drawing.Point(538, 9);
            this.btn_rect.Name = "btn_rect";
            this.btn_rect.Size = new System.Drawing.Size(50, 50);
            this.btn_rect.TabIndex = 6;
            this.btn_rect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_rect.UseVisualStyleBackColor = false;
            this.btn_rect.Click += new System.EventHandler(this.btn_rect_Click);
            this.btn_rect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_color
            // 
            this.btn_color.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_color.FlatAppearance.BorderSize = 0;
            this.btn_color.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_color.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_color.ForeColor = System.Drawing.Color.Black;
            this.btn_color.Image = ((System.Drawing.Image)(resources.GetObject("btn_color.Image")));
            this.btn_color.Location = new System.Drawing.Point(222, 9);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(50, 50);
            this.btn_color.TabIndex = 1;
            this.btn_color.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_color.UseVisualStyleBackColor = false;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            this.btn_color.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // color_picker
            // 
            this.color_picker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.color_picker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.color_picker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.color_picker.Image = global::BenDraw.Properties.Resources.color_picker;
            this.color_picker.Location = new System.Drawing.Point(12, 4);
            this.color_picker.Name = "color_picker";
            this.color_picker.Size = new System.Drawing.Size(118, 115);
            this.color_picker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.color_picker.TabIndex = 4;
            this.color_picker.TabStop = false;
            this.color_picker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.color_picker_MouseDown);
            this.color_picker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.color_picker_MouseMove);
            this.color_picker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.color_picker_MouseUp);
            // 
            // btn_fill
            // 
            this.btn_fill.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_fill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_fill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fill.FlatAppearance.BorderSize = 0;
            this.btn_fill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_fill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_fill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_fill.ForeColor = System.Drawing.Color.Black;
            this.btn_fill.Image = ((System.Drawing.Image)(resources.GetObject("btn_fill.Image")));
            this.btn_fill.Location = new System.Drawing.Point(286, 9);
            this.btn_fill.Name = "btn_fill";
            this.btn_fill.Size = new System.Drawing.Size(50, 50);
            this.btn_fill.TabIndex = 2;
            this.btn_fill.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_fill.UseVisualStyleBackColor = false;
            this.btn_fill.Click += new System.EventHandler(this.btn_fill_Click);
            this.btn_fill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // btn_show_color
            // 
            this.btn_show_color.BackColor = System.Drawing.Color.Black;
            this.btn_show_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_show_color.Location = new System.Drawing.Point(136, 5);
            this.btn_show_color.Name = "btn_show_color";
            this.btn_show_color.Size = new System.Drawing.Size(19, 114);
            this.btn_show_color.TabIndex = 1;
            // 
            // btn_ellipse
            // 
            this.btn_ellipse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_ellipse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_ellipse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ellipse.FlatAppearance.BorderSize = 0;
            this.btn_ellipse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_ellipse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_ellipse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ellipse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_ellipse.ForeColor = System.Drawing.Color.Black;
            this.btn_ellipse.Image = ((System.Drawing.Image)(resources.GetObject("btn_ellipse.Image")));
            this.btn_ellipse.Location = new System.Drawing.Point(475, 9);
            this.btn_ellipse.Name = "btn_ellipse";
            this.btn_ellipse.Size = new System.Drawing.Size(50, 50);
            this.btn_ellipse.TabIndex = 5;
            this.btn_ellipse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_ellipse.UseVisualStyleBackColor = false;
            this.btn_ellipse.Click += new System.EventHandler(this.btn_ellipse_Click);
            this.btn_ellipse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(786, 79);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown.Size = new System.Drawing.Size(44, 23);
            this.numericUpDown.TabIndex = 0;
            this.numericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown.Visible = false;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.Thickness_ValueChanged);
            this.numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Numeric_KeyDown);
            // 
            // btn_eraser
            // 
            this.btn_eraser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_eraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_eraser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_eraser.FlatAppearance.BorderSize = 0;
            this.btn_eraser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btn_eraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_eraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eraser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_eraser.ForeColor = System.Drawing.Color.Black;
            this.btn_eraser.Image = ((System.Drawing.Image)(resources.GetObject("btn_eraser.Image")));
            this.btn_eraser.Location = new System.Drawing.Point(412, 9);
            this.btn_eraser.Name = "btn_eraser";
            this.btn_eraser.Size = new System.Drawing.Size(50, 50);
            this.btn_eraser.TabIndex = 4;
            this.btn_eraser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_eraser.UseVisualStyleBackColor = false;
            this.btn_eraser.Click += new System.EventHandler(this.btn_eraser_Click);
            this.btn_eraser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyButton_KeyDown);
            // 
            // panel_lower
            // 
            this.panel_lower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_lower.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_lower.Location = new System.Drawing.Point(0, 776);
            this.panel_lower.Name = "panel_lower";
            this.panel_lower.Size = new System.Drawing.Size(1554, 27);
            this.panel_lower.TabIndex = 0;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 128);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(1554, 648);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Paint);
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            this.pic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pic_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 803);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.panel_lower);
            this.Controls.Add(this.pic_color);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BenDraw";
            this.pic_color.ResumeLayout(false);
            this.panel_backdrop.ResumeLayout(false);
            this.panel_backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_picker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pic_color;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.PictureBox color_picker;
        private System.Windows.Forms.Button btn_line;
        private System.Windows.Forms.Button btn_pencil;
        private System.Windows.Forms.Button btn_rect;
        private System.Windows.Forms.Button btn_ellipse;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Button btn_eraser;
        private System.Windows.Forms.Button btn_fill;
        private System.Windows.Forms.Panel panel_lower;
        private System.Windows.Forms.Panel panel_backdrop;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Panel btn_show_color;
        private System.Windows.Forms.Button btn_trash;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TrackBar trackBar1;
        private Components.RoundButton btn_WhiteCircle;
        private Components.RoundButton btn_BlackCircle;
    }
}

