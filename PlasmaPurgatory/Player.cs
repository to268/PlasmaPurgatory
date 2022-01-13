using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{
    public enum TypeCollision { None, Ennemis, Bullet, Player };
    class Player : EntitiesUtils
    {
        private KeyboardState keyboardState;
        private Rectangle recPlayer;
        private TypeCollision collision;
        private int pointVie = 3;
        private const float SPEED = 10;

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(0, 0);
            recPlayer = new Rectangle((int)position.X, (int)position.Y, 30, 30);
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent()
        {
            //animationSheet = contentManager.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            //animatedSprite = new AnimatedSprite(animationSheet);
            texture = contentManager.Load<Texture2D>("Player");
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            animationState = AnimationState.idle;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                animationState = AnimationState.walkNorth;

                movement = new Vector2(0, -1);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                animationState = AnimationState.walkSouth;

                movement = new Vector2(0, 1);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                animationState = AnimationState.walkWest;

                movement = new Vector2(-1, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                animationState = AnimationState.walkEast;

                movement = new Vector2(1, 0);
            }
            else
            {
                animationState = AnimationState.idle;

                movement = new Vector2(0, 0);
            }

            if (CheckBound(position, graphicsDevice, texture))
            {
                position += movement;
            }

            recPlayer.X = (int)position.X;
            recPlayer.Y = (int)position.Y;

            //if (CheckCollision(recPlayer, recEnnemy) && keyboardState.IsKeyDown(Keys.Space))
            //{

            //}



            // Check bounds with the window before applying the movement

            /*
            if (Collision == TypeCollisionMapMaison.Rien && !toucheBordFenetre)
                 positionPlayer += walkSpeed * deplacement;

            Player.Play(AnimationPlayer.ToString());
            Player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            */
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);


            spriteBatch.End();
        }



    }

}
