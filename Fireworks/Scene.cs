using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KeyFrames;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    /// <summary>
    /// Scene that contains animated objects
    /// </summary>
    public class Scene
    {
        private SizeF _size;
        private float _time = 0;
        private bool _paintDebugSelectedObject;
        private AnimatedObject _selectedObject;
        private List<AnimatedObject> _animatedObjects = new List<AnimatedObject>();
        private static readonly Random rnd = new Random();

        // Event handler taken mostly from
        // from : https://www.codeproject.com/Articles/9355/Creating-advanced-C-custom-events
        /// <summary>
        /// Event called when selected object is changed
        /// </summary>
        public event EventHandler SelectedObjectChanged;

        /// <summary>
        /// Event called when list AnimatedObjects has changed
        /// </summary>
        public event EventHandler AnimatedObjectsChanged;

        /// <summary>
        /// Current time of scene
        /// </summary>
        public float Time { get => _time; set => _time = value; }

        /// <summary>
        /// Maximum time between all of the animated objects
        /// </summary>
        public float MaxTime
        {
            get
            {
                try
                {
                    return AnimatedObjects.SelectMany(x => x.Keyframes).Max(x => x.T);
                } 
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// List of every animated objects in the scene
        /// </summary>
        public List<AnimatedObject> AnimatedObjects
        {
            get => _animatedObjects;
            set
            {
                _animatedObjects = value;
                AnimatedObjectsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// The currently selected object
        /// </summary>
        public AnimatedObject SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                SelectedObjectChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Size of drawn image
        /// </summary>
        public SizeF Size { get => _size; set => _size = value; }

        /// <summary>
        /// Defines if scene will draw the selected object in debug mode
        /// </summary>
        public bool PaintDebugSelectedObject { get => _paintDebugSelectedObject; set => _paintDebugSelectedObject = value; }

        /// <summary>
        /// Default constructor, default size of 400x400
        /// </summary>
        public Scene() : this(new SizeF(400, 400))
        {

        }

        /// <summary>
        /// New Scene with specified size
        /// </summary>
        public Scene(SizeF size)
        {
            Size = size;
        }

        /// <summary>
        /// Get frame at time of Scene
        /// </summary>
        public Bitmap GetFrame()
        {
            return Paint();
        }

        /// <summary>
        /// Get Frame at instant of scene
        /// </summary>
        private Bitmap Paint()
        {
            //Generate bitmap and graphics
            Bitmap frame = new Bitmap((int)Size.Width, (int)Size.Height);
            Graphics g = Graphics.FromImage(frame);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Draw every object on frame, by their Z-order
            foreach (AnimatedObject o in AnimatedObjects.OrderBy(x => x.ZOrder))
            {
                //Only draw if object is in lifetime
                if (o.IsTimeInLifeTime(_time))
                    o.Paint(g, _time);
            }

            if (SelectedObject != null && SelectedObject.IsTimeInLifeTime(_time) && PaintDebugSelectedObject)
            {
                //Draw selected object
                SelectedObject.PaintDebug(g, _time);
            }

            return frame;
        }

        /// <summary>
        /// Add an animated object to draw on the scene
        /// </summary>
        /// <param name="o">Object to add</param>
        public void AddAnimatedObject(AnimatedObject o)
        {
            AnimatedObjects.Add(o);
            SelectedObject = o;
            AnimatedObjectsChanged(this, new EventArgs());
        }

        /// <summary>
        /// Remove an animated object already present in scene
        /// </summary>
        /// <param name="o">Object to remove</param>
        public void RemoveAnimatedObject(AnimatedObject o)
        {
            AnimatedObjects.Remove(o);
            AnimatedObjectsChanged(this, new EventArgs());
        }

        /// <summary>
        /// ForceRefresh of an animated object
        /// </summary>
        /// <param name="o">Object to refresh</param>
        public void ForceAnimatedObjectUpdate(AnimatedObject o)
        {
            o.Update();
        }

        /// <summary>
        /// Add a sample demo set of objects to the scene
        /// </summary>
        public void AddDemoObjects()
        {
            List<AnimatedObject> toAdd = new List<AnimatedObject>();

            //Stars
            List<IKeyFrame> tmp1 = new List<IKeyFrame>();
            tmp1.Add(new KeyFrame(new PointF(25, 25), 0));
            tmp1.Add(new KeyFrame(new PointF(this.Size.Width - 25, 25), 1));
            tmp1.Add(new KeyFrame(new PointF(this.Size.Width - 25, this.Size.Height - 25), 2));
            tmp1.Add(new KeyFrame(new PointF(25, this.Size.Height - 25), 3));
            tmp1.Add(new KeyFrame(new PointF(25, 25), 4));
            List<List<IKeyFrame>> tmps = new List<List<IKeyFrame>>();
            PointF[] starCorners = { new PointF(0, -10), new PointF(-5, -5), new PointF(-10, 0), new PointF(-5, 5), new PointF(0, 10), new PointF(5, 5), new PointF(10, 0), new PointF(5, -5) };
            for (int i = 0; i < 10; i++)
            {
                tmps.Add(new List<IKeyFrame>());
                foreach (IKeyFrame kf in tmp1)
                {
                    IKeyFrame ckf = (IKeyFrame)kf.Clone();
                    ckf.T += i + 1;
                    tmps[i].Add(ckf);
                }
                toAdd.Add(new Polygon("Star" + i.ToString(), tmps[i], (PointF[])starCorners.Clone(), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), 0));
            }
            tmps.Clear();

            //Fireworks
            List<IKeyFrame> tmp2 = new List<IKeyFrame>();
            tmp2.Add(new KeyFrame(new PointF(this.Size.Width / 2, this.Size.Height / 2), 0));
            tmp2.Add(new KeyFrame(new PointF(this.Size.Width / 2, this.Size.Height / 2), 1));
            
            for (int i = 0; i < 10; i++)
            {
                tmps.Add(new List<IKeyFrame>());
                foreach (IKeyFrame kf in tmp2)
                {
                    IKeyFrame ckf = (IKeyFrame)kf.Clone();
                    ckf.T += i + 0.5f;
                    tmps[i].Add(ckf);
                }
                toAdd.Add(new Firework("Firework" + i.ToString(), Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256)), tmps[i], (i + 1) * 25, (i + 1) * 10, 0));
            }
            tmps.Clear();

            //Fireworks Left
            List<IKeyFrame> tmp3 = new List<IKeyFrame>();
            tmp3.Add(new KeyFrame(new PointF(this.Size.Width / 3, this.Size.Height / 3), 0.5f));
            tmp3.Add(new KeyFrame(new PointF(this.Size.Width / 3, this.Size.Height / 3), 1.5f));

            for (int i = 0; i < 5; i++)
            {
                tmps.Add(new List<IKeyFrame>());
                foreach (IKeyFrame kf in tmp3)
                {
                    IKeyFrame ckf = (IKeyFrame)kf.Clone();
                    ckf.T += i + 1.5f;
                    tmps[i].Add(ckf);
                }
                toAdd.Add(new Firework("FireworkLeft" + i.ToString(), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), tmps[i], 100, 100, 0));
            }
            tmps.Clear();

            //Fireworks Right
            List<IKeyFrame> tmp4 = new List<IKeyFrame>();
            tmp4.Add(new KeyFrame(new PointF(this.Size.Width - this.Size.Width / 3, this.Size.Height - this.Size.Height / 3), 0.5f));
            tmp4.Add(new KeyFrame(new PointF(this.Size.Width - this.Size.Width / 3, this.Size.Height - this.Size.Height / 3), 1.5f));

            for (int i = 0; i < 5; i++)
            {
                tmps.Add(new List<IKeyFrame>());
                foreach (IKeyFrame kf in tmp4)
                {
                    IKeyFrame ckf = (IKeyFrame)kf.Clone();
                    ckf.T += i + 1.5f;
                    tmps[i].Add(ckf);
                }
                toAdd.Add(new Firework("FireworkRight" + i.ToString(), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), tmps[i], 100, 100, 0));
            }
            tmps.Clear();


            AnimatedObjects.AddRange(toAdd);
            AnimatedObjectsChanged(this, new EventArgs());
        }
    }
}
