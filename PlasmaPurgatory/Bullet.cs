using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Bullet {
        public enum BulletType { BREAKABLE, UNBREAKABLE };
        
        public struct BulletProperties
        {
            public float movementSpeed;
            public float rotationSpeed;
        }

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
        public BulletType Type
        {
            get { return type; }
        }

        public int BulletProbability
        {
            get { return bulletProbability; }
            set { bulletProbability = value; }
        }

        public Bullet(ContentManager contentManager, GraphicsDevice graphicsDevice, Vector2 origin, Vector2 targetPosition, 
                      BulletProperties bulletProperties)
        {
            color = Color.White;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.targetPosition = targetPosition;
            this.origin = origin;
            
            isBulletDead = false;
            position = origin;
            
            movementSpeed = bulletProperties.movementSpeed;
            rotationSpeed = bulletProperties.rotationSpeed;
            bulletProbability = 2;
            CalculateMovementVector();

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
            MathsUtils.Polar polar = MathsUtils.VectorToPolar(position - origin);
            float res = polar.phase + rotationSpeed;

            polar.phase = res;
            
            position = MathsUtils.PolarToVector(polar);
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
