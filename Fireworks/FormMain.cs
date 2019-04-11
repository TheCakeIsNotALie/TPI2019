using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timeline;

namespace Fireworks
{
    /// <summary>
    /// Main view of application
    /// </summary>
    public partial class FormMain : Form
    {
        private const float MS_IN_SEC = 1000;
        private const int DRAWING_CALL_DELAY_MS = 1;
        //StopWatch for frame verification
        private Stopwatch _frameStopWatch = new Stopwatch();
        private bool _animate = false;
        private Scene _scene;

        /// <summary>
        /// Creates a new instance of FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load event
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            _scene = new Scene(new Size(pnlScene.Width, pnlScene.Height));
            _scene.SelectedObjectChanged += SelectedObjectChanged;
            _scene.AnimatedObjectsChanged += AnimatedObjectsChanged;

            timeline.SelectionModified += Timeline_SelectionModified;
            timeline.TimeChangedFromInput += Timeline_TimeChangedFromInput;
            timeline.SelectionDeleted += Timeline_SelectionDeleted;
            timeline.TrackSelected += Timeline_TrackSelected;

            //List<IKeyFrame> tmpKeyFrames = new List<IKeyFrame>();
            //tmpKeyFrames.Add(new KeyFrame(new PointF(0, 0), 1));
            //tmpKeyFrames.Add(new KeyFrame(new PointF(20, 10), 2));
            //tmpKeyFrames.Add(new KeyFrame(new PointF(200, 50), 4));

            //Particle p = new Particle("Particle1", Brushes.Red, tmpKeyFrames, new Size(3, 3), 0);
            //Firework f = new Firework("Firework1", Brushes.Red, new KeyFrame(new PointF(100, 120), 3.3f), 100, 70, 10, 2);
            //Firework f2 = new Firework("Firework2", Brushes.Red, new KeyFrame(new PointF(150, 100), 3.1f), 50, 20, 10, 1);
            //Firework f3 = new Firework("Firework3", Brushes.Red, new KeyFrame(new PointF(110, 110), 3.2f), 80, 50, 10, 3);
            //Firework f4 = new Firework("Firework4", Brushes.Red, new KeyFrame(new PointF(150, 156), 4.1f), 50, 20, 10, 1);
            //Firework f5 = new Firework("Firework5", Brushes.Red, new KeyFrame(new PointF(110, 110), 7.2f), 80, 50, 10, 3);
            //Firework f6 = new Firework("Firework6", Brushes.Red, new KeyFrame(new PointF(250, 200), 5.1f), 50, 20, 10, 1);
            //Firework f7 = new Firework("Firework7", Brushes.Red, new KeyFrame(new PointF(210, 110), 1.2f), 80, 50, 10, 3);
            //Firework f8 = new Firework("Firework8", Brushes.Red, new KeyFrame(new PointF(170, 100), 2.1f), 50, 20, 10, 1);
            //Firework f9 = new Firework("Firework9", Brushes.Red, new KeyFrame(new PointF(111, 110), 9.2f), 80, 50, 10, 3);

            //List<IKeyFrame> tmpKeyFrames2 = new List<IKeyFrame>();
            //tmpKeyFrames2.Add(new KeyFrame(new PointF(50, 50), 1));
            //tmpKeyFrames2.Add(new KeyFrame(new PointF(100, 100), 2));
            //tmpKeyFrames2.Add(new KeyFrame(new PointF(200, 20), 4));

            //PointF[] polygonCorners = { new PointF(5, 5), new PointF(-5, 5), new PointF(-5, -5), new PointF(12, -5), new PointF(12, 12) };

            //po = new Polygon("Polygon1", tmpKeyFrames2, polygonCorners, 50);

            //propertyGrid.SelectedObject = po;

            //_scene.AddAnimatedObject(p);
            //_scene.AddAnimatedObject(f);
            //_scene.AddAnimatedObject(f2);
            //_scene.AddAnimatedObject(f3);
            //_scene.AddAnimatedObject(f4);
            //_scene.AddAnimatedObject(f5);
            //_scene.AddAnimatedObject(f6);
            //_scene.AddAnimatedObject(f7);
            //_scene.AddAnimatedObject(f8);
            //_scene.AddAnimatedObject(f9);
            //_scene.AddAnimatedObject(po);

            UpdateTimeLineItems();
        }

        /// <summary>
        /// User clicked on track label
        /// </summary>
        private void Timeline_TrackSelected(object sender, Timeline.Events.TrackSelectionEventsArgs eventsArgs)
        {
            _scene.SelectedObject = eventsArgs.SelectedTrack as AnimatedObject;
        }

        /// <summary>
        /// User deleted the selection
        /// </summary>
        private void Timeline_SelectionDeleted(object sender, Timeline.Events.SelectionDeletedEventArgs eventArgs)
        {
            pnlScene.Invalidate();
        }

        /// <summary>
        /// User changed time
        /// </summary>
        private void Timeline_TimeChangedFromInput(object sender, EventArgs e)
        {
            _scene.Time = timeline.TimeSeconds;
            pnlScene.Invalidate();
        }

        /// <summary>
        /// Selection of keyframes changed
        /// </summary>
        private void Timeline_SelectionModified(object sender, Timeline.Events.SelectionModifiedEventArgs eventArgs)
        {
            foreach (AnimatedObject o in eventArgs.ModifiedTracks)
            {
                o.Update();
            }
            pnlScene.Invalidate();
        }

        /// <summary>
        /// List of objects changed
        /// </summary>
        private void AnimatedObjectsChanged(object sender, EventArgs e)
        {
            UpdateTimeLineItems();
        }

        /// <summary>
        /// Whenever the selected object in scene is changed change the selected object in view
        /// </summary>
        public void SelectedObjectChanged(object sender, EventArgs e)
        {
            UpdateTimeLineItems();
            //update properties grid
            propertyGrid.SelectedObject = _scene.SelectedObject;
            //redraw frame
            pnlScene.Invalidate();
        }
        
        /// <summary>
        /// Updates items in timeline
        /// </summary>
        private void UpdateTimeLineItems()
        {
            timeline.Tracks = _scene.AnimatedObjects.Cast<ITimelineTrack>().ToList();
            timeline.Invalidate();
        }

        /// <summary>
        /// Drawing of the frame at time of scene
        /// </summary>
        private void pnlScene_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_scene.GetFrame(), new Point(0, 0));
        }

        /// <summary>
        /// Async task that will update the drawing of frames
        /// </summary>
        private async Task FrameUpdateCall()
        {
            while (_animate)
            {
                //if enough time sinc last frame draw has passed
                if (_frameStopWatch.ElapsedMilliseconds > MS_IN_SEC / (float)nudFPS.Value)
                {
                    //Store the elapsedMs for it not to change in calculations
                    int elapsedMs = (int)_frameStopWatch.ElapsedMilliseconds;

                    _scene.Time += elapsedMs / MS_IN_SEC;
                    //restart timer now to take in account drawing time in next frame draw
                    _frameStopWatch.Restart();

                    pnlScene.Invalidate();

                    //Calculate real fps value
                    nudActualFPS.Value = (decimal)(MS_IN_SEC / elapsedMs);

                    //Check if max time has been passed
                    if (_scene.Time > _scene.MaxTime)
                    {
                        //max out time
                        _scene.Time = _scene.MaxTime;
                        StopAnimation();
                    }
                    //Change view
                    timeline.TimeSeconds = _scene.Time;
                }
                else if (!_frameStopWatch.IsRunning)
                {
                    //start stopwatch if it wasn't started before
                    _frameStopWatch.Start();
                }
                await Task.Delay(DRAWING_CALL_DELAY_MS);
            }
            _frameStopWatch.Reset();
        }

        /// <summary>
        /// Starts playing the animation
        /// </summary>
        private void StartAnimation()
        {
            btnPlayPause.Text = "Pause";
            _animate = true;
            FrameUpdateCall();
        }

        /// <summary>
        /// Stops the animation from playing
        /// </summary>
        private void StopAnimation()
        {
            btnPlayPause.Text = "Play";
            _animate = false;
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            //Toggle state
            if (_animate)
                StopAnimation();
            else
                StartAnimation();
        }

        /// <summary>
        /// Add a new particle to the scene
        /// </summary>
        private void btnAddParticle_Click(object sender, EventArgs e)
        {
            _scene.AddAnimatedObject(new Particle());
        }

        /// <summary>
        /// Add a new firework to the scene
        /// </summary>
        private void btnAddFirework_Click(object sender, EventArgs e)
        {
            _scene.AddAnimatedObject(new Firework());
        }

        /// <summary>
        /// Add a new polygon to the scene
        /// </summary>
        private void btnAddPolygon_Click(object sender, EventArgs e)
        {
            _scene.AddAnimatedObject(new Polygon());
        }

        /// <summary>
        /// User changed value of selected object
        /// </summary>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ((AnimatedObject)propertyGrid.SelectedObject).Update();
            pnlScene.Invalidate();
        }

        /// <summary>
        /// User clicked on delete
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete that object ?",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _scene.RemoveAnimatedObject(_scene.SelectedObject);
                _scene.SelectedObject = null;
            }
        }
    }
}
