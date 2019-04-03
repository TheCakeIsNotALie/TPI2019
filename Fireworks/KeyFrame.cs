using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    /// <summary>
    /// Stores a position and a time at which an object needs to be on this point
    /// </summary>
    public class KeyFrame
    {
        private PointF _point;
        private float _t;

        public PointF Point { get => _point; set => _point = value; }
        public float T { get => _t; set => _t = value; }

        public KeyFrame(PointF point, float t)
        {
            _point = point;
            _t = t;
        }
    }
}
