using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory
{
    class PatternGenerator
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private List<Bullet> bullets;
        private int windowWidth;
        private int windowHeight;
        private const int MAX_ITERATIONS = 40;

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

        private void MandelbrotPointsCalculation()
        {
            System.Numerics.Complex z = new System.Numerics.Complex(0, 0);
            //System.Numerics.Complex c = new System.Numerics.Complex(-0.38, 0.53);
            System.Numerics.Complex c = new System.Numerics.Complex(-0.05, -0.63);

            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                z = System.Numerics.Complex.Pow(z, 2) + c;

                bullets.Add(new Bullet(contentManager, graphicsDevice, AdaptResult(z)));
            }
            Debug.WriteLine(bullets[0].Position.ToString());
            Debug.WriteLine(bullets[2].Position.ToString());
            Debug.WriteLine((bullets[2].Position - bullets[0].Position).ToString());
        }

        private Vector2 AdaptResult(System.Numerics.Complex complex)
{
            Vector2 point = ComplexToVector(complex);
            Vector2 middle = new Vector2(windowWidth / 2, windowHeight / 2);

            //float resX = middle.X + (point.X * 400)  - (windowWidth / 2.7f);
            //float resY = (middle.Y + (point.Y * 400)) - (windowHeight / 1.25f);
            float resX = middle.X + (point.X * 400)  - (windowWidth / 2.4f);
            float resY = (middle.Y + (point.Y * 400)) - (windowHeight / 4.8f);

            Debug.WriteLine(resX.ToString() + " " + resY.ToString());
            return middle + new Vector2(resX, resY);
        }

        private Vector2 ComplexToVector(System.Numerics.Complex complex)
        {
            return new Vector2((float)complex.Real, (float)complex.Imaginary);
        }

        private System.Numerics.Complex VectorToComplex(Vector2 vector)
        {
            return new System.Numerics.Complex(vector.X, vector.Y);
        }

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
