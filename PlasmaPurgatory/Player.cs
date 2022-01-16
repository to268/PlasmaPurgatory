using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{

    class Player : EntitiesUtils
    {
        public enum TypeCollision { NONE, ENNEMIS, BULLET, PLAYER };

        private KeyboardState keyboardState;
        private Rectangle recPlayer;
        private TypeCollision collision;
        private int currentHealth;
        private Vector2 originPlayer;

        private const float SPEED = 6;
        private const int MAX_HP = 3;

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width/2, 600);
            recPlayer = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
            
            currentHealth = MAX_HP;

            movement = new Vector2(0,0);
        }

        public void LoadContent()
        {
            // TODO: Add animations to the player
            //animationSheet = contentManager.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            //animatedSprite = new AnimatedSprite(animationSheet);
            texture = contentManager.Load<Texture2D>("sGehenna");
            originPlayer = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            animationState = AnimationState.IDLE;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                animationState = AnimationState.WALKNORTH;

                movement.Y = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                animationState = AnimationState.WALKSOUTH;

                movement.Y = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                animationState = AnimationState.WALKWEST;

                movement.X = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                animationState = AnimationState.WALKEAST;

                movement.X = 1;
            }
            else
            {
                animationState = AnimationState.IDLE;

                movement = new Vector2(0, 0);
            }

            if (CheckBound(position, graphicsDevice, texture))
                position += movement * SPEED;

            recPlayer.X = (int)position.X;
            recPlayer.Y = (int)position.Y;

            // TODO: Implement attack feature
            //if (CheckCollision(recPlayer, recEnnemy) && keyboardState.IsKeyDown(Keys.Space))
            //{

            //}
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, originPlayer, 0.5f,  SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
