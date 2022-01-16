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
            NORMAL,
            BOSS,
        }
        
        public enum EnemyState
        {
            ALIVE,
            DEAD,
        }

        private EnemyType type;
        private EnemyState state;
        
        private const float SPEED = 6f;

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
            switch (type)
            {
                case EnemyType.NORMAL:
                    health = 3;
                    break;
                
                case EnemyType.BOSS:
                    health = 8;
                    break;

                default:
                    break;
            }
        }
        
        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 3.5f);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent()
        {
            // TODO: Load the correct texture
            switch (type)
            {
                case EnemyType.NORMAL:
                    texture = contentManager.Load<Texture2D>("Player");
                    break;
                    
                case EnemyType.BOSS:
                    texture = contentManager.Load<Texture2D>("Player");
                    break;
                
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (state == EnemyState.DEAD)
                return;
            
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
        private void Move()
        {
            Random random = new Random();
            int direction = random.Next(0, 1);
            int amount;

            if (direction == 0)
                amount = -random.Next(1, (int)position.X);
            else
                amount = random.Next(1, (int)(graphicsDevice.Viewport.Width - (position.X  + texture.Width)));
            
            position.X += amount;
        }
    }
}
