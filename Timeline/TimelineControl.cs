using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Timeline.Events;
using Timeline.Helper;
using Timeline.Surrogates;
using KeyFrames;

namespace Timeline
{
    /// <summary>
    ///   The main host control.
    ///   Taken from : https://github.com/oliversalzburg/TimeBeam
    ///   and heavily modified for uses with keyframes
    /// </summary>
    public partial class TimelineControl : UserControl
    {
        /// <summary>
        /// Defines the minimum allowed time
        /// </summary>
        private const float MIN_TIME = 0;

        private const float MIN_TIME_BETWEEN_KEYFRAMES = 0.2f;

        /// <summary>
        /// Backing field for <see cref="TimeSeconds"/>
        /// </summary>
        private float _timeSeconds;

        /// <summary>
        /// Current timeline time
        /// </summary>
        public float TimeSeconds
        {
            get => _timeSeconds;
            set
            {
                if (value > MinTime)
                    _timeSeconds = value;
                else
                    _timeSeconds = MinTime;
                Invalidate();
            }
        }

        /// <summary>
        /// Min time allowed
        /// </summary>
        public float MinTime
        {
            get => MIN_TIME;
        }

        /// <summary>
        ///   The tracks currently placed on the timeline.
        /// </summary>
        private List<ITimelineTrack> _tracks = new List<ITimelineTrack>();

        /// <summary>
        /// Get the tracks that timeline has
        /// </summary>
        public List<ITimelineTrack> GetTracks() {
            return _tracks;
        }

        /// <summary>
        /// Set the current tracks of timeline
        /// </summary>
        /// <param name="tracks">List of tracks to assign to timeline</param>
        public void SetTracks(List<ITimelineTrack> tracks)
        {
            _tracks = tracks;
        }
        #region Events

        public delegate void SelectionModifiedHandler(object sender, SelectionModifiedEventArgs eventArgs);

        /// <summary>
        /// Event called when the selection of keyframe has been modified
        /// </summary>
        public event SelectionModifiedHandler SelectionModified;

        /// <summary>
        /// Event called when user has changed
        /// </summary>
        public event EventHandler TimeChangedFromInput;

        public delegate void SelectionDeletedHandler(object sender, SelectionDeletedEventArgs eventArgs);

        /// <summary>
        /// Event called when the selection has been deleted
        /// </summary>
        public event SelectionDeletedHandler SelectionDeleted;

        public delegate void TrackSelectionHandler(object sender, TrackSelectionEventsArgs eventsArgs);

        /// <summary>
        /// Event called when the user selects a track
        /// </summary>
        public event TrackSelectionHandler TrackSelected;

        #endregion

        #region Layout Variables
        /// <summary>
        ///   Backing field for <see cref="TrackHeight" />.
        /// </summary>
        private int _trackHeight = 20;

        /// <summary>
        ///   How high a single track should be.
        /// </summary>
        [Description("How high a single track should be.")]
        [Category("Layout")]
        public int TrackHeight
        {
            get { return _trackHeight; }
            set { _trackHeight = value; }
        }

        /// <summary>
        /// Backing field for <see cref="KeyFrameWidth"/>
        /// </summary>
        private int _keyFrameWidth = 1;

        /// <summary>
        /// Width of a keyframe
        /// </summary>
        [Description("How wide a keyframe should be.")]
        [Category("Layout")]
        public int KeyFrameWidth
        {
            get { return _keyFrameWidth; }
            set { _keyFrameWidth = value; }
        }

        /// <summary>
        ///   Backing field for <see cref="KeyFrameBorderWidth" />.
        /// </summary>
        private int _keyFrameBorderWidth = 1;

        /// <summary>
        ///   How wide/high the border on a track item should be.
        ///   This border allows you to interact with an item.
        /// </summary>
        [Description("How wide/high the border on a track item should be.")]
        [Category("Layout")]
        public int KeyFrameBorderWidth
        {
            get { return _keyFrameBorderWidth; }
            set { _keyFrameBorderWidth = value; }
        }

        /// <summary>
        ///   Backing field for <see cref="TrackSpacing" />.
        /// </summary>
        private int _trackSpacing = 1;

        /// <summary>
        ///   How much space should be left between every track.
        /// </summary>
        [Description("How much space should be left between every track.")]
        [Category("Layout")]
        public int TrackSpacing
        {
            get { return _trackSpacing; }
            set { _trackSpacing = value; }
        }

        /// <summary>
        ///   Backing field for <see cref="TrackLabelWidth" />.
        /// </summary>
        private int _trackLabelWidth = 100;

        /// <summary>
        ///   The width of the label section before the tracks.
        /// </summary>
        [Description("The width of the label section before the tracks.")]
        [Category("Layout")]
        private int TrackLabelWidth
        {
            get { return _trackLabelWidth; }
            set { _trackLabelWidth = value; }
        }

        /// <summary>
        ///   The font to use to draw the track labels.
        /// </summary>
        private Font _labelFont = DefaultFont;

        /// <summary>
        ///   The size of the top part of the playhead.
        /// </summary>
        private SizeF _playheadExtents = new SizeF(5, 16);
        #endregion

        #region Drawing Variables
        /// <summary>
        ///   Backing field for <see cref="BackgroundColor" />.
        /// </summary>
        private Color _backgroundColor = Color.Black;

        /// <summary>
        ///   The background color of the timeline.
        /// </summary>
        [Description("The background color of the timeline.")]
        [Category("Drawing")]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        ///   When the timeline is scrolled (panned) around, this offset represents the panned distance.
        /// </summary>
        private PointF _renderingOffset = PointF.Empty;

        internal PointF RenderingOffset
        {
            get { return _renderingOffset; }
        }

        /// <summary>
        /// Backing field of <see cref="RenderingScale"/>.
        /// </summary>
        private PointF _renderingScale = new PointF(1, 1);

        /// <summary>
        ///   The scale at which to render the timeline.
        ///   This enables us to "zoom" the timeline in and out.
        /// </summary>
        [Description("The background color of the timeline.")]
        [Category("Drawing")]
        [TypeConverter(typeof(ValueTypeTypeConverter))]
        public PointF RenderingScale
        {
            get { return _renderingScale; }
            set { _renderingScale = value; }
        }

        /// <summary>
        ///   Backing field for <see cref="GridAlpha" />.
        /// </summary>
        private int _gridAlpha = 40;

        /// <summary>
        ///   The transparency of the background grid.
        /// </summary>
        [Description("The transparency of the background grid.")]
        [Category("Drawing")]
        public int GridAlpha
        {
            get { return _gridAlpha; }
            set { _gridAlpha = value; }
        }
        #endregion

        #region Interaction Variables
        /// <summary>
        ///   How far does the user have to move the mouse (while holding down the left mouse button) until dragging operations kick in?
        ///   Technically, this defines the length of the movement vector.
        /// </summary>
        private const float DraggingThreshold = 3f;

        /// <summary>
        ///   What mode is the timeline currently in?
        /// </summary>
        public BehaviorMode CurrentMode { get; private set; }

        /// <summary>
        /// Currently selected keyframes
        /// </summary>
        private readonly List<IKeyFrame> _selectedKeyFrames = new List<IKeyFrame>();

        /// <summary>
        /// Copies of the keyframes currently being changed
        /// </summary>
        private List<IKeyFrame> _keyframeSurrogates = new List<IKeyFrame>();

        /// <summary>
        /// Copies of the tracks currently having keyframes moved in them
        /// </summary>
        private List<ITimelineTrack> _trackSurrogates = new List<ITimelineTrack>();

        /// <summary>
        ///   The point at where a dragging operation started.
        /// </summary>
        private PointF _dragOrigin;

        /// <summary>
        ///   The point where the user started panning the view.
        /// </summary>
        private PointF _panOrigin;

        /// <summary>
        ///   The rendering offset as it was before a panning operation started.
        ///   Remembering this allows us to dynamically apply a delta during the panning operation.
        /// </summary>
        private PointF _renderingOffsetBeforePan = PointF.Empty;

        /// <summary>
        /// Backing field of <see cref="ZoomBalast"/>
        /// </summary>
        private float _zoomBalast = 750f;

        /// <summary>
        /// Slow down the zoom speed by dividing the scroll delta by the balast
        /// </summary>
        public float ZoomBalast { get => _zoomBalast; set => _zoomBalast = value; }
        #endregion

        #region Enums
        /// <summary>
        ///   Enumerates states the timeline can be in.
        ///   These are usually invoked through user interaction.
        /// </summary>
        public enum BehaviorMode
        {
            /// <summary>
            ///   The timeline is idle or not using any more specific state.
            /// </summary>
            Idle,

            /// <summary>
            ///   The user is currently in the process of selecting items on the timeline.
            /// </summary>
            Selecting,

            /// <summary>
            ///   The user is currently moving selected items.
            /// </summary>
            MovingSelection,

            /// <summary>
            ///   The user is almost moving selected items.
            /// </summary>
            RequestMovingSelection,

            /// <summary>
            /// The user is changing a keyframe's 
            /// </summary>
            ChangingKeyFramePosition,

            /// <summary>
            ///   The user is scrubbing the playhead.
            /// </summary>
            TimeScrub
        }
        #endregion

        #region Constructor
        /// <summary>
        ///   Construct a new timeline.
        /// </summary>
        public TimelineControl()
        {
            InitializeComponent();
            SetStyle(
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.Opaque |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.Selectable |
              ControlStyles.UserPaint, true);

            // Set up the font to use to draw the track labels
            float emHeightForLabel = EmHeightForLabel("WM_g^~", TrackHeight);
            _labelFont = new Font(DefaultFont.FontFamily, emHeightForLabel - 2);
        }
        #endregion

        #region Helpers
        /// <summary>
        ///   Recalculates appropriate values for scrollbar bounds.
        /// </summary>
        private void RecalculateScrollbarBounds()
        {
            ScrollbarV.Max = (int)((_tracks.Count * (TrackHeight + TrackSpacing)) * _renderingScale.Y);
            ScrollbarH.Max = (int)(_tracks.Max(t => t.KeyFrames.Last().T) * _renderingScale.X);
            ScrollbarV.Refresh();
            ScrollbarH.Refresh();
        }

        /// <summary>
        ///   Calculate the rectangle within which track should be drawn.
        /// </summary>
        /// <returns>The rectangle within which all tracks should be drawn.</returns>
        internal Rectangle GetTrackAreaBounds()
        {
            Rectangle trackArea = new Rectangle();

            // Start after the track labels
            trackArea.X = TrackLabelWidth;
            // Start at the top (later, we'll deduct the playhead and time label height)
            trackArea.Y = (int)_playheadExtents.Height;
            // Deduct scrollbar width.
            trackArea.Width = Width - ScrollbarV.Width;
            // Deduct scrollbar height.
            trackArea.Height = Height - ScrollbarH.Height;

            return trackArea;
        }


        /// <summary>
        ///   Check if a track is located at the given position.
        /// </summary>
        /// <param name="test">The point to test for</param>
        /// <returns>
        ///   The <see cref="ITimelineTrack" /> if there is one under the given point; <see langword="null" /> otherwise.
        /// </returns>
        private ITimelineTrack TrackHitTest(PointF test)
        {
            foreach (ITimelineTrack track in _tracks)
            {
                // The extent of the track, including the border
                RectangleF trackExtent = BoundsHelper.GetTrackExtents(track, this);

                if (trackExtent.Contains(test))
                {
                    return track;
                }
            }

            return null;
        }

        /// <summary>
        ///   Retrieve the index of a given track.
        ///   If the track is a surrogate, returns the index of the track it's a substitute for.
        /// </summary>
        /// <param name="track">The track for which to retrieve the index.</param>
        /// <returns>The index of the track or the index the track is a substitute for.</returns>
        internal int TrackIndexForTrack(ITimelineTrack track)
        {
            ITimelineTrack trackToLookFor = track;
            if (track is TrackSurrogate)
            {
                trackToLookFor = ((TrackSurrogate)track).SubstituteFor;
            }
            return _tracks.FindIndex(t => t == trackToLookFor);
        }

        /// <summary>
        /// Check if a keyframe is located at the given position
        /// </summary>
        /// <param name="test">The point to test for</param>
        /// <returns>The <see cref="KeyFrame" /> if there is one under the given point; <see langword="null" /> otherwise.</returns>
        private IKeyFrame KeyFrameHitTest(PointF test)
        {
            //Go through every keyframes in every tracks
            foreach (ITimelineTrack track in _tracks)
            {
                foreach (IKeyFrame kf in track.KeyFrames)
                {
                    // The extent of the keyframe on screen
                    RectangleF kfExtent = BoundsHelper.GetKeyFrameExtents(track, kf, this);

                    if (kfExtent.Contains(test))
                    {
                        return kf;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get track index that keyframe is owned by
        /// </summary>
        /// <param name="kf">Keyframe to search</param>
        /// <returns>-1 if not found or index of track if found</returns>
        private int TrackIndexForKeyFrame(IKeyFrame kf)
        {
            //Search the real keyframe
            IKeyFrame toSearch = kf;
            if (kf is KeyFrameSurrogate)
            {
                toSearch = ((KeyFrameSurrogate)kf).SubstituteFor;
            }

            //find which track owns keyframe
            foreach (ITimelineTrack track in _tracks)
            {
                if (track.KeyFrames.Contains(toSearch))
                {
                    return TrackIndexForTrack(track);
                }
            }

            //keyframe not found
            return -1;
        }

        /// <summary>
        ///   Check if a track label is located at the given position.
        /// </summary>
        /// <param name="test">The point to test for.</param>
        /// <returns>The index of the track the hit label belongs to, if one was hit; -1 otherwise.</returns>
        private int TrackLabelHitTest(PointF test)
        {
            if (test.X > 0 && test.X < TrackLabelWidth)
            {
                return TrackIndexAtPoint(test);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        ///   Get the index of the track that sits at a certain point.
        /// </summary>
        /// <param name="test">The point where to look for a track.</param>
        /// <returns>The index of the track if one was found; -1 otherwise.</returns>
        private int TrackIndexAtPoint(PointF test)
        {
            for (int index = 0; index < _tracks.Count; index++)
            {
                RectangleF trackExtent = BoundsHelper.GetTrackExtents(_tracks[index], this);

                if (trackExtent.Top < test.Y && trackExtent.Bottom > test.Y)
                {
                    return index;
                }
            }
            return -1;
        }

        /// <summary>
        ///   Calculate an Em-height for a font to fit within a given height.
        /// </summary>
        /// <param name="label">The text to use for the measurement.</param>
        /// <param name="maxHeight">The largest height the text must fit into.</param>
        /// <returns>An Em-height that can be used to construct a font that will fit into the given height.</returns>
        private float EmHeightForLabel(string label, float maxHeight)
        {
            float size = DefaultFont.Size;
            Font currentFont = new Font(DefaultFont.FontFamily, size);
            Graphics graphics = Graphics.FromHwnd(this.Handle);
            SizeF measured = graphics.MeasureString(label, currentFont);
            while (measured.Height < maxHeight)
            {
                size += 1;
                currentFont = new Font(DefaultFont.FontFamily, size);
                measured = graphics.MeasureString(label, currentFont);
            }
            return size - 1;
        }

        /// <summary>
        ///   Helper method to check if a given key is being pressed.
        /// </summary>
        /// <param name="key">The key to check if it is being held down.</param>
        /// <param name="keys">The collection of keys that hold the information about which keys are being held down. If none are provided, ModifierKeys is being used.</param>
        /// <returns>
        ///   <see langword="true" /> if the key is down; <see langword="false" /> otherwise.
        /// </returns>
        private bool IsKeyDown(Keys key, Keys keys = Keys.None)
        {
            if (Keys.None == keys)
            {
                keys = ModifierKeys;
            }

            return ((keys & key) != 0);
        }

        /// <summary>
        ///   Set the time to a value that relates to a given position on the playhead area.
        ///   Current rendering offset and scale will be taken into account.
        /// </summary>
        /// <param name="location">The location on the playhead area.</param>
        private void SetTimeFromMousePosition(PointF location)
        {
            TimeSeconds = TimeAtScreenPosition(location.X);
            TimeChangedFromInput(this, new EventArgs());
        }

        /// <summary>
        /// Time at x position on screen
        /// Current rendering offset and scale will be taken into account.
        /// </summary>
        /// <param name="x">X coordinate on screen</param>
        /// <returns>Time at the given x coordinates</returns>
        private float TimeAtScreenPosition(float x)
        {
            Rectangle trackAreaBounds = GetTrackAreaBounds();
            // Calculate a clock value for the current X coordinate.
            return (x - _renderingOffset.X - trackAreaBounds.X) * (1 / _renderingScale.X);
        }

        /// <summary>
        /// Returns the acceptable deltaX based on the list of keyframes that are not being moved
        /// (k2 cannot go past k3 and cannot go before k1)
        /// </summary>
        /// <param name="deltaX">Wanted deltaX</param>
        /// <returns>Acceptable deltaX (maximum or normal value depending of initial deltaX)</returns>
        private float AcceptableMovingDelta(float deltaX)
        {
            //define maximum positive and negative deltas
            float maxAllowedPositiveDelta = float.PositiveInfinity;
            float maxAllowedNegativeDelta = float.NegativeInfinity;

            //get the temporary tracks
            foreach (TrackSurrogate track in _trackSurrogates)
            {
                //Get their references keyframes
                foreach (KeyFrame kf in track.KeyFrames.Select(x => ((KeyFrameSurrogate)x).SubstituteFor).Intersect(_selectedKeyFrames))
                {
                    
                    //for the real keyframes
                    int indexKF = track.SubstituteFor.KeyFrames.IndexOf(kf);
                    float tempNegative;
                    float tempPositive;

                    //if not ending keyframe
                    if (indexKF != track.SubstituteFor.KeyFrames.Count - 1)
                    {
                        //Do not check for keyframes that are also in selection
                        if (!_selectedKeyFrames.Contains(track.SubstituteFor.KeyFrames[indexKF + 1]))
                        {
                            //calculate next max positive movement
                            tempPositive = track.SubstituteFor.KeyFrames[indexKF + 1].T - kf.T - MIN_TIME_BETWEEN_KEYFRAMES;
                            //store it if it is less than previous max
                            if (maxAllowedPositiveDelta > tempPositive)
                                maxAllowedPositiveDelta = tempPositive;
                        }
                    }

                    //if not starting keyframe
                    if (indexKF != 0)
                    {
                        //Do not check for keyframes that are also in selection
                        if (!_selectedKeyFrames.Contains(track.SubstituteFor.KeyFrames[indexKF - 1]))
                        {
                            //calculate next max negative movement
                            tempNegative = track.SubstituteFor.KeyFrames[indexKF - 1].T - kf.T + MIN_TIME_BETWEEN_KEYFRAMES;
                            //store it if it is less than previous max
                            if (maxAllowedNegativeDelta < tempNegative)
                                maxAllowedNegativeDelta = tempNegative;
                        }
                    }
                }
            }
            if (deltaX < maxAllowedNegativeDelta)
            {
                return maxAllowedNegativeDelta;
            }
            else if (deltaX > maxAllowedPositiveDelta)
            {
                return maxAllowedPositiveDelta;
            }
            else
            {
                return deltaX;
            }
        }

        #endregion

        #region Drawing Methods
        /// <summary>
        ///   Redraws the timeline.
        ///   Should only be called from WM_PAINT aka OnPaint.
        /// </summary>
        private void Redraw(Graphics graphics)
        {
            // Clear the buffer
            graphics.Clear(BackgroundColor);

            DrawBackground(graphics);
            Draw_tracksKeyFrames(_tracks, graphics);
            Draw_tracksKeyFrames(_trackSurrogates, graphics);

            // Draw labels after the tracks to draw over elements that are partially moved out of the viewing area
            DrawTrackLabels(graphics);

            DrawPlayhead(graphics);

            ScrollbarH.Refresh();
            ScrollbarV.Refresh();
        }

        /// <summary>
        ///   Draws the background of the control.
        /// </summary>
        private void DrawBackground(Graphics graphics)
        {
            Rectangle trackAreaBounds = GetTrackAreaBounds();

            // Draw horizontal grid.
            // Grid is white so just take the alpha as the white value.
            Pen gridPen = new Pen(Color.FromArgb(GridAlpha, GridAlpha, GridAlpha));
            // Calculate the Y position of the first line.
            int firstLineY = (int)(TrackHeight * _renderingScale.Y);
            // Calculate the distance between each following line.
            int actualRowHeight = (int)((TrackHeight) * _renderingScale.Y + TrackSpacing);
            actualRowHeight = Math.Max(1, actualRowHeight);

            int firstLineYOffset = (int)(_renderingOffset.Y % firstLineY);

            // Draw the actual lines.
            for (int y = firstLineY + firstLineYOffset; y < Height; y += actualRowHeight)
            {
                graphics.DrawLine(gridPen, trackAreaBounds.X, trackAreaBounds.Y + y, trackAreaBounds.Width, trackAreaBounds.Y + y);
            }

            // The distance between the minor ticks.
            float minorTickDistance = _renderingScale.X;
            int minorTickOffset = (int)(_renderingOffset.X % minorTickDistance);

            // The distance between the regular ticks.
            int tickDistance = (int)(10f * _renderingScale.X);
            tickDistance = Math.Max(1, tickDistance);

            // The distance between minute ticks
            int minuteDistance = tickDistance * 6;

            // Draw a vertical grid. Every 10 ticks, we place a line.
            int tickOffset = (int)(_renderingOffset.X % tickDistance);
            int minuteOffset = (int)(_renderingOffset.X % minuteDistance);

            // Calculate the distance between each column line.
            int columnWidth = (int)(10 * _renderingScale.X);
            columnWidth = Math.Max(1, columnWidth);

            // Should we draw minor ticks?
            if (minorTickDistance > 5.0f)
            {
                using (Pen minorGridPen = new Pen(Color.FromArgb(30, 30, 30))
                {
                    DashStyle = DashStyle.Dot
                })
                {
                    for (float x = minorTickOffset + minorTickDistance; x < Width; x += minorTickDistance)
                    {
                        graphics.DrawLine(minorGridPen, trackAreaBounds.X + x, trackAreaBounds.Y, trackAreaBounds.X + x, trackAreaBounds.Height);
                    }
                }
            }

            // We start one tick distance after the offset to draw the first line that is actually in the display area
            // The one that is only tickOffset pixels away it behind the track labels.
            int minutePenColor = (int)(255 * Math.Min(255, GridAlpha * 2) / 255f);
            Pen brightPen = new Pen(Color.FromArgb(minutePenColor, minutePenColor, minutePenColor));

            //Format for time labels
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            for (int x = tickOffset + tickDistance; x < Width; x += columnWidth)
            {
                // Every 60 ticks, we put a brighter, thicker line.
                Pen penToUse;
                if ((x - minuteOffset) % minuteDistance == 0)
                {
                    penToUse = brightPen;
                }
                else
                {
                    penToUse = gridPen;
                }

                //Draw time above the grid
                int time = (int)Math.Round(TimeAtScreenPosition(x + trackAreaBounds.X));
                float emsize = EmHeightForLabel(time.ToString(), _playheadExtents.Height);

                graphics.DrawString(time.ToString(), new Font(DefaultFont.FontFamily, emsize), new SolidBrush(penToUse.Color), new PointF(trackAreaBounds.X + x, _playheadExtents.Height / 2), sf);
                graphics.DrawLine(penToUse, trackAreaBounds.X + x, trackAreaBounds.Y, trackAreaBounds.X + x, trackAreaBounds.Height);
            }

            gridPen.Dispose();
            brightPen.Dispose();
        }

        /// <summary>
        /// Draw the Keyframes of the tracks
        /// </summary>
        /// <param name="tracks">_tracks of which to draw the keyframes of</param>
        /// <param name="graphics">Graphics to draw onto</param>
        private void Draw_tracksKeyFrames(IEnumerable<ITimelineTrack> tracks, Graphics graphics)
        {
            // Generate colors for the tracks.
            List<Color> colors = ColorHelper.GetRandomColors(_tracks.Count);

            Rectangle trackAreaBounds = GetTrackAreaBounds();

            foreach (ITimelineTrack track in tracks)
            {
                

                //lifetime rectangle for coloring the liftime of the object
                int trackIndex = TrackIndexForTrack(track);
                RectangleF lifetime = BoundsHelper.GetTrackLifetimeExtents(track, this);

                //Draw in transparency if the track to draw is a temporary one
                byte transparency = (track is TrackSurrogate) ? (byte)(byte.MaxValue / 2) : byte.MaxValue;

                Brush kfBrush = new SolidBrush(Color.FromArgb(transparency, colors[trackIndex].R / 2, colors[trackIndex].G / 2, colors[trackIndex].B / 2));
                Brush lifetimeBrush = new SolidBrush(Color.FromArgb(transparency, colors[trackIndex]));

                // Don't draw track elements that aren't within the target area.
                if (!lifetime.IntersectsWith(trackAreaBounds))
                {
                    continue;
                }

                //Draw lifetime
                graphics.FillRectangle(lifetimeBrush, lifetime);

                //Draw every keyframe of track
                foreach (IKeyFrame kf in track.KeyFrames)
                {
                    //rectangle to draw keyframes
                    RectangleF kfRectangle = BoundsHelper.GetKeyFrameExtents(track, kf, this);
                    //change color to be darker and a bit transparent

                    graphics.FillRectangle(kfBrush, kfRectangle);

                    //if kf is selected draw border
                    if (_selectedKeyFrames.Contains(kf))
                    {
                        //Take in account border size when drawing
                        kfRectangle.X += KeyFrameBorderWidth / 2;
                        kfRectangle.Y += KeyFrameBorderWidth / 2;
                        kfRectangle.Width -= KeyFrameBorderWidth / 2;
                        kfRectangle.Height -= KeyFrameBorderWidth / 2;

                        graphics.DrawRectangle(new Pen(Color.WhiteSmoke, KeyFrameBorderWidth), kfRectangle.ToRectangle());
                    }
                }
            }
        }

        /// <summary>
        ///   Draw the labels next to each track.
        /// </summary>
        private void DrawTrackLabels(Graphics graphics)
        {
            RectangleF trackAreaBounds = GetTrackAreaBounds();

            foreach (ITimelineTrack track in _tracks)
            {
                // We just need the height and Y-offset, so we get the extents of the first track
                RectangleF trackExtents = BoundsHelper.GetTrackExtents(track, this);
                RectangleF labelRect = new RectangleF(0, trackExtents.Y, TrackLabelWidth, trackExtents.Height);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), labelRect);
                graphics.DrawString(track.Name, _labelFont, Brushes.LightGray, labelRect);
            }
        }

        /// <summary>
        ///   Draw a playhead on the timeline.
        ///   The playhead indicates a time value.
        /// </summary>
        private void DrawPlayhead(Graphics graphics)
        {
            // Calculate the position of the playhead.
            Rectangle trackAreaBounds = GetTrackAreaBounds();

            // Draw a background for the playhead. This also overdraws elements that drew into the playhead area.
            //graphics.FillRectangle(Brushes.Black, 0, 0, Width, _playheadExtents.Height);

            float playheadOffset = (float)(trackAreaBounds.X + _timeSeconds * _renderingScale.X) + _renderingOffset.X;
            // Don't draw when not in view.
            if (playheadOffset < trackAreaBounds.X || playheadOffset > trackAreaBounds.X + trackAreaBounds.Width)
            {
                return;
            }

            // Draw the playhead as a single line.
            graphics.DrawLine(Pens.SpringGreen, playheadOffset, trackAreaBounds.Y, playheadOffset, trackAreaBounds.Height);

            graphics.FillRectangle(Brushes.SpringGreen, playheadOffset - _playheadExtents.Width / 2, 0, _playheadExtents.Width, _playheadExtents.Height);
        }
        #endregion

        #region Event Handler
        /// <summary>
        ///   Invoked when the control is repainted
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Redraw(e.Graphics);
        }

        /// <summary>
        ///   Invoked when the cursor is moved over the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Store the current mouse position.
            PointF location = new PointF(e.X, e.Y);
            // Check if there is a track at the current mouse position.
            IKeyFrame focusedKeyFrame = KeyFrameHitTest(location);
            ITimelineTrack focusedTrack = TrackHitTest(location);

            // Is the left mouse button pressed?
            if ((e.Button & MouseButtons.Left) != 0)
            {
                if (CurrentMode == BehaviorMode.MovingSelection)
                {
                    // Indicate ability to move though cursor.
                    Cursor = Cursors.SizeWE;

                    // Calculate the movement delta.
                    float deltaX = TimeAtScreenPosition(location.X) - TimeAtScreenPosition(_dragOrigin.X);
                    deltaX = AcceptableMovingDelta(deltaX);

                    // Apply the delta to all temporary keyframes
                    foreach (KeyFrameSurrogate surrogateKF in _keyframeSurrogates)
                    {
                        // Calculate the proposed new start for the track depending on the given delta.
                        float newPos = Math.Max(0, surrogateKF.SubstituteFor.T + deltaX);

                        // Snap to next full value
                        if (IsKeyDown(Keys.Alt))
                        {
                            newPos = (float)Math.Round(newPos);
                        }

                        surrogateKF.T = newPos;
                    }

                    // Force a redraw.
                    Invalidate();
                }
                else if (CurrentMode == BehaviorMode.RequestMovingSelection)
                {
                    // A previous action would like a dragging operation to start.

                    // Calculate the movement delta.
                    PointF delta = PointF.Subtract(location, new SizeF(_dragOrigin));

                    // Check if the user has moved the mouse far enough to trigger the dragging operation.
                    if (Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y) > DraggingThreshold)
                    {
                        CurrentMode = BehaviorMode.MovingSelection;

                        // Create and store surrogates for selected keyframes and tracks affected.
                        foreach (IKeyFrame kf in _selectedKeyFrames)
                        {
                            _trackSurrogates.Add(new TrackSurrogate(_tracks[TrackIndexForKeyFrame(kf)]));
                        }

                        // Remove duplicates in list
                        _trackSurrogates = _trackSurrogates.Distinct().ToList();

                        //have to do a second pass to reference only the selected keyframes's surrogates in the list
                        foreach (IKeyFrame kf in _selectedKeyFrames)
                        {
                            foreach (ITimelineTrack track in _trackSurrogates)
                            {
                                IKeyFrame kfToList = track.KeyFrames.Where(x => ((KeyFrameSurrogate)x).SubstituteFor == kf).FirstOrDefault();
                                if (kfToList != null)
                                {
                                    _keyframeSurrogates.Add(kfToList);
                                }
                            }
                        }
                    }
                }
                else if (CurrentMode == BehaviorMode.TimeScrub)
                {
                    SetTimeFromMousePosition(location);
                    Invalidate();
                }

            }
            else if ((e.Button & MouseButtons.Middle) != 0)
            {
                // Pan the view
                // Calculate the movement delta.
                PointF delta = PointF.Subtract(location, new SizeF(_panOrigin));
                // Now apply the delta to the rendering offsets to pan the view.
                _renderingOffset = PointF.Add(_renderingOffsetBeforePan, new SizeF(delta));

                // Make sure to stay within bounds.
                _renderingOffset.X = Math.Max(-ScrollbarH.Max, Math.Min(0, _renderingOffset.X));
                _renderingOffset.Y = Math.Max(-ScrollbarV.Max, Math.Min(0, _renderingOffset.Y));

                // Update scrollbar positions. This will invoke a redraw.
                ScrollbarH.Value = (int)(-_renderingOffset.X);
                ScrollbarV.Value = (int)(-_renderingOffset.Y);
            }
            else
            {
                // No mouse button is being pressed
                // and a keyframe is focused
                if (null != focusedKeyFrame)
                {
                    //display to user keyframe is moveable
                    Cursor = Cursors.SizeWE;
                }
                else
                {
                    //reset cursor
                    Cursor = Cursors.Arrow;
                }
            }
        }

        /// <summary>
        ///   Invoked when the user presses a mouse button over the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Store the current mouse position.
            PointF location = new PointF(e.X, e.Y);
            // Hide changing position context menu
            grbChangePos.Visible = false;

            if ((e.Button & MouseButtons.Left) != 0)
            {
                // Check if there is a keyframe at the current mouse position.
                IKeyFrame focusedKeyFrame = KeyFrameHitTest(location);

                if (null != focusedKeyFrame)
                {
                    // Was this keyframe already selected?
                    if (!_selectedKeyFrames.Contains(focusedKeyFrame))
                    {
                        // Clear the selection, unless the user is picking
                        if (!IsKeyDown(Keys.Control))
                        {
                            _selectedKeyFrames.Clear();
                        }

                        // Add keyframe to selection
                        _selectedKeyFrames.Add(focusedKeyFrame);
                    }
                    // If the keyframe was already selected and Ctrl is down
                    // then the user is picking and we want to remove the keyframe from the selection
                    else if (IsKeyDown(Keys.Control))
                    {
                        _selectedKeyFrames.Remove(focusedKeyFrame);
                    }

                    // Store the current mouse position. It'll be used later to calculate the movement delta.
                    _dragOrigin = location;

                    CurrentMode = BehaviorMode.RequestMovingSelection;
                }
                else if (location.Y < _playheadExtents.Height)
                {
                    CurrentMode = BehaviorMode.TimeScrub;
                    SetTimeFromMousePosition(location);
                }
                else
                {
                    // Clear the selection, unless the user is picking
                    if (!IsKeyDown(Keys.Control))
                    {
                        _selectedKeyFrames.Clear();
                    }

                    CurrentMode = BehaviorMode.Selecting;
                }
            }
            else if ((e.Button & MouseButtons.Right) != 0)
            {
                // Check if there is a keyframe at the current mouse position.
                IKeyFrame focusedKeyFrame = KeyFrameHitTest(location);
                int trackIndex;

                //if there is a keyframe under the cursor
                if(focusedKeyFrame != null)
                {
                    //only add the focused keyframe to selected keyframes
                    _selectedKeyFrames.Clear();
                    _selectedKeyFrames.Add(focusedKeyFrame);

                    grbChangePos.Location = Point.Round(location);
                    grbChangePos.Show();

                    tbxChangePosX.Text = _selectedKeyFrames[0].Point.X.ToString();
                    tbxChangePosY.Text = _selectedKeyFrames[0].Point.Y.ToString();

                    //Change X and Y value of keyframe
                }
                else if ((trackIndex = TrackIndexAtPoint(location)) != -1)
                {
                    //Add KeyFrame at position
                    KeyFrame newKeyFrame = new KeyFrame();
                    newKeyFrame.T = TimeAtScreenPosition(location.X);

                    //search for position in list
                    int i = 0;
                    //i < Keyframes.Count to stop searching if at end of list
                    while(i < _tracks[trackIndex].KeyFrames.Count &&
                        _tracks[trackIndex].KeyFrames[i].T < newKeyFrame.T)
                    {
                        i++;
                    }

                    //Add Keyframe at position
                    IList<IKeyFrame> tmpList = _tracks[trackIndex].KeyFrames;
                    tmpList.Insert(i, newKeyFrame);
                    _tracks[trackIndex].KeyFrames = tmpList;

                    //Trigger selection modified event
                    SelectionModified(this, new SelectionModifiedEventArgs(_tracks[trackIndex].Yield(), newKeyFrame.Yield()));

                    RecalculateScrollbarBounds();
                }
            }
            else if ((e.Button & MouseButtons.Middle) != 0)
            {
                _panOrigin = location;
                _renderingOffsetBeforePan = _renderingOffset;
            }
            
            Invalidate();
        }

        /// <summary>
        ///   Invoked when the user releases the mouse cursor over the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Store the current mouse position.
            PointF location = new PointF(e.X, e.Y);

            if ((e.Button & MouseButtons.Left) != 0)
            {
                if (CurrentMode == BehaviorMode.Selecting)
                {
                    // Are we on the track label column?
                    int trackIndex = TrackLabelHitTest(location);
                    if (-1 < trackIndex)
                    {
                        ITimelineTrack track = _tracks[trackIndex];

                        foreach (KeyFrame kf in track.KeyFrames)
                        {
                            // Toggle track in and out of selection.
                            if (_selectedKeyFrames.Contains(kf))
                            {
                                _selectedKeyFrames.Remove(kf);
                            }
                            else
                            {
                                _selectedKeyFrames.Add(kf);
                            }
                        }

                        //notify that whole track was selected
                        TrackSelected(this, new TrackSelectionEventsArgs(track));
                    }
                }
                else if (CurrentMode == BehaviorMode.MovingSelection)
                {
                    List<IKeyFrame> modifiedKeyFrames = new List<IKeyFrame>();
                    List<ITimelineTrack> modified_tracks = _trackSurrogates.Select(x => ((TrackSurrogate)x).SubstituteFor).ToList();
                    // The moving operation ended, apply the values of the surrogates to the originals
                    foreach (KeyFrameSurrogate surrogate in _keyframeSurrogates)
                    {
                        surrogate.CopyTo(surrogate.SubstituteFor);
                        modifiedKeyFrames.Add(surrogate.SubstituteFor);
                    }
                    //Trigger selection modified
                    SelectionModified(this, new SelectionModifiedEventArgs(modified_tracks, modifiedKeyFrames));

                    _trackSurrogates.Clear();
                    _keyframeSurrogates.Clear();

                    RecalculateScrollbarBounds();
                }

                // Reset cursor
                Cursor = Cursors.Arrow;
                // Reset mode.
                CurrentMode = BehaviorMode.Idle;
            }
            else if ((e.Button & MouseButtons.Middle) != 0)
            {
                _panOrigin = PointF.Empty;
                _renderingOffsetBeforePan = PointF.Empty;
            }

            Invalidate();
        }
        #endregion

        #region Scrolling
        /// <summary>
        ///   Invoked when the vertical scrollbar is being scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollbarVScroll(object sender, ScrollEventArgs e)
        {
            _renderingOffset.Y = -e.NewValue;
            Invalidate();
        }

        /// <summary>
        ///   Invoked when the horizontal scrollbar is being scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollbarHScroll(object sender, ScrollEventArgs e)
        {
            _renderingOffset.X = -e.NewValue;
            Invalidate();
        }

        /// <summary>
        ///   Invoked when the user scrolls the mouse wheel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (IsKeyDown(Keys.Alt))
            {
                // If Alt is down, we're zooming.
                float amount = e.Delta / ZoomBalast;
                Rectangle trackAreaBounds = GetTrackAreaBounds();

                if (IsKeyDown(Keys.Control))
                {
                    // If Ctrl is down as well, we're zooming horizontally.
                    _renderingScale.X += amount;
                    // Don't zoom below 10%
                    _renderingScale.X = Math.Max(0.1f, _renderingScale.X);

                    // We now also need to move the rendering offset so that the center of focus stays at the mouse cursor.
                    _renderingOffset.X -= trackAreaBounds.Width * ((e.Location.X - trackAreaBounds.X) / (float)trackAreaBounds.Width) * amount;
                    _renderingOffset.X = Math.Min(0, _renderingOffset.X);

                    // Update scrollbar position.
                    ScrollbarH.Value = (int)(-_renderingOffset.X);

                }
                else
                {
                    // If Ctrl isn't  down, we're zooming vertically.
                    _renderingScale.Y += amount;
                    // Don't zoom below 10%
                    _renderingScale.Y = Math.Max(0.1f, _renderingScale.Y);

                    // We now also need to move the rendering offset so that the center of focus stays at the mouse cursor.
                    _renderingOffset.Y -= trackAreaBounds.Height * ((e.Location.Y - trackAreaBounds.Y) / (float)trackAreaBounds.Height) * amount;
                    _renderingOffset.Y = Math.Min(0, _renderingOffset.Y);

                    // Update scrollbar position.
                    ScrollbarV.Value = (int)(-_renderingOffset.Y);
                }
                RecalculateScrollbarBounds();

            }
            else
            {
                // If Alt isn't down, we're scrolling/panning.
                if (IsKeyDown(Keys.Control))
                {
                    // If Ctrl is down, we're scrolling horizontally.
                    ScrollbarH.Value -= e.Delta / 10;
                }
                else
                {
                    // If no modifier keys are down, we're scrolling vertically.
                    ScrollbarV.Value -= e.Delta / 10;
                }
            }

            Invalidate();
        }

        /// <summary>
        /// User has pressed a key in a position textbox
        /// </summary>
        private void TbxPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Handle non numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// When tbxChangePosX's text changed
        /// </summary>
        private void tbxChangePosX_TextChanged(object sender, EventArgs e)
        {
            //only one keyframe is selected to arrive at this point
            PointF tmpPoint = new PointF(_selectedKeyFrames[0].Point.X, _selectedKeyFrames[0].Point.Y);

            //Make modifications of X
            if (tbxChangePosX.Text != "")
                tmpPoint.X = Convert.ToSingle(tbxChangePosX.Text);
            else
                tmpPoint.X = 0;

            _selectedKeyFrames[0].Point = tmpPoint;

            //Trigger event of selection modified
            int trackIndex = TrackIndexForKeyFrame(_selectedKeyFrames[0]);
            SelectionModified(this, new SelectionModifiedEventArgs(_tracks[trackIndex].Yield(), _selectedKeyFrames));
        }

        /// <summary>
        /// When tbxChangePosY's text changed
        /// </summary>
        private void tbxChangePosY_TextChanged(object sender, EventArgs e)
        {
            //only one keyframe is selected to arrive at this point
            PointF tmpPoint = _selectedKeyFrames[0].Point;

            //Make modifications of Y
            if (tbxChangePosY.Text != "")
                tmpPoint.Y = Convert.ToSingle(tbxChangePosY.Text);
            else
                tmpPoint.Y = 0;

            _selectedKeyFrames[0].Point = tmpPoint;

            //Trigger event of selection modified
            int trackIndex = TrackIndexForKeyFrame(_selectedKeyFrames[0]);
            SelectionModified(this, new SelectionModifiedEventArgs(_tracks[trackIndex].Yield(), _selectedKeyFrames));
        }

        /// <summary>
        /// When user pressed on the delete button
        /// </summary>
        private void btnDeleteKeyFrame_Click(object sender, EventArgs e)
        {
            int trackIndex = TrackIndexForKeyFrame(_selectedKeyFrames[0]);

            //Remove Keyframe from list and update it
            IList<IKeyFrame> tmpList = _tracks[trackIndex].KeyFrames;
            tmpList.Remove(_selectedKeyFrames[0]);
            _tracks[trackIndex].KeyFrames = tmpList;

            //Trigger event of selection modified
            SelectionDeleted(this, new SelectionDeletedEventArgs(_tracks[trackIndex].Yield(), _selectedKeyFrames));

            //Hide context menu
            grbChangePos.Visible = false;

            Invalidate();
        }
        #endregion
    }
}