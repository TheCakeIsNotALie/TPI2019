using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Polygon : AnimatedObject
    {
        
        private PointF[] _corners;

        //Stored in relative position to keyframe
        /// <summary>
        /// Position of corners relative to keyframe
        /// </summary>
        [Category("Properties")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PointF[] Corners { get => _corners;
            set {
                Size = GetSizeFromCorners(value); //recalculate size
                _corners = value;
            }
        }

        /// <summary>
        /// Creates a new instance of polygon
        /// </summary>
        /// <param name="keyFrames">Keyframes that the polygon will follow over time</param>
        /// <param name="corners">Corners of polygon relative to keyframe</param>
        /// <param name="zOrder">Order to draw the polygon</param>
        public Polygon(List<KeyFrame> keyFrames, PointF[] corners, int zOrder) : base(keyFrames, GetSizeFromCorners(corners), zOrder)
        {
            Corners = corners;
        }

        /// <summary>
        /// Draws a polygon
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time (seconds)</param>
        public override void Paint(Graphics g, float t)
        {
            g.FillPolygon(Brushes.Black, GetAbsolutePosCorners(Position(t)));
        }

        /// <summary>
        /// Get the absolute position of corners based on center position
        /// </summary>
        /// <param name="center">Center of polygon</param>
        private PointF[] GetAbsolutePosCorners(PointF center)
        {
            PointF[] absolutePosCorners = new PointF[Corners.Length];

            for (int i = 0; i < Corners.Length; i++)
            {
                absolutePosCorners[i] = new PointF(center.X + Corners[i].X, center.Y + Corners[i].Y);
            }

            return absolutePosCorners;
        }

        /// <summary>
        /// Get the size of object based on points separation
        /// </summary>
        /// <param name="corners">Absolute coordinates points</param>
        private static SizeF GetSizeFromCorners(PointF[] corners)
        {
            //Start the search by the first point values
            float minY = corners[0].Y, 
                minX = corners[0].X, 
                maxY = corners[0].Y, 
                maxX = corners[0].X;

            //Start at 1 since it's value are already used
            for(int i = 1; i < corners.Length; i++)
            {
                //Search for X (else if to optimise search time)
                if (corners[i].X < minX)
                    minX = corners[i].X;
                else if (corners[i].X > maxX)
                    maxX = corners[i].X;

                //Search for Y (else if to optimise search time)
                if (corners[i].Y < minY)
                    minY = corners[i].Y;
                else if (corners[i].Y > maxY)
                    maxY = corners[i].Y;
            }

            //return computed size
            return new SizeF(maxX - minX, maxY - minY);
        }
    }
}
