using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory
{
    public class Enemy : EntitiesUtils
    {
        public enum EnemyType
        {
            BARBAROSSA,
            BIGGARRY,
            DATASS,
            HADES,
        }
        
        private EnemyType type;
        private Vector2 pointToReach;
        private bool isMoving;
        
        private const float SPEED = 0.02f;

        public EnemyType Type
        {
            get { return type; }
        }
        
        public Vector2 Position
        {
            get { return position; }
        }

        public Enemy(ContentManager contentManager, GraphicsDevice graphicsDevice, EnemyType type)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.type = type;
            
            isDead = false;
            isMoving = false;
            
            switch (type)
            {
                case EnemyType.BARBAROSSA:
                    health = 1;
                    position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 3.8f);
                    break;
                
                case EnemyType.DATASS:
                    health = 2;
                    position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 4.5f);
                    break;
                
                case EnemyType.BIGGARRY:
                    health = 3;
                    position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 9f);
                    break;
                
                case EnemyType.HADES:
                    position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 20f);
                    health = 6;
                    break;
            }
        }
        
        public void Initialize()
        {
            movement = new Vector2(0, graphicsDevice.Viewport.Height / 4f);
            pointToReach = new Vector2(0, graphicsDevice.Viewport.Height / 4f);
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent()
        {
            switch (type)
            {
                case EnemyType.BARBAROSSA:
                    texture = contentManager.Load<Texture2D>("Barbarossa");
                    break;
                    
                case EnemyType.DATASS:
                    texture = contentManager.Load<Texture2D>("Datass");
                    break;
                
                case EnemyType.BIGGARRY:
                    texture = contentManager.Load<Texture2D>("BigGarry");
                    break;
                
                case EnemyType.HADES:
                    texture = contentManager.Load<Texture2D>("Hades");
                    break;
            }
            
            soundEffect = contentManager.Load<SoundEffect>("enemyHit");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (isDead)
                return;
           
            if (position.X < 0 || position.X > graphicsDevice.Viewport.Width - texture.Width)
                Reset();
            
            if (!isMoving)
                FindPath();
            else
                Move();
            
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public void Draw(GameTime gameTime)
        {
            if (isDead)
                return;
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }

        public void TakeDamage()
        {
            health--;
            soundEffect.Play(0.6f, 0f, 0f);

            if (health == 0)
                isDead = true;
        }

        private void FindPath()
        {
            Random random = new Random();
            int direction = random.Next(0, 2);
            int amount;

            if (direction == 0 && position.X > 0)
            {
                amount = -random.Next(0, (int)(position.X - texture.Width / 10f));
                pointToReach.X = (amount + position.X);
            }
            else if (position.X < graphicsDevice.Viewport.Width - texture.Width)
            {
                amount = random.Next(0, (int)(graphicsDevice.Viewport.Width - (position.X  + texture.Width / 10f)));
                pointToReach.X = (position.X + amount);
            }
            
            movement.X = (pointToReach.X - position.X) * SPEED;
            isMoving = true;
        }

        private void Move()
        {
            if ((int)position.X != (int)pointToReach.X)
                position.X += movement.X;
            else
                isMoving = false;
        }

        // This functions should never be called
        private void Reset()
        {
            pointToReach = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 4f);
            movement.X = (pointToReach.X - position.X) * SPEED;
        }
    }
}
