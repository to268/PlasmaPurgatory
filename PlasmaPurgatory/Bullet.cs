using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Bullet {
        public enum BulletType { BREAKABLE, UNBREAKABLE };

        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private Vector2 origin;
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 movementVector;
        private Texture2D texture;
        private Color color;
        private BulletType type; 
        
        private float movementSpeed;
        private float rotationSpeed;
        private bool isBulletDead;
        private int bulletProbability;
        
        // TODO: Remove unused fields
        public float MovementSpeed
        {
             get { return movementSpeed; }
             set { movementSpeed = value; CalculateMovementVector(); }
        }
        
        public float RotationSpeed
        {
             get { return rotationSpeed; }
             set { rotationSpeed = value; }
        }

        public BulletType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int BulletProbability
        {
            get { return bulletProbability; }
            set { bulletProbability = value; }
        }

        public Bullet(ContentManager contentManager, GraphicsDevice graphicsDevice, Vector2 origin, Vector2 targetPosition)
        {
            color = Color.White;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.targetPosition = targetPosition;
            this.origin = origin;
            
            isBulletDead = false;
            position = origin;
            
            // Default values
            MovementSpeed = 0.12f;
            RotationSpeed = MathsUtils.DegresToRadians(0.14f);
            bulletProbability = 2;

            type = RandomBulletType();
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            texture = contentManager.Load <Texture2D>("bullet");
        }

        public void Update(GameTime gameTime)
        {
            MoveBullet();
            RotateBullet();
        }

        public void Draw(GameTime gameTime)
        {
            if (isBulletDead)
                return;
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, color);
            spriteBatch.End();
        }

        private void CalculateMovementVector()
        {
            movementVector = (targetPosition - origin) * movementSpeed;
        }

        private void MoveBullet()
        {
            if ((position.X + texture.Width) < 0 || position.X > graphicsDevice.Viewport.Width &&
                position.Y < 0 || position.Y > graphicsDevice.Viewport.Height)
                isBulletDead = true;
            
            if (!isBulletDead)
                position += movementVector * movementSpeed;
        }

        // TODO: Find a way to fix the pattern to shrink for no obvious reasons
        private void RotateBullet()
        {
            MathsUtils.Polar polar = MathsUtils.ComplexToPolar(MathsUtils.VectorToComplex(position - origin));
            float res = polar.phase + rotationSpeed;

            polar.phase = res;
            
            position = MathsUtils.ComplexToVector(MathsUtils.PolarToComplex(polar));
            position += origin;
        }

        private BulletType RandomBulletType()
        {
            Random random = new Random();
            if (random.Next(0, bulletProbability) == 0)
            {
                return BulletType.BREAKABLE;
            }

            return BulletType.UNBREAKABLE;
        }
    }
}
