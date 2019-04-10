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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.trbTimeline = new System.Windows.Forms.TrackBar();
            this.lblMaxTime = new System.Windows.Forms.Label();
            this.nudMaxTime = new System.Windows.Forms.NumericUpDown();
            this.lblTimeLine = new System.Windows.Forms.Label();
            this.grbProperties = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbxObjects = new System.Windows.Forms.ComboBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grbToolBox = new System.Windows.Forms.GroupBox();
            this.btnAddPolygon = new System.Windows.Forms.Button();
            this.btnAddFirework = new System.Windows.Forms.Button();
            this.btnAddParticle = new System.Windows.Forms.Button();
            this.nudFPS = new System.Windows.Forms.NumericUpDown();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.tbxTime = new System.Windows.Forms.TextBox();
            this.nudActualFPS = new System.Windows.Forms.NumericUpDown();
            this.grbTimeline = new System.Windows.Forms.GroupBox();
            this.lblTargetFPS = new System.Windows.Forms.Label();
            this.lblActualFPS = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbFPS = new System.Windows.Forms.GroupBox();
            this.timeline = new TimeBeam.Timeline();
            this.pnlScene = new Fireworks.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTime)).BeginInit();
            this.grbProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grbToolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualFPS)).BeginInit();
            this.grbTimeline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbFPS.SuspendLayout();
            this.SuspendLayout();
            // 
            // trbTimeline
            // 
            this.trbTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbTimeline.Location = new System.Drawing.Point(113, 35);
            this.trbTimeline.Maximum = 10000;
            this.trbTimeline.Name = "trbTimeline";
            this.trbTimeline.Size = new System.Drawing.Size(567, 45);
            this.trbTimeline.TabIndex = 1;
            this.trbTimeline.TickFrequency = 1000;
            this.trbTimeline.ValueChanged += new System.EventHandler(this.trbTime_ValueChanged);
            // 
            // lblMaxTime
            // 
            this.lblMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxTime.AutoSize = true;
            this.lblMaxTime.Location = new System.Drawing.Point(6, 19);
            this.lblMaxTime.Name = "lblMaxTime";
            this.lblMaxTime.Size = new System.Drawing.Size(49, 13);
            this.lblMaxTime.TabIndex = 3;
            this.lblMaxTime.Text = "Max time";
            // 
            // nudMaxTime
            // 
            this.nudMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudMaxTime.Location = new System.Drawing.Point(6, 35);
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
            this.lblTimeLine.Location = new System.Drawing.Point(110, 19);
            this.lblTimeLine.Name = "lblTimeLine";
            this.lblTimeLine.Size = new System.Drawing.Size(53, 13);
            this.lblTimeLine.TabIndex = 7;
            this.lblTimeLine.Text = "Time Line";
            // 
            // grbProperties
            // 
            this.grbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProperties.Controls.Add(this.splitContainer2);
            this.grbProperties.Location = new System.Drawing.Point(6, 120);
            this.grbProperties.Name = "grbProperties";
            this.grbProperties.Size = new System.Drawing.Size(356, 366);
            this.grbProperties.TabIndex = 8;
            this.grbProperties.TabStop = false;
            this.grbProperties.Text = "Properties";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer2.Panel1.Controls.Add(this.cbxObjects);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer2.Size = new System.Drawing.Size(350, 347);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(321, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(26, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "X";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cbxObjects
            // 
            this.cbxObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxObjects.FormattingEnabled = true;
            this.cbxObjects.Location = new System.Drawing.Point(0, 0);
            this.cbxObjects.Name = "cbxObjects";
            this.cbxObjects.Size = new System.Drawing.Size(315, 21);
            this.cbxObjects.TabIndex = 1;
            this.cbxObjects.SelectedIndexChanged += new System.EventHandler(this.cbxItems_SelectedIndexChanged);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(350, 318);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // grbToolBox
            // 
            this.grbToolBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbToolBox.Controls.Add(this.btnAddPolygon);
            this.grbToolBox.Controls.Add(this.btnAddFirework);
            this.grbToolBox.Controls.Add(this.btnAddParticle);
            this.grbToolBox.Location = new System.Drawing.Point(6, 3);
            this.grbToolBox.Name = "grbToolBox";
            this.grbToolBox.Size = new System.Drawing.Size(356, 111);
            this.grbToolBox.TabIndex = 9;
            this.grbToolBox.TabStop = false;
            this.grbToolBox.Text = "Tool Box";
            // 
            // btnAddPolygon
            // 
            this.btnAddPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPolygon.Location = new System.Drawing.Point(6, 78);
            this.btnAddPolygon.Name = "btnAddPolygon";
            this.btnAddPolygon.Size = new System.Drawing.Size(344, 23);
            this.btnAddPolygon.TabIndex = 2;
            this.btnAddPolygon.Text = "Add Polygon";
            this.btnAddPolygon.UseVisualStyleBackColor = true;
            this.btnAddPolygon.Click += new System.EventHandler(this.btnAddPolygon_Click);
            // 
            // btnAddFirework
            // 
            this.btnAddFirework.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFirework.Location = new System.Drawing.Point(6, 49);
            this.btnAddFirework.Name = "btnAddFirework";
            this.btnAddFirework.Size = new System.Drawing.Size(344, 23);
            this.btnAddFirework.TabIndex = 1;
            this.btnAddFirework.Text = "Add Firework";
            this.btnAddFirework.UseVisualStyleBackColor = true;
            this.btnAddFirework.Click += new System.EventHandler(this.btnAddFirework_Click);
            // 
            // btnAddParticle
            // 
            this.btnAddParticle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParticle.Location = new System.Drawing.Point(6, 20);
            this.btnAddParticle.Name = "btnAddParticle";
            this.btnAddParticle.Size = new System.Drawing.Size(344, 23);
            this.btnAddParticle.TabIndex = 0;
            this.btnAddParticle.Text = "Add Particle";
            this.btnAddParticle.UseVisualStyleBackColor = true;
            this.btnAddParticle.Click += new System.EventHandler(this.btnAddParticle_Click);
            // 
            // nudFPS
            // 
            this.nudFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudFPS.Location = new System.Drawing.Point(6, 38);
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
            this.nudFPS.Size = new System.Drawing.Size(50, 20);
            this.nudFPS.TabIndex = 11;
            this.nudFPS.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayPause.Location = new System.Drawing.Point(6, 63);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(344, 32);
            this.btnPlayPause.TabIndex = 12;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // tbxTime
            // 
            this.tbxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxTime.Location = new System.Drawing.Point(169, 16);
            this.tbxTime.Name = "tbxTime";
            this.tbxTime.Size = new System.Drawing.Size(57, 20);
            this.tbxTime.TabIndex = 13;
            this.tbxTime.Text = "0";
            this.tbxTime.TextChanged += new System.EventHandler(this.tbxTime_TextChanged);
            this.tbxTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxTime_KeyPress);
            // 
            // nudActualFPS
            // 
            this.nudActualFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudActualFPS.DecimalPlaces = 1;
            this.nudActualFPS.Location = new System.Drawing.Point(300, 38);
            this.nudActualFPS.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudActualFPS.Name = "nudActualFPS";
            this.nudActualFPS.ReadOnly = true;
            this.nudActualFPS.Size = new System.Drawing.Size(50, 20);
            this.nudActualFPS.TabIndex = 14;
            this.nudActualFPS.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // grbTimeline
            // 
            this.grbTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTimeline.Controls.Add(this.tbxTime);
            this.grbTimeline.Controls.Add(this.lblMaxTime);
            this.grbTimeline.Controls.Add(this.lblTimeLine);
            this.grbTimeline.Controls.Add(this.trbTimeline);
            this.grbTimeline.Controls.Add(this.nudMaxTime);
            this.grbTimeline.Location = new System.Drawing.Point(3, 511);
            this.grbTimeline.Name = "grbTimeline";
            this.grbTimeline.Size = new System.Drawing.Size(686, 85);
            this.grbTimeline.TabIndex = 15;
            this.grbTimeline.TabStop = false;
            this.grbTimeline.Text = "Timeline (seconds)";
            // 
            // lblTargetFPS
            // 
            this.lblTargetFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTargetFPS.Location = new System.Drawing.Point(6, 20);
            this.lblTargetFPS.Name = "lblTargetFPS";
            this.lblTargetFPS.Size = new System.Drawing.Size(49, 13);
            this.lblTargetFPS.TabIndex = 16;
            this.lblTargetFPS.Text = "Target";
            this.lblTargetFPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActualFPS
            // 
            this.lblActualFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActualFPS.Location = new System.Drawing.Point(301, 22);
            this.lblActualFPS.Name = "lblActualFPS";
            this.lblActualFPS.Size = new System.Drawing.Size(49, 13);
            this.lblActualFPS.TabIndex = 17;
            this.lblActualFPS.Text = "Actual";
            this.lblActualFPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.timeline);
            this.splitContainer1.Panel1.Controls.Add(this.pnlScene);
            this.splitContainer1.Panel1.Controls.Add(this.grbTimeline);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbFPS);
            this.splitContainer1.Panel2.Controls.Add(this.grbProperties);
            this.splitContainer1.Panel2.Controls.Add(this.grbToolBox);
            this.splitContainer1.Panel2MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(1065, 603);
            this.splitContainer1.SplitterDistance = 696;
            this.splitContainer1.TabIndex = 18;
            // 
            // grbFPS
            // 
            this.grbFPS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbFPS.Controls.Add(this.lblTargetFPS);
            this.grbFPS.Controls.Add(this.lblActualFPS);
            this.grbFPS.Controls.Add(this.nudFPS);
            this.grbFPS.Controls.Add(this.btnPlayPause);
            this.grbFPS.Controls.Add(this.nudActualFPS);
            this.grbFPS.Location = new System.Drawing.Point(6, 492);
            this.grbFPS.Name = "grbFPS";
            this.grbFPS.Size = new System.Drawing.Size(356, 104);
            this.grbFPS.TabIndex = 19;
            this.grbFPS.TabStop = false;
            this.grbFPS.Text = "Frames per second";
            // 
            // timeline
            // 
            this.timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.timeline.BackgroundColor = System.Drawing.Color.Black;
            this.timeline.GridAlpha = 40;
            this.timeline.KeyFrameBorderWidth = 2;
            this.timeline.KeyFrameWidth = 4;
            this.timeline.Location = new System.Drawing.Point(3, 340);
            this.timeline.Name = "timeline";
            this.timeline.RenderingScale = ((System.Drawing.PointF)(resources.GetObject("timeline.RenderingScale")));
            this.timeline.Size = new System.Drawing.Size(686, 165);
            this.timeline.TabIndex = 0;
            this.timeline.TimeSeconds = 0F;
            this.timeline.TrackHeight = 20;
            this.timeline.TrackSpacing = 1;
            this.timeline.ZoomBalast = 750F;
            // 
            // pnlScene
            // 
            this.pnlScene.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScene.Location = new System.Drawing.Point(3, 3);
            this.pnlScene.Name = "pnlScene";
            this.pnlScene.Size = new System.Drawing.Size(686, 332);
            this.pnlScene.TabIndex = 0;
            this.pnlScene.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlScene_Paint);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 603);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(820, 505);
            this.Name = "FormMain";
            this.Text = "Fireworks";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTime)).EndInit();
            this.grbProperties.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grbToolBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualFPS)).EndInit();
            this.grbTimeline.ResumeLayout(false);
            this.grbTimeline.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbFPS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TrackBar trbTimeline;
        private System.Windows.Forms.Label lblMaxTime;
        private System.Windows.Forms.NumericUpDown nudMaxTime;
        private System.Windows.Forms.Label lblTimeLine;
        private System.Windows.Forms.GroupBox grbProperties;
        private System.Windows.Forms.GroupBox grbToolBox;
        private System.Windows.Forms.Button btnAddParticle;
        private System.Windows.Forms.NumericUpDown nudFPS;
        private DoubleBufferedPanel pnlScene;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.TextBox tbxTime;
        private System.Windows.Forms.NumericUpDown nudActualFPS;
        private System.Windows.Forms.GroupBox grbTimeline;
        private System.Windows.Forms.Button btnAddPolygon;
        private System.Windows.Forms.Button btnAddFirework;
        private System.Windows.Forms.Label lblTargetFPS;
        private System.Windows.Forms.Label lblActualFPS;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grbFPS;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox cbxObjects;
        private System.Windows.Forms.Button btnDelete;
        private TimeBeam.Timeline timeline;
    }
}

