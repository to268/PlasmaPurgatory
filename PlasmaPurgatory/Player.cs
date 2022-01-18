using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{

    class Player : EntitiesUtils
    {
        public enum CollisionType { NONE, ENNEMIS, BULLET, PLAYER };

        private KeyboardState keyboardState;
        private Vector2 originPlayer;
        private CollisionType collision;
        private Texture2D attack;
        private Vector2 attackPos;
        private Rectangle rectAttack;
        private bool isUnderCooldown;
        private int timer;
        private bool isAttacking;
        private bool isDead;

        private const float SPEED = 10;
        private const int MAX_PLAYER_HP = 3;

        public Rectangle RectAttack
        {
            get { return rectAttack; }
        }
        
        public bool IsAttacking
        {
            get { return isAttacking; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width / 2f, 600);
            rectangle = new Rectangle((int)position.X - 160, (int)position.Y - 160, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
            rectAttack = new Rectangle();
            health = MAX_PLAYER_HP;
            isUnderCooldown = false;
            isAttacking = false;
            isDead = false;
            timer = 60;
        }

        public void LoadContent()
        {
            attack = contentManager.Load<Texture2D>("Attack");
            texture = contentManager.Load<Texture2D>("sGehenna");
            originPlayer = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public void Update(GameTime gameTime)
        {
            if (isDead) return;
            
            if (timer == 0)
            {
                isUnderCooldown = false;
                timer = 60;
            }
            keyboardState = Keyboard.GetState();
            animationState = AnimationState.IDLE;

            if (keyboardState.IsKeyDown(Keys.Space))
                isAttacking = true;
            else
                isAttacking = false;
            
            if (keyboardState.IsKeyDown(Keys.Up))
                movement.Y = -1;
            
            if (keyboardState.IsKeyDown(Keys.Down))
                movement.Y = 1;
            
            if (keyboardState.IsKeyDown(Keys.Left))
                movement.X = -1;
            
            if (keyboardState.IsKeyDown(Keys.Right))
                movement.X = 1;

            if (CheckBound(position, graphicsDevice, texture))
                position += movement * SPEED;

            timer--;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            
            movement = new Vector2(0, 0);
        }

        public void Draw(GameTime gameTime)
        {
            if (isDead) return;
            isAttacking = false;
            
            attackPos = new Vector2(position.X, position.Y - 50);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, originPlayer, 0.5f,  SpriteEffects.None, 0f);
            if (keyboardState.IsKeyDown(Keys.Space) && isUnderCooldown == false)
            {
                spriteBatch.Draw(attack, attackPos, null, Color.White, 0f,
                    new Vector2(attack.Width / 2f,attack.Height / 2f), 4f, SpriteEffects.None, 0f);
                rectAttack = new((int)(attackPos.X - (attack.Width / 2) - 10), (int)(attackPos.Y - (attack.Height / 2) - 45), 
                                            attack.Width, attack.Height);
                
                isAttacking = true;
                isUnderCooldown = true;
            }
            spriteBatch.End();
        }

        public void TakeDamage()
        {
            health--;

            if (health <= 0)
                isDead = true;
        }
    }
}
