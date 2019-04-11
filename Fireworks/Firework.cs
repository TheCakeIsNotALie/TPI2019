﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using KeyFrames;

namespace Fireworks
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    class Firework : AnimatedObject
    {
        Brush _brush;
        float _radius;
        int _nbParticles;

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
        /// Creates a new instance of Firework
        /// </summary>
        /// <param name="brush">Brush to draw the fireworks particles with</param>
        /// <param name="startingKF">KeyFrame where the firework will start</param>
        /// <param name="radius">Radius of the firework</param>
        /// <param name="nbParticles">Number of particles the firework will generate</param>
        /// <param name="zOrder">The drawing order of the firework</param>
        /// <param name="ttl">Time to live (seconds from first keyframe)</param>
        public Firework(string name, Brush brush, IList<IKeyFrame> keyFrames, float radius, int nbParticles, int zOrder) :
            base(name, keyFrames, new SizeF(radius, radius), zOrder)
        {
            _brush = brush;
            _radius = radius;
            _nbParticles = nbParticles;
            GenerateParticles();
        }

        /// <summary>
        /// Basic instance of firework
        /// </summary>
        public Firework() : this("Firework", Brushes.Black, KeyFrame.BasicKeyFrames, 10, 10, 0)
        {

        }

        /// <summary>
        /// Generates particles based on the fireworks properties
        /// </summary>
        private void GenerateParticles()
        {
            //Create particles
            particles = new Particle[NbParticles];

            float maxTime = KeyFrames.Last().T;

            for (int i = 0; i < NbParticles; i++)
            {
                List<IKeyFrame> particleKeyFrames = new List<IKeyFrame>();
                particleKeyFrames.Add((KeyFrame)KeyFrames[0]);

                //Get end point of particle that's on the fireworks outer radius
                PointF relativePosOnCircle = new PointF();
                relativePosOnCircle.X = (float)(Radius * Math.Cos(i * (360f / NbParticles) * Math.PI / 180));
                relativePosOnCircle.Y = (float)(Radius * Math.Sin(i * (360f / NbParticles) * Math.PI / 180));

                //Calculate the different points based on keyframes of firework
                for (int j = 1; j < KeyFrames.Count; j++)
                {
                    PointF tmpPoint = new PointF();

                    //Compute percentage of travel done at keyframe
                    float percentageDone = (Keyframes[j].T - KeyFrames[j-1].T) / (maxTime- KeyFrames[j - 1].T);

                    //Add the travel done of the point on circle to keyframe point
                    tmpPoint.X = relativePosOnCircle.X * percentageDone + Keyframes[j].Point.X;
                    tmpPoint.Y = relativePosOnCircle.Y * percentageDone + Keyframes[j].Point.Y;

                    particleKeyFrames.Add(new KeyFrame(tmpPoint, KeyFrames[j].T));
                }

                particles[i] = new Particle(Name + "-p" + i, Brush, particleKeyFrames, new SizeF(3, 3), ZOrder);
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
        /// Force update and regenerate particles
        /// </summary>
        public override void Update()
        {
            GenerateParticles();
        }

        /// <summary>
        /// Crea
        /// </summary>
        /// <param name="firstKeyFrame"></param>
        /// <param name="ttl"></param>
        private static IList<IKeyFrame> CreateKeyFrameList(KeyFrame firstKeyFrame, float ttl)
        {
            List<IKeyFrame> result = new List<IKeyFrame>();

            result.Add(firstKeyFrame);
            result.Add(new KeyFrame(firstKeyFrame.Point, firstKeyFrame.T + ttl));

            return result;
        }
    }
}
