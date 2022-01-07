using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace PlasmaPurgatory
{
    public enum TypeAnimation { walkSouth, walkNorth, walkEast, walkWest, idle };
    class Player : PlayerUtils
    {
        private AnimatedSprite spriteplayer;
        private TypeAnimation animationPlayer;
        private Vector2 positionPlayer;
        private KeyboardState keyboardState;

        public AnimatedSprite SpritePlayer
        {
            get
            {
                return this.spriteplayer;
            }

            set
            {
                this.spriteplayer = value;
            }
        }

        public TypeAnimation AnimationPlayer
        {
            get
            {
                return this.animationPlayer;
            }

            set
            {
                this.animationPlayer = value;
            }
        }

        public Player()
        {

        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            positionPlayer = new Vector2();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteSheet animation = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            SpritePlayer = new AnimatedSprite(animation);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            AnimationPlayer = TypeAnimation.idle;
            bool toucheBordFenetre = false;
            Vector2 deplacement = new Vector2(0, 0);
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                AnimationPlayer = TypeAnimation.walkNorth;
                
                deplacement = new Vector2(0, -1);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                AnimationPlayer = TypeAnimation.walkSouth;
                
                deplacement = new Vector2(0, 1);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                AnimationPlayer = TypeAnimation.walkWest;
                
                deplacement = new Vector2(-1, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                AnimationPlayer = TypeAnimation.walkEast;
               
                deplacement = new Vector2(1, 0);
            }
            if (Collision == TypeCollisionMapMaison.Rien && !toucheBordFenetre)
                 positionPlayer += walkSpeed * deplacement;

            Player.Play(AnimationPlayer.ToString());
            Player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

    }

    }
}
