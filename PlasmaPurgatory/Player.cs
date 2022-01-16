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
        private CollisionType collision;

        private const float SPEED = 10;
        private const int MAX_PLAYER_HP = 3;

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(0, 0);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
            health = MAX_PLAYER_HP;
        }

        public void LoadContent()
        {
            // TODO: Add animations to the player
            //animationSheet = contentManager.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            //animatedSprite = new AnimatedSprite(animationSheet);
            texture = contentManager.Load<Texture2D>("Player");
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            animationState = AnimationState.IDLE;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                animationState = AnimationState.WALKNORTH;

                movement = new Vector2(0, -1);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                animationState = AnimationState.WALKSOUTH;

                movement = new Vector2(0, 1);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                animationState = AnimationState.WALKWEST;

                movement = new Vector2(-1, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                animationState = AnimationState.WALKEAST;

                movement = new Vector2(1, 0);
            }
            else
            {
                animationState = AnimationState.IDLE;

                movement = new Vector2(0, 0);
            }

            if (CheckBound(position, graphicsDevice, texture))
                position += movement;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;

            // TODO: Implement attack feature
            //if (CheckCollision(recPlayer, recEnnemy) && keyboardState.IsKeyDown(Keys.Space))
            //{

            //}
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }
    }
}
