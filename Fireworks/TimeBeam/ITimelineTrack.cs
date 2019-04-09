using System.Collections.Generic;
using Fireworks;

namespace TimeBeam {
  /// <summary>
  ///   Describes an item that can be placed on a track in the timeline.
  /// </summary>
  public interface ITimelineTrack : ITimelineTrackBase {
        IList<IKeyFrame> KeyFrames { get; set; }
  }
}