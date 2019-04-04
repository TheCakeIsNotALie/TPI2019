using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    /// <summary>
    /// Stores a position and a time at which an object needs to be on this point
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KeyFrame
    {
        private PointF _point;
        private float _t;

        /// <summary>
        /// Point to be at time T
        /// </summary>
        [Category("Properties")]
        [Description("Point to be at time T")]
        [DisplayName("Point")]
        [TypeConverter(typeof(ValueTypeTypeConverter))]
        public PointF Point { get => _point; set => _point = value; }

        /// <summary>
        /// Time to be at Point
        /// </summary>
        [Category("Properties")]
        [Description("Time to be at Point")]
        [DisplayName("Time")]
        public float T { get => _t; set => _t = value; }

        public KeyFrame(PointF point, float t)
        {
            _point = point;
            _t = t;
        }
        public KeyFrame() : this(new PointF(), 0)
        {

        }

        public static List<KeyFrame> BasicKeyFrames()
        {
            List<KeyFrame> basics = new List<KeyFrame>();

            basics.Add(new KeyFrame(new PointF(), 0));
            basics.Add(new KeyFrame(new PointF(), 1));

            return basics;
        }
    }
}
