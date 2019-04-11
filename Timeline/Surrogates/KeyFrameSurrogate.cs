using System.Drawing;
using KeyFrames;

namespace Timeline.Surrogates
{
    /// <summary>
    /// A substitute for another keyframe on the timeline
    /// (Change values here and copy to real keyframe when moving is validated)
    /// </summary>
    class KeyFrameSurrogate : IKeyFrame
    {
        /// <summary>
        /// Substitute for keyframe x
        /// </summary>
        public IKeyFrame SubstituteFor { get; set; }
        /// <summary>
        /// Point of KeyFrameSurrogate
        /// </summary>
        public PointF Point { get; set; }
        /// <summary>
        /// Time of KeyFrameSurrogate
        /// </summary>
        public float T { get; set; }
        
        /// <summary>
        /// Creates a copy of a keyframe
        /// </summary>
        /// <param name="substituteFor">Substitutes keyframe</param>
        /// <param name="owner">The track the keyframe belongs to</param>
        public KeyFrameSurrogate(IKeyFrame substituteFor)
        {
            SubstituteFor = substituteFor;
            Point = substituteFor.Point;
            T = substituteFor.T;
        }
        
        /// <summary>
        /// Empty contructor
        /// </summary>
        public KeyFrameSurrogate()
        {

        }

        /// <summary>
        /// Copy surrogates values to target
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(IKeyFrame target)
        {
            target.Point = Point;
            target.T = T;
        }

        /// <summary>
        /// Clone surrogate
        /// </summary>
        /// <returns>Copy of surrogate</returns>
        public object Clone()
        {
            KeyFrameSurrogate tmp =  new KeyFrameSurrogate();
            tmp.SubstituteFor = SubstituteFor;
            tmp.T = T;
            tmp.Point = Point;

            return tmp;
        }

        /// <summary>
        /// Cast to KeyFrame
        /// </summary>
        public static explicit operator KeyFrame(KeyFrameSurrogate surrogate)
        {
            return new KeyFrame(surrogate.Point, surrogate.T);
        }
    }
}
