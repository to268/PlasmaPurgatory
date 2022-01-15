using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory.Generator
{
    public class PatternPreset
    {
        public struct PolarProperties
        {
            public float startMagnitude;
            public float startPhase;
            public float incrementMagnitude;
            public float incrementPhase;
            public float multiplierMagnitude;
            public float multiplierPhase;
        }

        public enum PresetName
        {
            SPIRAL,
            CIRCLE,
            SHOTGUN,
            MANDELBROT_SPIRAL,
            MANDELBROT_STAR,
            MANDELBROT_DUAL_SPIRAL,
        }
        
        private readonly float LEFT_ANGLE = MathsUtils.DegresToRadians(105f);
        private readonly float MIDDLE_ANGLE = MathsUtils.DegresToRadians(90f);
        private readonly float RIGHT_ANGLE = MathsUtils.DegresToRadians(75f);
        private const int SHOTGUN_DIRECTIONS = 3;

        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private List<Bullet> bullets;
        private Vector2 origin;
        private int bulletCount;
        private PolarProperties polarProperties;
        private PresetName presetName;
        private Bullet.BulletProperties bulletProperties;

        private System.Numerics.Complex mandelbrotComplex;
        private float paddingMultiplier;

        public List<Bullet> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        public PatternPreset(PresetName presetName, Bullet.BulletProperties bulletProperties,
                             ContentManager contentManager, GraphicsDevice graphicsDevice, Vector2 origin, 
                             int bulletCount)
        {
            this.presetName = presetName;
            this.bulletProperties = bulletProperties;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.origin = origin;
            this.bulletCount = bulletCount;

            bullets = new List<Bullet>();
            FillMandelbrotPresetData();
        }

        public PatternPreset(PresetName presetName, PolarProperties polarProperties, 
                             Bullet.BulletProperties bulletProperties, ContentManager contentManager,
                             GraphicsDevice graphicsDevice, Vector2 origin, 
                             int bulletCount)
        {
            this.presetName = presetName;
            this.polarProperties = polarProperties;
            this.bulletProperties = bulletProperties;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.origin = origin;
            this.bulletCount = bulletCount;

            bullets = new List<Bullet>();
        }

        public void ApplyPattern()
        {
            switch (presetName)
            {
                case PresetName.SPIRAL:
                    SpiralCalculaton();
                    break;
                
                case PresetName.CIRCLE:
                    CircleCalculation();
                    break;
                
                case PresetName.SHOTGUN:
                    ShotgunCalculation();
                    break;

                case PresetName.MANDELBROT_SPIRAL:
                case PresetName.MANDELBROT_STAR:
                case PresetName.MANDELBROT_DUAL_SPIRAL:
                    MandelbrotPointsCalculation();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException($"Unknown pattern preset");
            }
        }

        private void FillMandelbrotPresetData()
        {
            switch (presetName)
            {
                // Enforce some values to avoid an unexpected crash in all Mandelbrot pattern
                case PresetName.MANDELBROT_SPIRAL:
                    bulletCount = 80;
                    paddingMultiplier = 7.1f;

                    mandelbrotComplex = new System.Numerics.Complex(-0.05, -0.63);
                    break;

                case PresetName.MANDELBROT_STAR:
                    bulletCount = 40;
                    paddingMultiplier = 12;

                    mandelbrotComplex = new System.Numerics.Complex(-0.28, -0.59);
                    break;

                case PresetName.MANDELBROT_DUAL_SPIRAL:
                    bulletCount = 20;
                    paddingMultiplier = 24;

                    mandelbrotComplex = new System.Numerics.Complex(-0.61, -0.18);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"Unknown mandelbrot pattern preset");
            }
        }

        private void SpiralCalculaton()
        {
            MathsUtils.Polar polar = new MathsUtils.Polar();
            polar.magnitude = polarProperties.startMagnitude;
            polar.phase = polarProperties.startPhase;

            Vector2 bulletOrigin = MathsUtils.PolarToVector(polar);
            bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                            CenterPolarPoint(bulletOrigin), bulletProperties));

            for (int i = 1; i < bulletCount; i++)
            {
                polar.magnitude += polarProperties.incrementMagnitude * polarProperties.multiplierMagnitude;
                polar.phase += polarProperties.incrementPhase * polarProperties.multiplierPhase;

                Vector2 point = MathsUtils.PolarToVector(polar);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                CenterPolarPoint(point), bulletProperties));
            }
        }

        private void CircleCalculation()
        {
            MathsUtils.Polar polar = new MathsUtils.Polar();
            polar.magnitude = polarProperties.startMagnitude;
            polar.phase = polarProperties.startPhase;
            
            float phasePerBullet = MathsUtils.DegresToRadians(360f / bulletCount);
            
            Vector2 bulletOrigin = MathsUtils.PolarToVector(polar);
            bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                            CenterPolarPoint(bulletOrigin), bulletProperties));

            for (int i = 1; i < bulletCount; i++)
            {
                polar.phase += phasePerBullet;

                Vector2 point = MathsUtils.PolarToVector(polar);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                CenterPolarPoint(point), bulletProperties));
            }
        }

        private void ShotgunCalculation()
        {
            MathsUtils.Polar polar = new MathsUtils.Polar();
            polar.magnitude = polarProperties.startMagnitude;
            
            int bulletsPerDirection = bulletCount / SHOTGUN_DIRECTIONS;
            float leftAngle = LEFT_ANGLE + polarProperties.startPhase;
            float middleAngle = MIDDLE_ANGLE + polarProperties.startPhase;
            float rightAngle = RIGHT_ANGLE + polarProperties.startPhase;

            for (int i = 0; i < bulletsPerDirection; i++)
            {
                polar.phase = leftAngle;
                Vector2 point = MathsUtils.PolarToVector(polar);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                CenterPolarPoint(point), bulletProperties));
                
                polar.phase = middleAngle;
                point = MathsUtils.PolarToVector(polar);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                CenterPolarPoint(point), bulletProperties));
                
                polar.phase = rightAngle;
                point = MathsUtils.PolarToVector(polar);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                CenterPolarPoint(point), bulletProperties));

                polar.magnitude += polarProperties.incrementMagnitude;
                polar.phase += polarProperties.incrementPhase;
            }
        }

        private Vector2 CenterPolarPoint(Vector2 point)
        {
            return point + origin;
        }

        private void MandelbrotPointsCalculation()
        {
            System.Numerics.Complex z = new System.Numerics.Complex(0, 0);

            for (int i = 0; i < bulletCount; i++)
            {
                z = System.Numerics.Complex.Pow(z, 2) + mandelbrotComplex;

                Vector2 point = MathsUtils.ComplexToVector(z);
                bullets.Add(new Bullet(contentManager, graphicsDevice, origin, 
                                MandelbrotCenterPoint(point), bulletProperties));
            }
        }

        // Mandelbrot generated patterns (only works with fixed values)
        private Vector2 MandelbrotCenterPoint(Vector2 target)
        {
            Vector2 padding = origin;
            switch (presetName)
            {
                // Only works with 80 bullets and a multiplier of 7.1
                case PresetName.MANDELBROT_SPIRAL:
                    padding -= new Vector2(graphicsDevice.Viewport.Width / 2.8f, graphicsDevice.Viewport.Height / -48f);
                    break;

                // Only works with 40 bullets and a multiplier of 12
                case PresetName.MANDELBROT_STAR:
                    padding -= new Vector2(graphicsDevice.Viewport.Width / 3.2f, graphicsDevice.Viewport.Height / 6.8f);
                    break;

                // Only works with 20 bullets and a multiplier of 24
                case PresetName.MANDELBROT_DUAL_SPIRAL:
                    padding -= new Vector2(graphicsDevice.Viewport.Width / 4f, graphicsDevice.Viewport.Height / 2.5f);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"The preset name is not a Mandelbrot generated pattern");
            }

            float resX = origin.X + (target.X * (bulletCount * paddingMultiplier)) + padding.X;
            float resY = origin.Y + (target.Y * (bulletCount * paddingMultiplier)) + padding.Y;

            return new Vector2(resX, resY);
        }
    }
}
