using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KeyFrames
{
    public interface IKeyFrame : ICloneable
    {
        PointF Point { get; set; }
        float T { get; set; }
    }
}

