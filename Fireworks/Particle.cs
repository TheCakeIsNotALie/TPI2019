using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using KeyFrames;

namespace Fireworks
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Particle : AnimatedObject
    {
        private SolidBrush _brush;

        /// <summary>
        /// Color that will be used for drawing this particle
        /// </summary>
        [Category("Visuals")]
        [Description("Color that will be used for drawing this particle")]
        [DisplayName("Color")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Color Color
        {
            get => _brush.Color;
            set
            {
                _brush = new SolidBrush(value);
            }
        }

        /// <summary>
        /// Create a new instance of Particle
        /// </summary>
        /// <param name="name">Name of the particle</param>
        /// <param name="color">Color that will be used to draw the particle</param>
        /// <param name="path">Path of keyframes the particle will follow</param>
        /// <param name="size">Size of particle</param>
        /// <param name="zOrder">Drawing order on scene</param>
        public Particle(string name, Color color, IList<IKeyFrame> path, SizeF size, int zOrder) : 
            base(name, path, size, zOrder)
        {
            Color = color;
        }

        /// <summary>
        /// Basic instance of particle
        /// </summary>
        public Particle() : this("Particle", Color.Black, KeyFrame.BasicKeyFrames, new SizeF(1,1), 0)
        {

        }

        /// <summary>
        /// Draws the particle as a filled ellipse
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public override void Paint(Graphics g, float t)
        {
            g.FillEllipse(_brush, HitBox(t));
        }
    }
}
