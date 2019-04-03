namespace Fireworks
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trbTimeline = new System.Windows.Forms.TrackBar();
            this.lblMaxTime = new System.Windows.Forms.Label();
            this.nudMaxTime = new System.Windows.Forms.NumericUpDown();
            this.lblTimeLine = new System.Windows.Forms.Label();
            this.grbProperties = new System.Windows.Forms.GroupBox();
            this.grbToolBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.nudFPS = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.tmrAnimate = new System.Windows.Forms.Timer(this.components);
            this.tbxTime = new System.Windows.Forms.TextBox();
            this.pnlScene = new Fireworks.DoubleBufferedPanel();
            this.nudActualFPS = new System.Windows.Forms.NumericUpDown();
            this.grbTimeline = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTime)).BeginInit();
            this.grbToolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualFPS)).BeginInit();
            this.grbTimeline.SuspendLayout();
            this.SuspendLayout();
            // 
            // trbTimeline
            // 
            this.trbTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbTimeline.Location = new System.Drawing.Point(113, 32);
            this.trbTimeline.Maximum = 10000;
            this.trbTimeline.Name = "trbTimeline";
            this.trbTimeline.Size = new System.Drawing.Size(528, 45);
            this.trbTimeline.TabIndex = 1;
            this.trbTimeline.TickFrequency = 1000;
            this.trbTimeline.ValueChanged += new System.EventHandler(this.trbTime_ValueChanged);
            // 
            // lblMaxTime
            // 
            this.lblMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxTime.AutoSize = true;
            this.lblMaxTime.Location = new System.Drawing.Point(6, 16);
            this.lblMaxTime.Name = "lblMaxTime";
            this.lblMaxTime.Size = new System.Drawing.Size(49, 13);
            this.lblMaxTime.TabIndex = 3;
            this.lblMaxTime.Text = "Max time";
            // 
            // nudMaxTime
            // 
            this.nudMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudMaxTime.Location = new System.Drawing.Point(6, 32);
            this.nudMaxTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxTime.Name = "nudMaxTime";
            this.nudMaxTime.Size = new System.Drawing.Size(100, 20);
            this.nudMaxTime.TabIndex = 4;
            this.nudMaxTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMaxTime.ValueChanged += new System.EventHandler(this.SliderParameterChanged);
            // 
            // lblTimeLine
            // 
            this.lblTimeLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeLine.AutoSize = true;
            this.lblTimeLine.Location = new System.Drawing.Point(110, 16);
            this.lblTimeLine.Name = "lblTimeLine";
            this.lblTimeLine.Size = new System.Drawing.Size(53, 13);
            this.lblTimeLine.TabIndex = 7;
            this.lblTimeLine.Text = "Time Line";
            // 
            // grbProperties
            // 
            this.grbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProperties.Location = new System.Drawing.Point(665, 157);
            this.grbProperties.Name = "grbProperties";
            this.grbProperties.Size = new System.Drawing.Size(127, 219);
            this.grbProperties.TabIndex = 8;
            this.grbProperties.TabStop = false;
            this.grbProperties.Text = "Properties";
            // 
            // grbToolBox
            // 
            this.grbToolBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbToolBox.Controls.Add(this.button1);
            this.grbToolBox.Location = new System.Drawing.Point(665, 13);
            this.grbToolBox.Name = "grbToolBox";
            this.grbToolBox.Size = new System.Drawing.Size(127, 135);
            this.grbToolBox.TabIndex = 9;
            this.grbToolBox.TabStop = false;
            this.grbToolBox.Text = "Tool Box";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Particle";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // nudFPS
            // 
            this.nudFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFPS.Location = new System.Drawing.Point(664, 395);
            this.nudFPS.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudFPS.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFPS.Name = "nudFPS";
            this.nudFPS.Size = new System.Drawing.Size(63, 20);
            this.nudFPS.TabIndex = 11;
            this.nudFPS.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudFPS.ValueChanged += new System.EventHandler(this.nudFPS_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(661, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Frames per second";
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayPause.Location = new System.Drawing.Point(664, 422);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(130, 32);
            this.btnPlayPause.TabIndex = 12;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // tmrAnimate
            // 
            this.tmrAnimate.Tick += new System.EventHandler(this.tmrAnimate_Tick);
            // 
            // tbxTime
            // 
            this.tbxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxTime.Location = new System.Drawing.Point(169, 14);
            this.tbxTime.Name = "tbxTime";
            this.tbxTime.Size = new System.Drawing.Size(87, 20);
            this.tbxTime.TabIndex = 13;
            this.tbxTime.Text = "0";
            this.tbxTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxTime.TextChanged += new System.EventHandler(this.tbxTime_TextChanged);
            this.tbxTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxTime_KeyPress);
            // 
            // pnlScene
            // 
            this.pnlScene.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScene.Location = new System.Drawing.Point(12, 13);
            this.pnlScene.Name = "pnlScene";
            this.pnlScene.Size = new System.Drawing.Size(646, 363);
            this.pnlScene.TabIndex = 0;
            this.pnlScene.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlScene_Paint);
            // 
            // nudActualFPS
            // 
            this.nudActualFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudActualFPS.DecimalPlaces = 2;
            this.nudActualFPS.Location = new System.Drawing.Point(731, 396);
            this.nudActualFPS.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudActualFPS.Name = "nudActualFPS";
            this.nudActualFPS.ReadOnly = true;
            this.nudActualFPS.Size = new System.Drawing.Size(63, 20);
            this.nudActualFPS.TabIndex = 14;
            this.nudActualFPS.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // grbTimeline
            // 
            this.grbTimeline.Controls.Add(this.tbxTime);
            this.grbTimeline.Controls.Add(this.lblMaxTime);
            this.grbTimeline.Controls.Add(this.lblTimeLine);
            this.grbTimeline.Controls.Add(this.trbTimeline);
            this.grbTimeline.Controls.Add(this.nudMaxTime);
            this.grbTimeline.Location = new System.Drawing.Point(12, 382);
            this.grbTimeline.Name = "grbTimeline";
            this.grbTimeline.Size = new System.Drawing.Size(646, 72);
            this.grbTimeline.TabIndex = 15;
            this.grbTimeline.TabStop = false;
            this.grbTimeline.Text = "Timeline (seconds)";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 466);
            this.Controls.Add(this.grbTimeline);
            this.Controls.Add(this.nudActualFPS);
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.nudFPS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grbToolBox);
            this.Controls.Add(this.grbProperties);
            this.Controls.Add(this.pnlScene);
            this.MinimumSize = new System.Drawing.Size(820, 505);
            this.Name = "FormMain";
            this.Text = "Fireworks";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTime)).EndInit();
            this.grbToolBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualFPS)).EndInit();
            this.grbTimeline.ResumeLayout(false);
            this.grbTimeline.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trbTimeline;
        private System.Windows.Forms.Label lblMaxTime;
        private System.Windows.Forms.NumericUpDown nudMaxTime;
        private System.Windows.Forms.Label lblTimeLine;
        private System.Windows.Forms.GroupBox grbProperties;
        private System.Windows.Forms.GroupBox grbToolBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudFPS;
        private System.Windows.Forms.Label label1;
        private DoubleBufferedPanel pnlScene;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Timer tmrAnimate;
        private System.Windows.Forms.TextBox tbxTime;
        private System.Windows.Forms.NumericUpDown nudActualFPS;
        private System.Windows.Forms.GroupBox grbTimeline;
    }
}

