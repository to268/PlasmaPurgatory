using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{
    public class Player : EntitiesUtils
    {
        private KeyboardState keyboardState;
        private Vector2 originPlayer;
        private Texture2D attack;
        private Vector2 attackPos;
        private Rectangle rectAttack;
        private bool isUnderCooldown;
        private int timer;
        private bool isAttacking;

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

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width / 2f, 600);
            rectangle = new Rectangle((int)position.X - 10, (int)position.Y - 10, 15, 15);
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
            soundEffect = contentManager.Load<SoundEffect>("attackSound");
            originPlayer = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public void Update(GameTime gameTime)
        {
            if (isDead) return;
            
            HandleAttackCooldown();
            HandleInputs();
        }

        public void Draw(GameTime gameTime)
        {
            if (isDead) return;
            isAttacking = false;
            
            attackPos = new Vector2(position.X, position.Y - 50);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, originPlayer, 0.5f,  SpriteEffects.None, 0f);
            CheckAttack();
            spriteBatch.End();
        }

        private void CheckAttack()
        {
            if (keyboardState.IsKeyDown(Keys.Space) && isUnderCooldown == false)
            {
                spriteBatch.Draw(attack, attackPos, null, Color.White, 0f,
                    new Vector2(attack.Width / 2f,attack.Height / 2f), 4f, SpriteEffects.None, 0f);
                rectAttack = new((int)(attackPos.X - (attack.Width * 2)),
                                        (int)(attackPos.Y - (attack.Height / 2f) - 35), 
                                         attack.Width * 4, attack.Height);
                
                soundEffect.Play(0.2f, 0f, 0f);
                isAttacking = true;
                isUnderCooldown = true;
            }
        }

        public void TakeDamage()
        {
            health--;

            if (health <= 0)
                isDead = true;
        }

        private void HandleInputs()
        {
            keyboardState = Keyboard.GetState();

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

            rectangle.X = (int)position.X - 10;
            rectangle.Y = (int)position.Y - 10;
            
            movement = new Vector2(0, 0);
        }
        
        private void HandleAttackCooldown()
        {
            if (timer == 0)
            {
                isUnderCooldown = false;
                timer = 60;
            }

            timer--;
        }
    }
}
