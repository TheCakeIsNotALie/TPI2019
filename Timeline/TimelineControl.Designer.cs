using System;
using System.Windows.Forms;
using Timeline.Scrollbar;
using System.Collections.Generic;
using KeyFrames;

namespace Timeline
{
    partial class TimelineControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScrollbarV = new Timeline.Scrollbar.VerticalScrollbar();
            this.ScrollbarH = new Timeline.Scrollbar.HorizontalScrollbar();
            this.grbChangePos = new System.Windows.Forms.GroupBox();
            this.btnDeleteKeyFrame = new System.Windows.Forms.Button();
            this.tbxChangePosY = new System.Windows.Forms.TextBox();
            this.tbxChangePosX = new System.Windows.Forms.TextBox();
            this.lblChangePosY = new System.Windows.Forms.Label();
            this.lblChangePosX = new System.Windows.Forms.Label();
            this.grbChangePos.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScrollbarV
            // 
            this.ScrollbarV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollbarV.BackgroundColor = System.Drawing.Color.Black;
            this.ScrollbarV.ForegroundColor = System.Drawing.Color.Gray;
            this.ScrollbarV.Location = new System.Drawing.Point(791, 0);
            this.ScrollbarV.Max = 100;
            this.ScrollbarV.Min = 0;
            this.ScrollbarV.Name = "ScrollbarV";
            this.ScrollbarV.Size = new System.Drawing.Size(10, 180);
            this.ScrollbarV.TabIndex = 1;
            this.ScrollbarV.Value = 0;
            this.ScrollbarV.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.ScrollbarVScroll);
            // 
            // ScrollbarH
            // 
            this.ScrollbarH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollbarH.BackgroundColor = System.Drawing.Color.Black;
            this.ScrollbarH.ForegroundColor = System.Drawing.Color.Gray;
            this.ScrollbarH.Location = new System.Drawing.Point(0, 190);
            this.ScrollbarH.Max = 100;
            this.ScrollbarH.Min = 0;
            this.ScrollbarH.Name = "ScrollbarH";
            this.ScrollbarH.Size = new System.Drawing.Size(780, 10);
            this.ScrollbarH.TabIndex = 0;
            this.ScrollbarH.Value = 0;
            this.ScrollbarH.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.ScrollbarHScroll);
            // 
            // grbChangePos
            // 
            this.grbChangePos.BackColor = System.Drawing.Color.Transparent;
            this.grbChangePos.Controls.Add(this.btnDeleteKeyFrame);
            this.grbChangePos.Controls.Add(this.tbxChangePosY);
            this.grbChangePos.Controls.Add(this.tbxChangePosX);
            this.grbChangePos.Controls.Add(this.lblChangePosY);
            this.grbChangePos.Controls.Add(this.lblChangePosX);
            this.grbChangePos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.grbChangePos.Location = new System.Drawing.Point(341, 60);
            this.grbChangePos.Name = "grbChangePos";
            this.grbChangePos.Size = new System.Drawing.Size(138, 73);
            this.grbChangePos.TabIndex = 2;
            this.grbChangePos.TabStop = false;
            this.grbChangePos.Text = "KeyFrame";
            this.grbChangePos.Visible = false;
            // 
            // btnDeleteKeyFrame
            // 
            this.btnDeleteKeyFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteKeyFrame.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteKeyFrame.Location = new System.Drawing.Point(99, 20);
            this.btnDeleteKeyFrame.Name = "btnDeleteKeyFrame";
            this.btnDeleteKeyFrame.Size = new System.Drawing.Size(33, 40);
            this.btnDeleteKeyFrame.TabIndex = 4;
            this.btnDeleteKeyFrame.Text = "X";
            this.btnDeleteKeyFrame.UseVisualStyleBackColor = true;
            this.btnDeleteKeyFrame.Click += new System.EventHandler(this.BtnDeleteKeyFrame_Click);
            // 
            // tbxChangePosY
            // 
            this.tbxChangePosY.Location = new System.Drawing.Point(27, 43);
            this.tbxChangePosY.Name = "tbxChangePosY";
            this.tbxChangePosY.Size = new System.Drawing.Size(66, 20);
            this.tbxChangePosY.TabIndex = 3;
            this.tbxChangePosY.TextChanged += new System.EventHandler(this.TbxChangePosY_TextChanged);
            this.tbxChangePosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbxPosition_KeyPress);
            // 
            // tbxChangePosX
            // 
            this.tbxChangePosX.Location = new System.Drawing.Point(27, 17);
            this.tbxChangePosX.Name = "tbxChangePosX";
            this.tbxChangePosX.Size = new System.Drawing.Size(66, 20);
            this.tbxChangePosX.TabIndex = 2;
            this.tbxChangePosX.TextChanged += new System.EventHandler(this.TbxChangePosX_TextChanged);
            this.tbxChangePosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbxPosition_KeyPress);
            // 
            // lblChangePosY
            // 
            this.lblChangePosY.AutoSize = true;
            this.lblChangePosY.Location = new System.Drawing.Point(6, 47);
            this.lblChangePosY.Name = "lblChangePosY";
            this.lblChangePosY.Size = new System.Drawing.Size(14, 13);
            this.lblChangePosY.TabIndex = 1;
            this.lblChangePosY.Text = "Y";
            // 
            // lblChangePosX
            // 
            this.lblChangePosX.AutoSize = true;
            this.lblChangePosX.Location = new System.Drawing.Point(7, 20);
            this.lblChangePosX.Name = "lblChangePosX";
            this.lblChangePosX.Size = new System.Drawing.Size(14, 13);
            this.lblChangePosX.TabIndex = 0;
            this.lblChangePosX.Text = "X";
            // 
            // Timeline
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.grbChangePos);
            this.Controls.Add(this.ScrollbarV);
            this.Controls.Add(this.ScrollbarH);
            this.DoubleBuffered = true;
            this.Name = "Timeline";
            this.Size = new System.Drawing.Size(800, 200);
            this.grbChangePos.ResumeLayout(false);
            this.grbChangePos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HorizontalScrollbar ScrollbarH;
        private VerticalScrollbar ScrollbarV;
        private GroupBox grbChangePos;
        private Label lblChangePosY;
        private Label lblChangePosX;
        private TextBox tbxChangePosY;
        private TextBox tbxChangePosX;
        private Button btnDeleteKeyFrame;
    }
}
