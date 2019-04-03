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
        //StopWatch for real fps count
        private Stopwatch stopWatch;
        private const int MS_IN_SEC = 1000;
        private Scene _scene;


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            stopWatch = new Stopwatch();

            List<KeyFrame> particleKeyFrames = new List<KeyFrame>();
            particleKeyFrames.Add(new KeyFrame(new PointF(0, 0), 1));
            particleKeyFrames.Add(new KeyFrame(new PointF(20, 10), 2));
            particleKeyFrames.Add(new KeyFrame(new PointF(200, 50), 4));

            _scene = new Scene(new Size(pnlScene.Width, pnlScene.Height));

            Particle p = new Particle(particleKeyFrames, new Size(3,3), 0);
            Firework f = new Firework(new KeyFrame(new PointF(100, 120), 3.3f), 100, 70, 10, 2);
            Firework f2 = new Firework(new KeyFrame(new PointF(150, 100), 3.1f), 50, 20, 10, 1);
            Firework f3 = new Firework(new KeyFrame(new PointF(110, 110), 3.2f), 80, 50, 10, 3);
            _scene.AddAnimatedObject(p);
            _scene.AddAnimatedObject(f);
            _scene.AddAnimatedObject(f2);
            _scene.AddAnimatedObject(f3);
        }

        private void SliderParameterChanged(object sender, EventArgs e)
        {
            trbTimeline.Maximum = (int)nudMaxTime.Value * MS_IN_SEC; //seconds to ms
        }

        private void trbTime_ValueChanged(object sender, EventArgs e)
        {
            tbxTime.Text = Convert.ToString(trbTimeline.Value / (float)MS_IN_SEC);
            pnlScene.Invalidate();
        }

        private void pnlScene_Paint(object sender, PaintEventArgs e)
        {
            stopWatch.Restart();
            _scene.Time = trbTimeline.Value / (float)MS_IN_SEC; //ms to seconds float
            e.Graphics.DrawImage(_scene.GetFrame(), new Point(0,0)); //draw scene
            stopWatch.Stop();
            nudActualFPS.Value = (decimal)((float)MS_IN_SEC / (stopWatch.ElapsedMilliseconds + 1)); // elapsed milliseconds + 1 to avoid divided by 0 error
        }

        private void StartAnimation()
        {
            tmrAnimate.Enabled = true;
            btnPlayPause.Text = "Pause";
        }

        private void StopAnimation()
        {
            tmrAnimate.Enabled = false;
            btnPlayPause.Text = "Play";
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            //Toggle state
            if (tmrAnimate.Enabled)
                StopAnimation();
            else
                StartAnimation();
        }

        private void tmrAnimate_Tick(object sender, EventArgs e)
        {
            //catch error when value + interval exceeds sliders max value
            if (trbTimeline.Value + tmrAnimate.Interval > trbTimeline.Maximum)
            {
                trbTimeline.Value = trbTimeline.Maximum; //Max out timer
                StopAnimation();
            }
            else
                trbTimeline.Value += tmrAnimate.Interval;
        }

        private void nudFPS_ValueChanged(object sender, EventArgs e)
        {
            tmrAnimate.Interval = (int)(MS_IN_SEC/nudFPS.Value); 
        }

        private void tbxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Accept only numbers, comma or backspace in textbox
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void tbxTime_TextChanged(object sender, EventArgs e)
        {
            //Change value of slider based on textbox value
            try
            {
                int newSliderValue = (int)(Convert.ToSingle(tbxTime.Text) * MS_IN_SEC);

                if (newSliderValue <= trbTimeline.Maximum)
                {
                    trbTimeline.Value = newSliderValue;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
