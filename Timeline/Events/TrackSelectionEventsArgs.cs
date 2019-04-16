using System;
using System.Collections.Generic;
using KeyFrames;

namespace Timeline.Events
{
    /// <summary>
    ///   Event arguments for an event that notifies about a change in the selection
    ///   of tracks.
    /// </summary>
    public class TrackSelectionEventsArgs : EventArgs
    {
        /// <summary>
        /// The selected track
        /// </summary>
        public ITimelineTrack SelectedTrack { get; private set; }

        /// <summary>
        /// Construct a new TrackSelectionEventsArgs instance
        /// </summary>
        /// <param name="track">The track that was selected</param>
        public TrackSelectionEventsArgs(ITimelineTrack track)
        {
            SelectedTrack = track;
        }
    }
}