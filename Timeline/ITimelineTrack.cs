using System.Collections.Generic;
using KeyFrames;

namespace Timeline
{
  /// <summary>
  ///   Describes an item that can be placed on a track in the timeline.
  /// </summary>
  public interface ITimelineTrack : ITimelineTrackBase {
        IList<IKeyFrame> KeyFrames { get; set; }
  }
}