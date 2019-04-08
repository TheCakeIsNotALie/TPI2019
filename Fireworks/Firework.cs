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
    class Firework : AnimatedObject
    {
        Brush _brush;
        float _radius;
        int _nbParticles;
        float _ttl;

        private Particle[] particles;

        /// <summary>
        /// Brush that will draw every particles
        /// </summary>
        [Category("Visuals")]
        [Description("Brush that will draw every particles")]
        [DisplayName("Brush")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Brush Brush
        {
            get => _brush;
            set
            {
                _brush = value;
                //Recreate particles
                GenerateParticles();
            }
        }

        /// <summary>
        /// Maximum radius the particles will go to
        /// </summary>
        [Category("Properties")]
        [Description("Maximum radius the particles will go to")]
        [DisplayName("Radius")]
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                //Recreate particles
                GenerateParticles();
            }
        }

        /// <summary>
        /// Number of particules
        /// </summary>
        [Category("Properties")]
        [Description("Number of particules")]
        [DisplayName("ParticulesNb")]
        public int NbParticles
        {
            get => _nbParticles;
            set
            {
                _nbParticles = value;
                //Recreate particles
                GenerateParticles();
            }
        }

        /// <summary>
        /// Time that the firework will live
        /// </summary>
        [Category("Properties")]
        [Description("Time until death")]
        [DisplayName("Time To Live")]
        public float TTL
        {
            get => _ttl; set
            {
                _ttl = value;
                //Recreate particles
                GenerateParticles();
            }
        }

        /// <summary>
        /// Creates a new instance of Firework
        /// </summary>
        /// <param name="brush">Brush to draw the fireworks particles with</param>
        /// <param name="startingKF">KeyFrame where the firework will start</param>
        /// <param name="radius">Radius of the firework</param>
        /// <param name="nbParticles">Number of particles the firework will generate</param>
        /// <param name="zOrder">The drawing order of the firework</param>
        /// <param name="ttl">Time to live (seconds from first keyframe)</param>
        public Firework(Brush brush, KeyFrame startingKF, float radius, int nbParticles, int zOrder, float ttl) :
            base(CreateKeyFrameList(startingKF, ttl), new SizeF(radius, radius), zOrder)
        {
            _brush = brush;
            _radius = radius;
            _nbParticles = nbParticles;
            _ttl = ttl;
            GenerateParticles();
        }

        /// <summary>
        /// Basic instance of firework
        /// </summary>
        public Firework() : this(Brushes.Black, new KeyFrame(), 10, 10, 0, 1)
        {

        }

        /// <summary>
        /// Generates particles based on the fireworks properties
        /// </summary>
        private void GenerateParticles()
        {
            //Create particles
            particles = new Particle[NbParticles];

            for (int i = 0; i < NbParticles; i++)
            {
                List<KeyFrame> particleKeyFrames = new List<KeyFrame>();
                particleKeyFrames.Add(KeyFrames[0]);

                //Get end point of particle that's on the fireworks outer radius
                PointF pointOnCircle = KeyFrames[0].Point;
                pointOnCircle.X += (float)(Radius * Math.Cos(i * (360f / NbParticles) * Math.PI / 180));
                pointOnCircle.Y += (float)(Radius * Math.Sin(i * (360f / NbParticles) * Math.PI / 180));

                particleKeyFrames.Add(new KeyFrame(pointOnCircle, KeyFrames[0].T + TTL));

                particles[i] = new Particle(Brush, particleKeyFrames, new SizeF(3, 3), ZOrder);
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
