using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Particle : AnimatedObject
    {
        private Brush _brush;

        /// <summary>
        /// Brush used to draw particle
        /// </summary>
        [Category("Visuals")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Brush Brush { get => _brush; set => _brush = value; }

        /// <summary>
        /// Create a new instance of Particle
        /// </summary>
        /// <param name="path">Path of keyframes the particle will follow</param>
        /// <param name="size">Size of particle</param>
        /// <param name="zOrder">Drawing order on scene</param>
        public Particle(Brush brush, List<KeyFrame> path, SizeF size, int zOrder) : 
            base(path, size, zOrder)
        {
            Brush = brush;
        }

        /// <summary>
        /// Basic instance of particle
        /// </summary>
        public Particle() : this(Brushes.Black, KeyFrame.BasicKeyFrames, new SizeF(), 0)
        {
        }

        /// <summary>
        /// Draws the particle as a filled ellipse
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public override void Paint(Graphics g, float t)
        {
            g.FillEllipse(Brush, HitBox(t));
        }
    }
}
