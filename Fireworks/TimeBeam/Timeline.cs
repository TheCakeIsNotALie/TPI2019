﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TimeBeam.Events;
using TimeBeam.Helper;
using TimeBeam.Surrogates;
using TimeBeam.Timing;
using Fireworks;

namespace TimeBeam
{
    /// <summary>
    ///   The main host control.
    ///   Taken from : https://github.com/oliversalzburg/TimeBeam
    ///   and heavily modified for uses with keyframes
    /// </summary>
    public partial class Timeline : UserControl
    {
        /// <summary>
        ///   How far does the user have to move the mouse (while holding down the left mouse button) until dragging operations kick in?
        ///   Technically, this defines the length of the movement vector.
        /// </summary>
        private const float DraggingThreshold = 3f;

        #region Events
        /// <summary>
        ///   Invoked when the selection of track elements changed.
        ///   Inspect <see cref="SelectedTracks"/> to see the current selection.
        /// </summary>
        public EventHandler<SelectionChangedEventArgs> SelectionChanged;

        /// <summary>
        ///   Invoke the <see cref="SelectionChanged" /> event.
        /// </summary>
        /// <param name="eventArgs">The arguments to pass with the event.</param>
        private void InvokeSelectionChanged(SelectionChangedEventArgs eventArgs = null)
        {
            if (null != SelectionChanged)
            {
                SelectionChanged.Invoke(this, eventArgs ?? SelectionChangedEventArgs.Empty);
            }
        }
        #endregion


        #region Layout
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
        ///   Backing field for <see cref="TrackHeight" />.
        /// </summary>
        private int _trackHeight = 20;

        /// <summary>
        ///   How wide/high the border on a track item should be.
        ///   This border allows you to interact with an item.
        /// </summary>
        [Description("How wide/high the border on a track item should be.")]
        [Category("Layout")]
        public int TrackBorderSize
        {
            get { return _trackBorderSize; }
            set { _trackBorderSize = value; }
        }

        /// <summary>
        ///   Backing field for <see cref="TrackBorderSize" />.
        /// </summary>
        private int _trackBorderSize = 2;

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
        ///   Backing field for <see cref="TrackSpacing" />.
        /// </summary>
        private int _trackSpacing = 1;

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
        ///   Backing field for <see cref="TrackLabelWidth" />.
        /// </summary>
        private int _trackLabelWidth = 100;

        /// <summary>
        ///   The font to use to draw the track labels.
        /// </summary>
        private Font _labelFont = DefaultFont;

        /// <summary>
        ///   The size of the top part of the playhead.
        /// </summary>
        private SizeF _playheadExtents = new SizeF(5, 16);
        #endregion

        #region Drawing
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
        ///   Backing field for <see cref="BackgroundColor" />.
        /// </summary>
        private Color _backgroundColor = Color.Black;

        internal PointF RenderingOffset
        {
            get { return _renderingOffset; }
        }

        /// <summary>
        ///   When the timeline is scrolled (panned) around, this offset represents the panned distance.
        /// </summary>
        private PointF _renderingOffset = PointF.Empty;

        internal PointF RenderingScale
        {
            get { return _renderingScale; }
        }

        /// <summary>
        ///   The scale at which to render the timeline.
        ///   This enables us to "zoom" the timeline in and out.
        /// </summary>
        private PointF _renderingScale = new PointF(1, 1);

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

        /// <summary>
        ///   Backing field for <see cref="GridAlpha" />.
        /// </summary>
        private int _gridAlpha = 40;
        #endregion

        #region Time

        private float _time;

        /// <summary>
        /// Current timeline time
        /// </summary>
        public float TimeSeconds { get => _time; set => _time = value; }

        #endregion

        #region Tracks and KeyFrames

        private readonly List<IKeyFrame> _selectedKeyFrames = new List<IKeyFrame>();
        private List<IKeyFrame> _keyframeSurrogates = new List<IKeyFrame>();

        /// <summary>
        ///   The tracks currently placed on the timeline.
        /// </summary>
        private readonly List<ITimelineTrack> _tracks = new List<ITimelineTrack>();

        /// <summary>
        ///   The currently selected tracks.
        /// </summary>
        private readonly List<ITimelineTrack> _selectedTracks = new List<ITimelineTrack>();

        /// <summary>
        ///   Which tracks are currently selected?
        /// </summary>
        public IEnumerable<ITimelineTrack> SelectedTracks { get { return _selectedTracks; } }
        #endregion

        #region Interaction
        /// <summary>
        ///   What mode is the timeline currently in?
        /// </summary>
        public BehaviorMode CurrentMode { get; private set; }

        /// <summary>
        ///   The list of surrogates (stand-ins) for timeline tracks.
        ///   These surrogates are used as temporary placeholders during certain operations.
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
            ///   The user is resizing the selected tracks.
            /// </summary>
            ResizingSelection,

            /// <summary>
            ///   The user is almost moving selected items.
            /// </summary>
            RequestMovingSelection,

            /// <summary>
            ///   The user is almost resizing the selected tracks.
            /// </summary>
            RequestResizingSelection,

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
        public Timeline()
        {
            InitializeComponent();
            SetStyle(
              ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserPaint, true);

            // Set up the font to use to draw the track labels
            float emHeightForLabel = EmHeightForLabel("WM_g^~", TrackHeight);
            _labelFont = new Font(DefaultFont.FontFamily, emHeightForLabel - 2);
        }
        #endregion

        /// <summary>
        ///   Add a track to the timeline.
        /// </summary>
        /// <param name="track">The track to add.</param>
        public void AddTrack(ITimelineTrack track)
        {
            _tracks.Add(track);
            RecalculateScrollbarBounds();
            Invalidate();
        }

        #region Helpers
        /// <summary>
        ///   Recalculates appropriate values for scrollbar bounds.
        /// </summary>
        private void RecalculateScrollbarBounds()
        {
            ScrollbarV.Max = (int)((_tracks.Count * (TrackHeight + TrackSpacing)) * _renderingScale.Y);
            ScrollbarH.Max = (int)(_tracks.Max(t => t.KeyFrames.Last().T * _renderingScale.X));
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
        /// <param name="test">The point to test for.</param>
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

        private KeyFrame KeyFrameHitTest(PointF test)
        {
            foreach (ITimelineTrack track in _tracks)
            {
                foreach (KeyFrame kf in track.KeyFrames)
                {
                    // The extent of the track, including the border
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
        ///   Set the clock to a position that relates to a given position on the playhead area.
        ///   Current rendering offset and scale will be taken into account.
        /// </summary>
        /// <param name="location">The location on the playhead area.</param>
        private void SetClockFromMousePosition(PointF location)
        {
            Rectangle trackAreaBounds = GetTrackAreaBounds();
            // Calculate a clock value for the current X coordinate.
            float clockValue = (location.X - _renderingOffset.X - trackAreaBounds.X) * (1 / _renderingScale.X) * 1000f;
            _time = clockValue;
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
            DrawTracks(_tracks, graphics);
            DrawTracks(_trackSurrogates, graphics);

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
            int firstLineY = (int)(TrackHeight * _renderingScale.Y + trackAreaBounds.Y + _renderingOffset.Y);
            // Calculate the distance between each following line.
            int actualRowHeight = (int)((TrackHeight) * _renderingScale.Y + TrackSpacing);
            actualRowHeight = Math.Max(1, actualRowHeight);
            // Draw the actual lines.
            for (int y = firstLineY; y < Height; y += actualRowHeight)
            {
                graphics.DrawLine(gridPen, trackAreaBounds.X, y, trackAreaBounds.Width, y);
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
                    for (float x = minorTickOffset; x < Width; x += minorTickDistance)
                    {
                        graphics.DrawLine(minorGridPen, trackAreaBounds.X + x, trackAreaBounds.Y, trackAreaBounds.X + x, trackAreaBounds.Height);
                    }
                }
            }

            // We start one tick distance after the offset to draw the first line that is actually in the display area
            // The one that is only tickOffset pixels away it behind the track labels.
            int minutePenColor = (int)(255 * Math.Min(255, GridAlpha * 2) / 255f);
            Pen brightPen = new Pen(Color.FromArgb(minutePenColor, minutePenColor, minutePenColor));
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

                graphics.DrawLine(penToUse, trackAreaBounds.X + x, trackAreaBounds.Y, trackAreaBounds.X + x, trackAreaBounds.Height);
            }

            gridPen.Dispose();
            brightPen.Dispose();
        }

        /// <summary>
        ///   Draw a list of tracks onto the timeline.
        /// </summary>
        /// <param name="tracks">The tracks to draw.</param>
        private void DrawTracks(IEnumerable<ITimelineTrack> tracks, Graphics graphics)
        {

            Rectangle trackAreaBounds = GetTrackAreaBounds();

            // Generate colors for the tracks.
            List<Color> colors = ColorHelper.GetRandomColors(_tracks.Count);

            foreach (ITimelineTrack track in tracks)
            {
                // The extent of the track, including the border
                RectangleF trackExtent = BoundsHelper.GetTrackExtents(track, this);

                // Don't draw track elements that aren't within the target area.
                if (!trackAreaBounds.IntersectsWith(trackExtent.ToRectangle()))
                {
                    continue;
                }

                // The index of this track (or the one it's a substitute for).
                int trackIndex = TrackIndexForTrack(track);

                // Determine colors for this track
                Color trackColor = ColorHelper.AdjustColor(colors[trackIndex], 0, -0.1, -0.2);
                Color borderColor = Color.FromArgb(128, Color.Black);

                if (_selectedTracks.Contains(track))
                {
                    borderColor = Color.WhiteSmoke;
                }

                // Draw the main track area.
                if (track is TrackSurrogate)
                {
                    // Draw surrogates with a transparent brush.
                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, trackColor)), trackExtent);

                    foreach (KeyFrame kf in track.KeyFrames)
                    {
                        RectangleF kfExtent = BoundsHelper.GetKeyFrameExtents(track, kf, this);
                        graphics.FillEllipse(new SolidBrush(Color.FromArgb(128, Color.White)), kfExtent);
                    }
                }
                else
                {
                    graphics.FillRectangle(new SolidBrush(trackColor), trackExtent);

                    foreach (KeyFrame kf in track.KeyFrames)
                    {
                        RectangleF kfExtent = BoundsHelper.GetKeyFrameExtents(track, kf, this);
                        graphics.FillEllipse(new SolidBrush(Color.White), kfExtent);
                    }
                }

                // Compensate for border size
                trackExtent.X += TrackBorderSize / 2f;
                trackExtent.Y += TrackBorderSize / 2f;
                trackExtent.Height -= TrackBorderSize;
                trackExtent.Width -= TrackBorderSize;

                graphics.DrawRectangle(new Pen(borderColor, TrackBorderSize), trackExtent.X, trackExtent.Y, trackExtent.Width, trackExtent.Height);
            }
        }

        /// <summary>
        ///   Draw the labels next to each track.
        /// </summary>
        private void DrawTrackLabels(Graphics graphics)
        {
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
            graphics.FillRectangle(Brushes.Black, 0, 0, Width, _playheadExtents.Height);

            float playheadOffset = (float)(trackAreaBounds.X + (_time * 0.001f) * _renderingScale.X) + _renderingOffset.X;
            // Don't draw when not in view.
            if (playheadOffset < trackAreaBounds.X || playheadOffset > trackAreaBounds.X + trackAreaBounds.Width)
            {
                return;
            }

            // Draw the playhead as a single line.
            graphics.DrawLine(Pens.SpringGreen, playheadOffset, trackAreaBounds.Y, playheadOffset, trackAreaBounds.Height);

            graphics.FillRectangle(Brushes.SpringGreen, playheadOffset - _playheadExtents.Width / 2, 0, _playheadExtents.Width, _playheadExtents.Height);
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
        #endregion

        #region Event Handler
        /// <summary>
        ///   Invoked when the control is repainted
        /// </summary>
        /// <param name="e"></param>
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
                    PointF delta = PointF.Subtract(location, new SizeF(_dragOrigin));

                    // Apply the delta to all selected tracks
                    foreach (KeyFrameSurrogate selectedKF in _keyframeSurrogates)
                    {
                        // Calculate the proposed new start for the track depending on the given delta.
                        float newPos = Math.Max(0, selectedKF.SubstituteFor.T + (delta.X * (1 / _renderingScale.X)));
                        
                        // Snap to next full value
                        if (!IsKeyDown(Keys.Alt))
                        {
                            newPos = (float)Math.Round(newPos);
                        }

                        selectedKF.T = newPos;
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
                        // Start the requested dragging operation.
                        if (CurrentMode == BehaviorMode.RequestMovingSelection)
                        {
                            CurrentMode = BehaviorMode.MovingSelection;
                        }

                        // Create and store surrogates for selected timeline tracks.
                        _keyframeSurrogates = SurrogateHelper.GetSurrogates(_selectedKeyFrames);
                    }

                }
                else if (CurrentMode == BehaviorMode.TimeScrub)
                {
                    SetClockFromMousePosition(location);
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
                if (null != focusedKeyFrame)
                {
                    RectangleF kfExtents = BoundsHelper.GetKeyFrameExtents(focusedTrack, focusedKeyFrame, this);
                    RectangleHelper.Edge isPointOnEdge = RectangleHelper.IsPointOnEdge(kfExtents, location, 3f, RectangleHelper.EdgeTest.Horizontal);

                    // Select the appropriate cursor for the cursor position (Only west and east edges are valid).
                    switch (isPointOnEdge)
                    {
                        case RectangleHelper.Edge.Right:
                        case RectangleHelper.Edge.Left:
                            Cursor = Cursors.SizeWE;
                            break;
                        case RectangleHelper.Edge.None:
                            Cursor = Cursors.Arrow;
                            break;
                        default:
                            Cursor = Cursors.Arrow;
                            break;
                    }
                }
                else
                {
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

            if ((e.Button & MouseButtons.Left) != 0)
            {
                // Check if there is a track at the current mouse position.
                KeyFrame focusedKeyFrame = KeyFrameHitTest(location);

                if (null != focusedKeyFrame)
                {
                    // Was this track already selected?
                    if (!_selectedKeyFrames.Contains(focusedKeyFrame))
                    {
                        // Tell the track that it was selected.
                        InvokeSelectionChanged(new SelectionChangedEventArgs(focusedKeyFrame.Yield(), null));
                        // Clear the selection, unless the user is picking
                        if (!IsKeyDown(Keys.Control))
                        {
                            InvokeSelectionChanged(new SelectionChangedEventArgs(null, _selectedKeyFrames));
                            _selectedKeyFrames.Clear();
                        }

                        // Add track to selection
                        _selectedKeyFrames.Add(focusedKeyFrame);

                        // If the track was already selected and Ctrl is down
                        // then the user is picking and we want to remove the track from the selection
                    }
                    else if (IsKeyDown(Keys.Control))
                    {
                        _selectedKeyFrames.Remove(focusedKeyFrame);
                        InvokeSelectionChanged(new SelectionChangedEventArgs(null, focusedKeyFrame.Yield()));
                    }

                    // Store the current mouse position. It'll be used later to calculate the movement delta.
                    _dragOrigin = location;

                    CurrentMode = BehaviorMode.RequestMovingSelection;
                }
                else if (location.Y < _playheadExtents.Height)
                {
                    CurrentMode = BehaviorMode.TimeScrub;
                    SetClockFromMousePosition(location);
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
                                InvokeSelectionChanged(new SelectionChangedEventArgs(null, kf.Yield()));
                            }
                            else
                            {
                                _selectedKeyFrames.Add(kf);
                                InvokeSelectionChanged(new SelectionChangedEventArgs(kf.Yield(), null));
                            }
                        }

                    }
                }
                else if (CurrentMode == BehaviorMode.MovingSelection)
                {
                    // The moving operation ended, apply the values of the surrogates to the originals
                    foreach (KeyFrameSurrogate surrogate in _keyframeSurrogates)
                    {
                        surrogate.CopyTo(surrogate.SubstituteFor);
                    }
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




        /// <summary>
        ///   Invoked when a key is released.
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    base.OnKeyUp(e);

        //    if (e.KeyCode == Keys.A && IsKeyDown(Keys.Control))
        //    {
        //        // Ctrl+A - Select all
        //        InvokeSelectionChanged(new SelectionChangedEventArgs(null, _selectedTracks));
        //        _selectedTracks.Clear();
        //        foreach (ITimelineTrack track in _tracks.SelectMany(t => t.TrackElements))
        //        {
        //            _selectedTracks.Add(track);
        //        }
        //        InvokeSelectionChanged(new SelectionChangedEventArgs(_selectedTracks, null));
        //        Invalidate();

        //    }
        //    else if (e.KeyCode == Keys.D && IsKeyDown(Keys.Control))
        //    {
        //        // Ctrl+D - Deselect all
        //        InvokeSelectionChanged(new SelectionChangedEventArgs(null, _selectedTracks));
        //        _selectedTracks.Clear();
        //        Invalidate();
        //    }
        //}
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
                float amount = e.Delta / 1200f;
                Rectangle trackAreaBounds = GetTrackAreaBounds();

                if (IsKeyDown(Keys.Control))
                {
                    // If Ctrl is down as well, we're zooming horizontally.
                    _renderingScale.X += amount;
                    // Don't zoom below 1%
                    _renderingScale.X = Math.Max(0.01f, _renderingScale.X);

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
                    // Don't zoom below 1%
                    _renderingScale.Y = Math.Max(0.01f, _renderingScale.Y);

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
        #endregion
    }
}