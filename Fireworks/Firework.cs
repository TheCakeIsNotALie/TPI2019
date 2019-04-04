using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fireworks
{
    class Firework : AnimatedObject
    {
        private Particle[] particles;
        
        /// <summary>
        /// Creates a new instance of Firework
        /// </summary>
        /// <param name="startingKF">KeyFrame where the firework will start</param>
        /// <param name="radius">Radius of the firework</param>
        /// <param name="nbParticles">Number of particles the firework will generate</param>
        /// <param name="zOrder">The drawing order of the firework</param>
        /// <param name="ttl">Time to live (seconds from firs)</param>
        public Firework(KeyFrame startingKF, float radius, int nbParticles, int zOrder, float ttl) : 
            base(CreateKeyFrameList(startingKF, ttl), new SizeF(radius, radius), zOrder)
        {
            //Create particles
            particles = new Particle[nbParticles];

            for (int i = 0; i < nbParticles; i++)
            {
                List<KeyFrame> particleKeyFrames = new List<KeyFrame>();
                particleKeyFrames.Add(startingKF);

                //Get end point of particle that's on the fireworks outer radius
                PointF pointOnCircle = startingKF.Point;
                pointOnCircle.X += (float)(radius * Math.Cos(i * (360f / nbParticles) * Math.PI / 180));
                pointOnCircle.Y += (float)(radius * Math.Sin(i * (360f / nbParticles) * Math.PI / 180));

                particleKeyFrames.Add(new KeyFrame(pointOnCircle, startingKF.T + ttl));

                particles[i] = new Particle(particleKeyFrames, new SizeF(3,3), zOrder);
            }
        }

        /// <summary>
        /// Draw every particles in firework
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="t">Time t (seconds)</param>
        public override void Paint(Graphics g, float t)
        {
            foreach (Particle p in particles)
            {
                p.Paint(g, t);
            }
        }

        /// <summary>
        /// Draw every particles own PaintDebug
        /// </summary>
        /// <param name="g"></param>
        /// <param name="t"></param>
        public override void PaintDebug(Graphics g, float t)
        {
            foreach (Particle p in particles)
            {
                p.PaintDebug(g, t);
            }
        }

        /// <summary>
        /// Crea
        /// </summary>
        /// <param name="firstKeyFrame"></param>
        /// <param name="ttl"></param>
        private static List<KeyFrame> CreateKeyFrameList(KeyFrame firstKeyFrame, float ttl)
        {
            List<KeyFrame> result = new List<KeyFrame>();

            result.Add(firstKeyFrame);
            result.Add(new KeyFrame(firstKeyFrame.Point, firstKeyFrame.T + ttl));

            return result;
        }
    }
}
