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

        private const float SPEED = 10;
        private const int MAX_PLAYER_HP = 3;

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(graphicsDevice.Viewport.Width / 2f, 600);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
            health = MAX_PLAYER_HP;
        }

        public void LoadContent()
        {
            texture = contentManager.Load<Texture2D>("sGehenna");
            originPlayer = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            animationState = AnimationState.IDLE;

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

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            
            movement = new Vector2(0, 0);

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
