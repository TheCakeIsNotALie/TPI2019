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

namespace Fireworks
{
    public partial class FormMain : Form
    {
        private const float MS_IN_SEC = 1000;
        private const int DRAWING_CALL_DELAY_MS = 1;
        //StopWatch for frame verification
        private Stopwatch _frameStopWatch;
        private bool _animate = false;
        private Scene _scene;
        private Polygon po;

        /// <summary>
        /// Creates new FormMain
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
            _frameStopWatch = new Stopwatch();
            _scene = new Scene(new Size(pnlScene.Width, pnlScene.Height));

            //List<KeyFrame> tmpKeyFrames = new List<KeyFrame>();
            //tmpKeyFrames.Add(new KeyFrame(new PointF(0, 0), 1));
            //tmpKeyFrames.Add(new KeyFrame(new PointF(20, 10), 2));
            //tmpKeyFrames.Add(new KeyFrame(new PointF(200, 50), 4));

            //Particle p = new Particle(tmpKeyFrames, new Size(3, 3), 0);
            //Firework f = new Firework(new KeyFrame(new PointF(100, 120), 3.3f), 100, 70, 10, 2);
            //Firework f2 = new Firework(new KeyFrame(new PointF(150, 100), 3.1f), 50, 20, 10, 1);
            //Firework f3 = new Firework(new KeyFrame(new PointF(110, 110), 3.2f), 80, 50, 10, 3);
            //Firework f4 = new Firework(new KeyFrame(new PointF(150, 156), 4.1f), 50, 20, 10, 1);
            //Firework f5 = new Firework(new KeyFrame(new PointF(110, 110), 7.2f), 80, 50, 10, 3);
            //Firework f6 = new Firework(new KeyFrame(new PointF(250, 200), 5.1f), 50, 20, 10, 1);
            //Firework f7 = new Firework(new KeyFrame(new PointF(210, 110), 1.2f), 80, 50, 10, 3);
            //Firework f8 = new Firework(new KeyFrame(new PointF(170, 100), 2.1f), 50, 20, 10, 1);
            //Firework f9 = new Firework(new KeyFrame(new PointF(111, 110), 9.2f), 80, 50, 10, 3);

            List<KeyFrame> tmpKeyFrames2 = new List<KeyFrame>();
            tmpKeyFrames2.Add(new KeyFrame(new PointF(50, 50), 1));
            tmpKeyFrames2.Add(new KeyFrame(new PointF(100, 100), 2));
            tmpKeyFrames2.Add(new KeyFrame(new PointF(200, 20), 4));

            PointF[] polygonCorners = { new PointF(5, 5), new PointF(-5, 5), new PointF(-5, -5), new PointF(12, -5), new PointF(12, 12) };

            po = new Polygon(tmpKeyFrames2, polygonCorners, 50);

            propertyGrid.SelectedObject = po;

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
            _scene.AddAnimatedObject(po);
        }

        /// <summary>
        /// Event for when user changes max time 
        /// </summary>
        private void SliderParameterChanged(object sender, EventArgs e)
        {
            //Change the maximum value of trackbar
            trbTimeline.Maximum = (int)nudMaxTime.Value * (int)MS_IN_SEC; //seconds to ms
        }

        /// <summary>
        /// When the slider value changes
        /// </summary>
        private void trbTime_ValueChanged(object sender, EventArgs e)
        {
            tbxTime.Text = Convert.ToString(trbTimeline.Value / MS_IN_SEC);
            pnlScene.Invalidate();
            //Change time when moving slider by hand
            if (!_animate)
            {
                _scene.Time = trbTimeline.Value / MS_IN_SEC;
            }
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

                    //Update slider value
                    if (!ChangeSliderTime((int)(_scene.Time * MS_IN_SEC)))
                    {
                        _scene.Time = trbTimeline.Maximum;
                        StopAnimation();
                    }
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

        private void StartAnimation()
        {
            btnPlayPause.Text = "Pause";
            _animate = true;
            FrameUpdateCall();
        }

        private void StopAnimation()
        {
            btnPlayPause.Text = "Play";
            _animate = false;
        }

        /// <summary>
        /// Go to time in timeline
        /// </summary>
        /// <param name="time">Absolute time in ms</param>
        /// <returns>If the time was changed correctly</returns>
        private bool ChangeSliderTime(int time)
        {
            //Error catching and animation stopping
            if (!(time > trbTimeline.Maximum || time < trbTimeline.Minimum))
            {
                trbTimeline.Value = time;
                return true;
            }
            return false;
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            //Toggle state
            if (_animate)
                StopAnimation();
            else
                StartAnimation();
        }

        private void tbxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Accept only numbers, comma or backspace in textbox
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Event for when user changes the time manually
        /// </summary>
        private void tbxTime_TextChanged(object sender, EventArgs e)
        {
            //Change value of slider based on textbox value
            try
            {
                int newSliderValue = (int)(Convert.ToSingle(tbxTime.Text) * MS_IN_SEC);

                //Change time of slider
                if (ChangeSliderTime(newSliderValue))
                {
                    //clear visual feedback
                    tbxTime.BackColor = SystemColors.Window;
                }
                else //if changing time fails fails
                {
                    //Visual feedback for user to see that something is wrong
                    tbxTime.BackColor = Color.Red;
                }
            }
            catch (Exception ex) //Catch exception of Convert
            {
                //Visual feedback for user to see that something is wrong
                tbxTime.BackColor = Color.Red;
            }
        }
    }
}
