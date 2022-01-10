using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory.Generator
{
    class PatternGenerator
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private List<Bullet> bullets;
        private int windowWidth;
        private int windowHeight;
        private const int MAX_ITERATIONS = 120;
        private const int PADDING_MULTIPLYER = 8;

        // TODO: Pass a PatternPreset object to the constructor
        public PatternGenerator(ContentManager contentManager, GraphicsDevice graphicsDevice,
                                int windowWidth, int windowHeight)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            bullets = new List<Bullet>();
            MandelbrotPointsCalculation();
        }

        // TODO: Move this into the PatternPreset class
        private void MandelbrotPointsCalculation()
        {
            System.Numerics.Complex z = new System.Numerics.Complex(0, 0);
            // Test value
            //System.Numerics.Complex c = new System.Numerics.Complex(-0.38, 0.53);
            System.Numerics.Complex c = new System.Numerics.Complex(-0.05, -0.63);

            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                z = System.Numerics.Complex.Pow(z, 2) + c;

                bullets.Add(new Bullet(contentManager, graphicsDevice, AdaptResult(z)));
            }
        }

        private Vector2 AdaptResult(System.Numerics.Complex complex)
{
            Vector2 point = ComplexToVector(complex);
            Vector2 middle = new Vector2(windowWidth / 2, windowHeight / 2);


            return middle + CenterMiddle(point, middle);
        }

        private Vector2 CenterMiddle(Vector2 point, Vector2 middle)
        {
            // Test values
            //middle -= new Vector2(windowWidth / 2.7f, windowHeight / 1.5f);
            //float resX = middle.X + (point.X * (MAX_ITERATIONS * PADDING_MULTIPLYER));
            //float resY = middle.Y + (point.Y * (MAX_ITERATIONS * PADDING_MULTIPLYER));

            // 20 (multiplier * 2)
            //middle -= new Vector2(windowWidth / 2.3f, windowHeight / 3.6f);
            // 40
            //middle -= new Vector2(windowWidth / 2.3f, windowHeight / 3.6f);
            // 80
            //middle -= new Vector2(windowWidth / 2.8f, windowHeight / -32f);
            // 120
            middle -= new Vector2(windowWidth / 3.4f, windowHeight / -3f);
            float resX = middle.X + (point.X * (MAX_ITERATIONS * PADDING_MULTIPLYER));
            float resY = middle.Y + (point.Y * (MAX_ITERATIONS * PADDING_MULTIPLYER));

            Debug.WriteLine(resX.ToString() + " " + resY.ToString());

            return new Vector2(resX, resY);
        }

        // TODO: Maybe move this function out in a new class
        private Vector2 ComplexToVector(System.Numerics.Complex complex)
        {
            return new Vector2((float)complex.Real, (float)complex.Imaginary);
        }

        // TODO: Maybe move this function out in a new class or remove it
        private System.Numerics.Complex VectorToComplex(Vector2 vector)
        {
            return new System.Numerics.Complex(vector.X, vector.Y);
        }

        // TODO: Move Monogame functions of use it a a wrapper
        public void Initialize()
        {
        }

        public void LoadContent()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].LoadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(gameTime);
            }
        }
    }
}
