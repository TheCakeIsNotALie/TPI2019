using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KeyFrames
{
    /// <summary>
    /// Interface of what a keyframe should have at minimum
    /// </summary>
    public interface IKeyFrame : ICloneable
    {
        /// <summary>
        /// Location to be at time T
        /// </summary>
        PointF Point { get; set; }

        /// <summary>
        /// Time to be at location
        /// </summary>
        float T { get; set; }
    }
}

