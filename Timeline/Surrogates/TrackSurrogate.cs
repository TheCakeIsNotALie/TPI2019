using System.Collections.Generic;
using KeyFrames;
using Timeline.Helper;

namespace Timeline.Surrogates
{
    /// <summary>
    ///   A substitute for another track on the timeline.
    /// </summary>
    internal class TrackSurrogate : ITimelineTrack
    {
        /// <summary>
        ///   The object this surrogate is a substitute for.
        /// </summary>
        public ITimelineTrack SubstituteFor { get; set; }

        /// <summary>
        ///   The name of the item.
        /// </summary>
        public string Name { get; set; }

        public IList<IKeyFrame> KeyFrames { get; set; }

        /// <summary>
        ///   Construct a new surrogate for another <see cref="ITimelineTrack" />.
        /// </summary>
        /// <param name="substituteFor">The ITimelineTrack we're substituting for.</param>
        public TrackSurrogate(ITimelineTrack substituteFor)
        {
            SubstituteFor = substituteFor;

            //create surrogates of the keyframes that the original track has
            KeyFrames = new List<IKeyFrame>();
            foreach (IKeyFrame kf in substituteFor.KeyFrames)
            {
                KeyFrames.Add(new KeyFrameSurrogate(kf));
            }

            Name = substituteFor.Name;
        }

        /// <summary>
        ///   Copies all properties of the surrogate to another <see cref="ITimelineTrack" />.
        /// </summary>
        /// <param name="target">The target timeline track to copy the properties to.</param>
        public void CopyTo(ITimelineTrack target)
        {
            target.KeyFrames = Extensions.Clone<IKeyFrame>(KeyFrames);
            target.Name = Name;
        }
    }
}