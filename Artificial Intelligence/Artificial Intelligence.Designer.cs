namespace Artificial_Intelligence
{
    partial class Artificial_Intelligence
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Artificial_Intelligence));
            this.Output = new System.Windows.Forms.Label();
            this.Input = new System.Windows.Forms.TextBox();
            this.Avatar = new System.Windows.Forms.PictureBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.Image_Comparison = new System.Windows.Forms.PictureBox();
            this.Image2 = new System.Windows.Forms.PictureBox();
            this.Common_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Character_Converter = new System.Windows.Forms.PictureBox();
            this.GIF_Maker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image_Comparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Character_Converter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GIF_Maker)).BeginInit();
            this.SuspendLayout();
            // 
            // Output
            // 
            this.Output.BackColor = System.Drawing.Color.Transparent;
            this.Output.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Output.ForeColor = System.Drawing.Color.Cyan;
            this.Output.Location = new System.Drawing.Point(10, 7);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(1181, 585);
            this.Output.TabIndex = 2;
            this.Output.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Input
            // 
            this.Input.BackColor = System.Drawing.Color.MediumAquamarine;
            this.Input.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Input.ForeColor = System.Drawing.Color.Black;
            this.Input.Location = new System.Drawing.Point(281, 622);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(631, 36);
            this.Input.TabIndex = 1;
            this.Input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Avatar
            // 
            this.Avatar.Location = new System.Drawing.Point(0, 0);
            this.Avatar.Name = "Avatar";
            this.Avatar.Size = new System.Drawing.Size(1200, 600);
            this.Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Avatar.TabIndex = 3;
            this.Avatar.TabStop = false;
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Tick += new System.EventHandler(this.Clock_Tower);
            // 
            // WMP
            // 
            this.WMP.Enabled = true;
            this.WMP.Location = new System.Drawing.Point(572, 255);
            this.WMP.Name = "WMP";
            this.WMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMP.OcxState")));
            this.WMP.Size = new System.Drawing.Size(38, 34);
            this.WMP.TabIndex = 4;
            this.WMP.Visible = false;
            // 
            // Image1
            // 
            this.Image1.BackColor = System.Drawing.Color.Transparent;
            this.Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Image1.Location = new System.Drawing.Point(21, 110);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(366, 318);
            this.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image1.TabIndex = 5;
            this.Image1.TabStop = false;
            this.Image1.Visible = false;
            this.Image1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Image1_DragDrop);
            this.Image1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Image1_DragEnter);
            // 
            // Image_Comparison
            // 
            this.Image_Comparison.BackColor = System.Drawing.Color.Transparent;
            this.Image_Comparison.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Image_Comparison.Location = new System.Drawing.Point(405, 110);
            this.Image_Comparison.Name = "Image_Comparison";
            this.Image_Comparison.Size = new System.Drawing.Size(366, 318);
            this.Image_Comparison.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image_Comparison.TabIndex = 6;
            this.Image_Comparison.TabStop = false;
            this.Image_Comparison.Visible = false;
            // 
            // Image2
            // 
            this.Image2.BackColor = System.Drawing.Color.Transparent;
            this.Image2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Image2.Location = new System.Drawing.Point(790, 110);
            this.Image2.Name = "Image2";
            this.Image2.Size = new System.Drawing.Size(366, 318);
            this.Image2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image2.TabIndex = 7;
            this.Image2.TabStop = false;
            this.Image2.Visible = false;
            this.Image2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Image2_DragDrop);
            this.Image2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Image2_DragEnter);
            // 
            // Common_ProgressBar
            // 
            this.Common_ProgressBar.BackColor = System.Drawing.Color.Black;
            this.Common_ProgressBar.Enabled = false;
            this.Common_ProgressBar.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.Common_ProgressBar.Location = new System.Drawing.Point(21, 448);
            this.Common_ProgressBar.Name = "Common_ProgressBar";
            this.Common_ProgressBar.Size = new System.Drawing.Size(1136, 35);
            this.Common_ProgressBar.TabIndex = 8;
            this.Common_ProgressBar.Visible = false;
            // 
            // Character_Converter
            // 
            this.Character_Converter.BackColor = System.Drawing.Color.Transparent;
            this.Character_Converter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Character_Converter.Location = new System.Drawing.Point(405, 110);
            this.Character_Converter.Name = "Character_Converter";
            this.Character_Converter.Size = new System.Drawing.Size(366, 318);
            this.Character_Converter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Character_Converter.TabIndex = 9;
            this.Character_Converter.TabStop = false;
            this.Character_Converter.Visible = false;
            this.Character_Converter.DragDrop += new System.Windows.Forms.DragEventHandler(this.Character_Converter_DragDrop);
            this.Character_Converter.DragEnter += new System.Windows.Forms.DragEventHandler(this.Character_Converter_DragEnter);
            // 
            // GIF_Maker
            // 
            this.GIF_Maker.BackColor = System.Drawing.Color.Transparent;
            this.GIF_Maker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIF_Maker.Location = new System.Drawing.Point(405, 110);
            this.GIF_Maker.Name = "GIF_Maker";
            this.GIF_Maker.Size = new System.Drawing.Size(366, 318);
            this.GIF_Maker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GIF_Maker.TabIndex = 10;
            this.GIF_Maker.TabStop = false;
            this.GIF_Maker.Visible = false;
            this.GIF_Maker.DragDrop += new System.Windows.Forms.DragEventHandler(this.GIF_Maker_DragDrop);
            this.GIF_Maker.DragEnter += new System.Windows.Forms.DragEventHandler(this.GIF_Maker_DragEnter);
            // 
            // Artificial_Intelligence
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1200, 660);
            this.Controls.Add(this.GIF_Maker);
            this.Controls.Add(this.Character_Converter);
            this.Controls.Add(this.Common_ProgressBar);
            this.Controls.Add(this.Image2);
            this.Controls.Add(this.Image_Comparison);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.WMP);
            this.Controls.Add(this.Avatar);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.Output);
            this.Cursor = System.Windows.Forms.Cursors.PanNW;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Artificial_Intelligence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Artificial Intelligence";
            this.Load += new System.EventHandler(this.Artificial_Intelligence_Settings);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Artificial_Intelligence_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image_Comparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Character_Converter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GIF_Maker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.PictureBox Avatar;
        private System.Windows.Forms.Timer Timer;
        private AxWMPLib.AxWindowsMediaPlayer WMP;
        private System.Windows.Forms.PictureBox Image1;
        private System.Windows.Forms.PictureBox Image_Comparison;
        private System.Windows.Forms.PictureBox Image2;
        private System.Windows.Forms.ProgressBar Common_ProgressBar;
        private System.Windows.Forms.PictureBox Character_Converter;
        private System.Windows.Forms.PictureBox GIF_Maker;
    }
}

