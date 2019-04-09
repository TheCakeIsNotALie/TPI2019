using System;
using System.Drawing;
using Fireworks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeBeam.Surrogates
{
    class KeyFrameSurrogate : IKeyFrame
    {
        public IKeyFrame SubstituteFor { get; set; }

        public PointF Point { get; set; }

        public float T { get; set; }
        
        public KeyFrameSurrogate(IKeyFrame substituteFor)
        {
            SubstituteFor = substituteFor;
            Point = substituteFor.Point;
            T = substituteFor.T;
        }
        
        public KeyFrameSurrogate()
        {

        }

        public void CopyTo(IKeyFrame target)
        {
            target.Point = Point;
            target.T = T;
        }

        public object Clone()
        {
            KeyFrameSurrogate tmp =  new KeyFrameSurrogate();
            tmp.SubstituteFor = SubstituteFor;
            tmp.T = T;
            tmp.Point = Point;

            return tmp;
        }
    }
}
