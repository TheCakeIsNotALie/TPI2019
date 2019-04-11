using System;
using System.Collections.Generic;
using KeyFrames;

namespace Timeline.Events
{
    /// <summary>
    ///   Event arguments for an event that notifies about deletion of the selected Keyframes
    /// </summary>
    public class SelectionDeletedEventArgs : EventArgs
    {
        /// <summary>
        ///   The keyframes that were deleted
        /// </summary>
        public IEnumerable<IKeyFrame> DeletedKeyFrames { get; private set; }

        /// <summary>
        ///   The Tracks that had keyframes deleted
        /// </summary>
        public IEnumerable<ITimelineTrack> DeletedInTracks { get; private set; }

        /// <summary>
        ///   Construct a new SelectionDeletedEventArgs instance.
        /// </summary>
        /// <param name="deletedInTracks">Track in which the keyframes were deleted</param>
        /// <param name="deletedKeyFrames">The Keyframes that were deleted</param>
        public SelectionDeletedEventArgs(IEnumerable<ITimelineTrack> deletedInTracks, IEnumerable<IKeyFrame> deletedKeyFrames)
        {
            DeletedInTracks = deletedInTracks;
            DeletedKeyFrames = deletedKeyFrames;
        }
    }
}