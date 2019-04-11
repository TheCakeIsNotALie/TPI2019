using System;
using Fireworks;
using System.Collections.Generic;

namespace TimeBeam.Events
{
    /// <summary>
    ///   Event arguments for an event that notifies about changes in the selected Keyframes
    /// </summary>
    public class SelectionModifiedEventArgs : EventArgs
    {
        /// <summary>
        ///   The keyframes that were changed
        /// </summary>
        public IEnumerable<IKeyFrame> ModifiedKeyFrames { get; private set; }

        /// <summary>
        ///   The Tracks that had keyframes modified
        /// </summary>
        public IEnumerable<ITimelineTrack> ModifiedTracks { get; private set; }

        /// <summary>
        ///   Construct a new SelectionModifiedEventArgs instance.
        /// </summary>
        /// <param name="modified">The Keyframes that were changed</param>
        public SelectionModifiedEventArgs(IEnumerable<ITimelineTrack> modifiedTracks, IEnumerable<IKeyFrame> modifiedKeyFrames)
        {
            ModifiedKeyFrames = modifiedKeyFrames;
            ModifiedTracks = modifiedTracks;
        }

        /// <summary>
        ///   An empty instance of the <see cref="SelectionModifiedEventArgs"/> class.
        /// </summary>
        public new static SelectionModifiedEventArgs Empty
        {
            get { return new SelectionModifiedEventArgs(null, null); }
        }
    }
}