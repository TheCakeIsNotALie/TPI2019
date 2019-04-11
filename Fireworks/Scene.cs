using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public float Time { get => _time; set => _time = value; }

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

        public SizeF Size { get => _size; set => _size = value; }

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

            if (SelectedObject != null && SelectedObject.IsTimeInLifeTime(_time))
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
    }
}
