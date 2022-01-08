using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace PlasmaPurgatory
{
    class Player : PlayerUtils
    {
        private KeyboardState keyboardState;

        public Player(ContentManager contentManager)
        {
            base.contentManager = contentManager;
        }

        public void Initialize()
        {
            position = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            animationSheet = contentManager.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            animatedSprite = new AnimatedSprite(animationSheet);
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

            // Check bounds with the window before applying the movement

            /*
            if (Collision == TypeCollisionMapMaison.Rien && !toucheBordFenetre)
                 positionPlayer += walkSpeed * deplacement;

            Player.Play(AnimationPlayer.ToString());
            Player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            */
        }

    }

}
