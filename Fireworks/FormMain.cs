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
        /// <summary>
        /// Amount of milliseconds in a second
        /// </summary>
        private const float MS_IN_SEC = 1000;

        /// <summary>
        /// Delay for async draw call
        /// </summary>
        private const int DRAWING_CALL_DELAY_MS = 1;

        /// <summary>
        /// StopWatch for frame verification
        /// </summary>
        private Stopwatch _frameStopWatch = new Stopwatch();

        /// <summary>
        /// Flag letting the program know if we want to animate the scene
        /// </summary>
        private bool _animate = false;

        /// <summary>
        /// Scene containing the animated objects inserted by user
        /// </summary>
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
            //
            _scene = new Scene(new Size(pnlScene.Width, pnlScene.Height));
            _scene.SelectedObjectChanged += SelectedObjectChanged;
            _scene.AnimatedObjectsChanged += AnimatedObjectsChanged;

            //Add event listener for timeline events
            timeline.SelectionModified += Timeline_SelectionModified;
            timeline.TimeChangedFromInput += Timeline_TimeChangedFromInput;
            timeline.SelectionDeleted += Timeline_SelectionDeleted;
            timeline.TrackSelected += Timeline_TrackSelected;

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
                _scene.ForceAnimatedObjectUpdate(o);
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
            timeline.SetTracks(_scene.AnimatedObjects.Cast<ITimelineTrack>().ToList());
            timeline.Invalidate();
        }

        /// <summary>
        /// Updates the scene size
        /// </summary>
        private void UpdateSceneSize()
        {
            _scene.Size = pnlScene.Size;
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
            _scene.ForceAnimatedObjectUpdate(_scene.SelectedObject);
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

        /// <summary>
        /// Show selected object checkbox has changed
        /// </summary>
        private void cbxShowSelectedObject_CheckedChanged(object sender, EventArgs e)
        {
            //Change the way we want to draw object
            _scene.PaintDebugSelectedObject = cbxShowSelectedObject.Checked;
            //Redraw scene
            pnlScene.Invalidate();
        }

        /// <summary>
        /// Whenever the panel is resized
        /// </summary>
        private void pnlScene_Resize(object sender, EventArgs e)
        {
            UpdateSceneSize();
        }
    }
}
