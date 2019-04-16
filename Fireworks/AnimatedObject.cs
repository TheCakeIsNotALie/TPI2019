using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using KeyFrames;
using Timeline;

namespace Fireworks
{
    /// <summary>
    /// Abstract class for animated objects
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class AnimatedObject : ITimelineTrack
    {
        //Every points and time at which they pass it at is in the keyframes list
        //Keyframes have to be in order
        private List<KeyFrame> _keyFrames;
        private SizeF _size;
        private string _name;
        private int _zOrder;

        /// <summary>
        /// Order to draw the object
        /// </summary>
        [Category("Properties")]
        [Description("Order to draw the object in")]
        [DisplayName("Z-Order")]
        public int ZOrder { get => _zOrder; set => _zOrder = value;  }

        /// <summary>
        /// KeyFrames that the object will follow over time
        /// </summary>
        [Browsable(false)]
        public List<KeyFrame> Keyframes { get => _keyFrames; set => _keyFrames = value; }

        /// <summary>
        /// Size of rectangle around the object
        /// </summary>
        [Category("Properties")]
        [Description("Size of rectangle around the object")]
        [DisplayName("Size")]
        [TypeConverter(typeof(ValueTypeTypeConverter))]
        public SizeF Size { get => _size; set => _size = value; }
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Implement ITimeLineTrack from keyframe list
        /// </summary>
        [Browsable(false)]
        public IList<IKeyFrame> KeyFrames { get => _keyFrames.ToList<IKeyFrame>();
            set => _keyFrames =  value.Cast<KeyFrame>().ToList(); }

        /// <summary>
        /// Basic constructor for animated objects
        /// </summary>
        /// <param name="name">Name of the animated object</param>
        /// <param name="keyFrames">KeyFrames that the object will follow over time</param>
        /// <param name="size">Size of rectangle around the object</param>
        /// <param name="zOrder">Order to draw the object</param>
        public AnimatedObject(string name, IList<IKeyFrame> keyFrames, SizeF size, int zOrder)
        {
            KeyFrames = keyFrames;
            Size = size;
            Name = name;
            ZOrder = zOrder; 
        }

        /// <summary>W
        /// Tests if t is in lifetime of object
        /// </summary>
        /// <param name="t">Time t (seconds)</param>
        public bool IsTimeInLifeTime(float t)
        {
            return !(t < KeyFrames.First().T || t > KeyFrames.Last().T);
        }

        /// <summary>
        /// Get position of object at instant t
        /// </summary>
        /// <param name="t">Time t (seconds)</param>
        /// <returns>PointF where object is at t</returns>
        public PointF Position(float t)
        {
            //First keyframe that has it's time > t
            //means that it's the next KeyFrame to go to
            //If condition finds no match take last keyframe because it means object.time == t
            KeyFrame nextKeyFrame = (KeyFrame)KeyFrames.Where(x => x.T > t).DefaultIfEmpty(KeyFrames.Last()).First();
            KeyFrame currentKeyFrame = (KeyFrame)KeyFrames[KeyFrames.IndexOf(nextKeyFrame) - 1];

            //Compute percentage of travel done
            float percentageDone = (t-currentKeyFrame.T) / (nextKeyFrame.T-currentKeyFrame.T);

            //Compute difference between the two points
            PointF deltaPos = new PointF(nextKeyFrame.Point.X - currentKeyFrame.Point.X,
                nextKeyFrame.Point.Y - currentKeyFrame.Point.Y);

            //Return new position based on difference * percentage done
            return new PointF(currentKeyFrame.Point.X + deltaPos.X * percentageDone,
                currentKeyFrame.Point.Y + deltaPos.Y * percentageDone);
        }

        /// <summary>
        /// Get hitbox of object at instant t
        /// </summary>
        /// <param name="t">Time t (seconds)</param>
        public Rectangle HitBox(float t)
        {
            //Center rectangle draw point
            PointF upperleftPoint = Position(t);
            upperleftPoint.X -= Size.Width / 2;
            upperleftPoint.Y -= Size.Height / 2;
            
            return new Rectangle(Point.Round(upperleftPoint), System.Drawing.Size.Round(Size));
        }

        /// <summary>
        /// Draws object at instant t
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public abstract void Paint(Graphics g, float t);

        /// <summary>
        /// Draws objects itinerary and hitbox at instant t
        /// (Always draws itinerary but not hitbox)
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public virtual void PaintDebug(Graphics g, float t)
        {
            //Draw a line between every point in keyframes (-1 to count to avoid out of array error)
            for (int i = 0; i < KeyFrames.Count - 1; i++)
            {
                g.DrawLine(Pens.Black, KeyFrames[i].Point, KeyFrames[i+1].Point);
            }
            
            //Draw hitbox
            g.DrawRectangle(Pens.Lime, HitBox(t));
        }

        /// <summary>
        /// Update an animated object
        /// </summary>
        public virtual void Update()
        {
            //Normally do nothing
            return;
        }
    }
}
