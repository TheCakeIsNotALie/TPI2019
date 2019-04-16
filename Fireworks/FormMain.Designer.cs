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
            this.grbProperties = new System.Windows.Forms.GroupBox();
            this.cbxShowSelectedObject = new System.Windows.Forms.CheckBox();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grbToolBox = new System.Windows.Forms.GroupBox();
            this.btnAddPolygon = new System.Windows.Forms.Button();
            this.btnAddFirework = new System.Windows.Forms.Button();
            this.btnAddParticle = new System.Windows.Forms.Button();
            this.nudFPS = new System.Windows.Forms.NumericUpDown();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.nudActualFPS = new System.Windows.Forms.NumericUpDown();
            this.grbTimeline = new System.Windows.Forms.GroupBox();
            this.timeline = new Timeline.TimelineControl();
            this.lblTargetFPS = new System.Windows.Forms.Label();
            this.lblActualFPS = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlScene = new Fireworks.DoubleBufferedPanel();
            this.grbFPS = new System.Windows.Forms.GroupBox();
            this.grbProperties.SuspendLayout();
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
            // grbProperties
            // 
            this.grbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProperties.Controls.Add(this.cbxShowSelectedObject);
            this.grbProperties.Controls.Add(this.btnDeleteObject);
            this.grbProperties.Controls.Add(this.propertyGrid);
            this.grbProperties.Location = new System.Drawing.Point(6, 120);
            this.grbProperties.Name = "grbProperties";
            this.grbProperties.Size = new System.Drawing.Size(326, 320);
            this.grbProperties.TabIndex = 8;
            this.grbProperties.TabStop = false;
            this.grbProperties.Text = "Properties";
            // 
            // cbxShowSelectedObject
            // 
            this.cbxShowSelectedObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxShowSelectedObject.AutoSize = true;
            this.cbxShowSelectedObject.Location = new System.Drawing.Point(147, 19);
            this.cbxShowSelectedObject.Name = "cbxShowSelectedObject";
            this.cbxShowSelectedObject.Size = new System.Drawing.Size(173, 17);
            this.cbxShowSelectedObject.TabIndex = 3;
            this.cbxShowSelectedObject.Text = "Paint debug for selected object";
            this.cbxShowSelectedObject.UseVisualStyleBackColor = true;
            this.cbxShowSelectedObject.CheckedChanged += new System.EventHandler(this.CbxShowSelectedObject_CheckedChanged);
            // 
            // btnDeleteObject
            // 
            this.btnDeleteObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteObject.Location = new System.Drawing.Point(6, 291);
            this.btnDeleteObject.Name = "btnDeleteObject";
            this.btnDeleteObject.Size = new System.Drawing.Size(313, 23);
            this.btnDeleteObject.TabIndex = 2;
            this.btnDeleteObject.Text = "Delete object";
            this.btnDeleteObject.UseVisualStyleBackColor = true;
            this.btnDeleteObject.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(320, 301);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
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
            this.grbToolBox.Size = new System.Drawing.Size(326, 111);
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
            this.btnAddPolygon.Size = new System.Drawing.Size(314, 23);
            this.btnAddPolygon.TabIndex = 2;
            this.btnAddPolygon.Text = "Add Polygon";
            this.btnAddPolygon.UseVisualStyleBackColor = true;
            this.btnAddPolygon.Click += new System.EventHandler(this.BtnAddPolygon_Click);
            // 
            // btnAddFirework
            // 
            this.btnAddFirework.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFirework.Location = new System.Drawing.Point(6, 49);
            this.btnAddFirework.Name = "btnAddFirework";
            this.btnAddFirework.Size = new System.Drawing.Size(314, 23);
            this.btnAddFirework.TabIndex = 1;
            this.btnAddFirework.Text = "Add Firework";
            this.btnAddFirework.UseVisualStyleBackColor = true;
            this.btnAddFirework.Click += new System.EventHandler(this.BtnAddFirework_Click);
            // 
            // btnAddParticle
            // 
            this.btnAddParticle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParticle.Location = new System.Drawing.Point(6, 20);
            this.btnAddParticle.Name = "btnAddParticle";
            this.btnAddParticle.Size = new System.Drawing.Size(314, 23);
            this.btnAddParticle.TabIndex = 0;
            this.btnAddParticle.Text = "Add Particle";
            this.btnAddParticle.UseVisualStyleBackColor = true;
            this.btnAddParticle.Click += new System.EventHandler(this.BtnAddParticle_Click);
            // 
            // nudFPS
            // 
            this.nudFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudFPS.Location = new System.Drawing.Point(6, 36);
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
            this.btnPlayPause.Location = new System.Drawing.Point(6, 64);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(308, 32);
            this.btnPlayPause.TabIndex = 12;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
            // 
            // nudActualFPS
            // 
            this.nudActualFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudActualFPS.DecimalPlaces = 1;
            this.nudActualFPS.Location = new System.Drawing.Point(264, 34);
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
            this.grbTimeline.Controls.Add(this.timeline);
            this.grbTimeline.Location = new System.Drawing.Point(10, 352);
            this.grbTimeline.Name = "grbTimeline";
            this.grbTimeline.Size = new System.Drawing.Size(723, 196);
            this.grbTimeline.TabIndex = 15;
            this.grbTimeline.TabStop = false;
            this.grbTimeline.Text = "Timeline (seconds)";
            // 
            // timeline
            // 
            this.timeline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.timeline.BackgroundColor = System.Drawing.Color.Black;
            this.timeline.GridAlpha = 40;
            this.timeline.KeyFrameBorderWidth = 1;
            this.timeline.KeyFrameWidth = 4;
            this.timeline.Location = new System.Drawing.Point(6, 19);
            this.timeline.Name = "timeline";
            this.timeline.RenderingScale = ((System.Drawing.PointF)(resources.GetObject("timeline.RenderingScale")));
            this.timeline.Size = new System.Drawing.Size(711, 171);
            this.timeline.TabIndex = 16;
            this.timeline.TimeSeconds = 0F;
            this.timeline.TrackHeight = 20;
            this.timeline.TrackSpacing = 1;
            this.timeline.ZoomBalast = 750F;
            // 
            // lblTargetFPS
            // 
            this.lblTargetFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTargetFPS.Location = new System.Drawing.Point(6, 18);
            this.lblTargetFPS.Name = "lblTargetFPS";
            this.lblTargetFPS.Size = new System.Drawing.Size(49, 13);
            this.lblTargetFPS.TabIndex = 16;
            this.lblTargetFPS.Text = "Target";
            this.lblTargetFPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActualFPS
            // 
            this.lblActualFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActualFPS.Location = new System.Drawing.Point(265, 18);
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
            this.splitContainer1.Size = new System.Drawing.Size(1083, 555);
            this.splitContainer1.SplitterDistance = 740;
            this.splitContainer1.TabIndex = 18;
            // 
            // pnlScene
            // 
            this.pnlScene.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScene.Location = new System.Drawing.Point(10, 10);
            this.pnlScene.Name = "pnlScene";
            this.pnlScene.Size = new System.Drawing.Size(723, 336);
            this.pnlScene.TabIndex = 16;
            this.pnlScene.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlScene_Paint);
            this.pnlScene.Resize += new System.EventHandler(this.PnlScene_Resize);
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
            this.grbFPS.Location = new System.Drawing.Point(12, 446);
            this.grbFPS.Name = "grbFPS";
            this.grbFPS.Size = new System.Drawing.Size(320, 102);
            this.grbFPS.TabIndex = 19;
            this.grbFPS.TabStop = false;
            this.grbFPS.Text = "Frames per second";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 555);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(820, 505);
            this.Name = "FormMain";
            this.Text = "Fireworks";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.grbProperties.ResumeLayout(false);
            this.grbProperties.PerformLayout();
            this.grbToolBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualFPS)).EndInit();
            this.grbTimeline.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbFPS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grbProperties;
        private System.Windows.Forms.GroupBox grbToolBox;
        private System.Windows.Forms.Button btnAddParticle;
        private System.Windows.Forms.NumericUpDown nudFPS;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.NumericUpDown nudActualFPS;
        private System.Windows.Forms.GroupBox grbTimeline;
        private System.Windows.Forms.Button btnAddPolygon;
        private System.Windows.Forms.Button btnAddFirework;
        private System.Windows.Forms.Label lblTargetFPS;
        private System.Windows.Forms.Label lblActualFPS;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grbFPS;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Timeline.TimelineControl timeline;
        private DoubleBufferedPanel pnlScene;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.CheckBox cbxShowSelectedObject;
    }
}

