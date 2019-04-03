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
        private List<AnimatedObject> _animatedObjects = new List<AnimatedObject>();
        private static readonly Random rnd = new Random();

        public float Time { get => _time; set => _time = value; }

        /// <summary>
        /// Get frame at time of scene
        /// </summary>
        public Bitmap GetFrame()
        {
            return Paint();
        }

        /// <summary>
        /// Get the next frame after some time
        /// </summary>
        /// <param name="deltaTime">time that has passed</param>
        /// <returns>Current state of scene</returns>
        public Bitmap GetNextFrame(float deltaTime)
        {
            Time += deltaTime;

            return Paint();
        }

        /// <summary>
        /// Default constructor, default size of 400x400
        /// </summary>
        public Scene() : this(new SizeF(400,400))
        {

        }

        /// <summary>
        /// New Scene with specified size
        /// </summary>
        public Scene(SizeF size)
        {
            _size = size;
        }

        /// <summary>
        /// Get Frame at instant of scene
        /// </summary>
        private Bitmap Paint()
        {
            //Generate bitmap and graphics
            Bitmap frame = new Bitmap((int)_size.Width, (int)_size.Height);
            Graphics g = Graphics.FromImage(frame);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Draw every object on frame, based on their Z-order
            foreach (AnimatedObject o in _animatedObjects.OrderBy(x => x.ZOrder))
            {
                //Only draw if object is in lifetime
                if(o.IsTimeInLifeTime(_time))
                    o.PaintDebug(g, _time);
            }

            return frame;
        }

        /// <summary>
        /// Add an animated object to draw on the scene
        /// </summary>
        /// <param name="o">Object to draw on scene</param>
        public void AddAnimatedObject(AnimatedObject o)
        {
            _animatedObjects.Add(o);
        }
    }
}
