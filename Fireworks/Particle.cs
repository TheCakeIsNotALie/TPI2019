using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    public class Particle : AnimatedObject
    {
        /// <summary>
        /// Create a new instance of Particle
        /// </summary>
        /// <param name="path">Path of keyframes the particle will follow</param>
        /// <param name="size">Size of particle</param>
        /// <param name="zOrder">Drawing order on scene</param>
        public Particle(List<KeyFrame> path, SizeF size, int zOrder) : 
            base(path, size, zOrder)
        {

        }

        /// <summary>
        /// Draws the particle as a filled ellipse
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public override void Paint(Graphics g, float t)
        {
            g.FillEllipse(Brushes.Black, HitBox(t));
        }
    }
}
