using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    /// <summary>
    /// Abstraact class for animated objects
    /// </summary>
    public abstract class AnimatedObject
    {
        //Every points and time at which they pass it at is in the keyframes list
        //Keyframes have to be in order
        private List<KeyFrame> _keyFrames;
        private SizeF _size;
        private int _zOrder;

        /// <summary>
        /// Order to draw the object
        /// </summary>
        public int ZOrder { get => _zOrder; }

        /// <summary>
        /// KeyFrames that the object will follow
        /// </summary>
        public List<KeyFrame> KeyFrames { get => _keyFrames; set => _keyFrames = value; }

        public AnimatedObject(List<KeyFrame> keyFrames, SizeF size, int zOrder)
        {
            KeyFrames = keyFrames;
            _size = size;
            _zOrder = zOrder;
        }

        /// <summary>
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
            KeyFrame nextKeyFrame = KeyFrames.Where(x => x.T > t).First();
            KeyFrame currentKeyFrame = KeyFrames[KeyFrames.IndexOf(nextKeyFrame) - 1];

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
            upperleftPoint.X -= _size.Width / 2;
            upperleftPoint.Y -= _size.Height / 2;
            
            return new Rectangle(Point.Round(upperleftPoint), Size.Round(_size));
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
            
            //Draw hitbox if alive
            if (IsTimeInLifeTime(t))
                g.DrawRectangle(Pens.Lime, HitBox(t));
        }
    }
}
