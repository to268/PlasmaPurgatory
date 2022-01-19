using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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
        
        public enum EnemyState
        {
            ALIVE,
            DEAD,
        }

        private EnemyType type;
        private EnemyState state;
        private Vector2 pointToReach;
        private bool isMoving;
        private SoundEffect hitSound;
        
        private const float SPEED = 0.02f;

        public EnemyState State
        {
            get { return state; }
        }
        
        public EnemyType Type
        {
            get { return type; }
        }
        
        public Vector2 Position
        {
            get { return position; }
        }

        public Texture2D Texture
        {
            get { return texture; }
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
            // TODO: Load the correct texture
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
            
            hitSound = contentManager.Load<SoundEffect>("enemyHit");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (state == EnemyState.DEAD)
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
            if (state == EnemyState.DEAD)
                return;
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }

        public void TakeDamage()
        {
            health--;
            hitSound.Play(0.6f, 0f, 0f);
            
            if (health == 0)
                state = EnemyState.DEAD;
        }

        // TODO: Try to make movement based on the player X position
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

            if (type == EnemyType.BIGGARRY)
                Debug.WriteLine("BIGGARRY: " + movement.X);
            if (type == EnemyType.HADES)
                Debug.WriteLine("HADES: " + movement.X);
        }

        private void Move()
        {
            if ((int)position.X != (int)pointToReach.X)
                position.X += movement.X;
            else
                isMoving = false;
        }

        private void Reset()
        {
            pointToReach = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 4f);
            movement.X = (pointToReach.X - position.X) * SPEED;
        }
    }
}
