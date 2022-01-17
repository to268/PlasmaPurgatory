using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory
{
    class Enemy : EntitiesUtils
    {
        public enum EnemyType
        {
            BARBAROSSA,
            BIGGARRY,
            DATASS,
        }
        
        public enum EnemyState
        {
            ALIVE,
            DEAD,
        }

        private EnemyType type;
        private EnemyState state;
        private Vector2 pointToReach;
        private bool isMoving;
        
        private const float SPEED = 0.2f;

        public EnemyState State
        {
            get { return state; }
        }
        
        public Enemy(ContentManager contentManager, GraphicsDevice graphicsDevice, EnemyType type)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.type = type;
            
            state = EnemyState.ALIVE;
            isMoving = false;
            switch (type)
            {
                case EnemyType.BARBAROSSA:
                    health = 1;
                    break;
                
                case EnemyType.DATASS:
                    health = 2;
                    break;
                
                case EnemyType.BIGGARRY:
                    health = 3;
                    break;

                default:
                    break;
            }
        }
        
        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 3.5f);
            movement = new Vector2(0, 0);
            pointToReach = new Vector2(0, 0);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent()
        {
            // TODO: Load the correct texture
            switch (type)
            {
                case EnemyType.BARBAROSSA:
                    texture = contentManager.Load<Texture2D>("Ememy\\Barbarossa");
                    break;
                    
                case EnemyType.DATASS:
                    texture = contentManager.Load<Texture2D>("Ememy\\Datass");
                    break;
                
                case EnemyType.BIGGARRY:
                    texture = contentManager.Load<Texture2D>("Ememy\\BigGarry");
                    break;
                
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (state == EnemyState.DEAD)
                return;

            if (!isMoving)
                FindPath();
            else
                Move();
            
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public void Draw(GameTime gameTime)
        {
            if (state == EnemyState.DEAD)
                return;
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }

        public void TakeDamage()
        {
            health--;

            if (health == 0)
                state = EnemyState.DEAD;
        }

        // TODO: Try to make movement based on the player X position
        private void FindPath()
        {
            Random random = new Random();
            int direction = random.Next(0, 1);
            int amount;

            if (direction == 0)
                amount = -random.Next(1, (int)position.X - texture.Width);
            else
                amount = random.Next(1, (int)(graphicsDevice.Viewport.Width - (position.X  + texture.Width)));

            pointToReach.X = amount;
            movement = (pointToReach - position) * SPEED;
            isMoving = true;
        }

        private void Move()
        {
            if (position != pointToReach)
                position += movement;
            else
                isMoving = false;
        }
    }
}
