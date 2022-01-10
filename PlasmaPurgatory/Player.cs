using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Diagnostics;

namespace PlasmaPurgatory
{
    class Player : EntitiesUtils
    {
        private KeyboardState keyboardState;
        private const float SPEED = 10;

        public Player(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.contentManager = contentManager;
            base.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            position = new Vector2(0, 0);
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
            if (CheckBound(position, graphicsDevice))
            {
                position += movement;
            }




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
