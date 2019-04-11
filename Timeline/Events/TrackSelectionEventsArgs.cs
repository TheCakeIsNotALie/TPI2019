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
        ///   The track that was selected
        /// </summary>
        public ITimelineTrack SelectedTrack { get; private set; }

        /// <summary>
        ///   Construct a new TrackSelectionEventsArgs instance.
        /// </summary>
        /// <param name="selected">The track elements that were deselected in the operation.</param>
        /// <param name="deselected">The tracks that were selected in the operation.</param>
        public TrackSelectionEventsArgs(ITimelineTrack track)
        {
            SelectedTrack = track;
        }
    }
}