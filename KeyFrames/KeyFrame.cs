using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFrames
{
    /// <summary>
    /// Stores a position and a time at which an object needs to be on this point
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KeyFrame : IKeyFrame
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

        /// <summary>
        /// Creates a new instance of keyframe
        /// </summary>
        /// <param name="point">Point to be at time T</param>
        /// <param name="t">Time to be at Point</param>
        public KeyFrame(PointF point, float t)
        {
            _point = point;
            _t = t;
        }

        /// <summary>
        /// Basic instance of keyframe
        /// </summary>
        public KeyFrame() : this(new PointF(), 0)
        {

        }

        /// <summary>
        /// Basic list of keyframes { (P(0,0):t(0)) -> (P(0,0):t(1)) }
        /// </summary>
        public static IList<IKeyFrame> BasicKeyFrames
        {
            get
            {
                List<IKeyFrame> basics = new List<IKeyFrame>();

                basics.Add(new KeyFrame(new PointF(), 0));
                basics.Add(new KeyFrame(new PointF(), 1));

                return basics;
            }
        }

        public object Clone()
        {
            return new KeyFrame(new PointF(Point.X, Point.Y), T);
        }
    }
}
