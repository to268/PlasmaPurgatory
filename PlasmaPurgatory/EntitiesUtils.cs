﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace PlasmaPurgatory
{
    class EntitiesUtils
    {
        protected enum AnimationState { idle, walkNorth, walkSouth, walkEast, walkWest };

        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;

        protected AnimatedSprite animatedSprite;
        protected AnimationState animationState;
        protected SpriteSheet animationSheet;
        protected Texture2D texture;
        protected SpriteBatch spriteBatch; 

        protected Vector2 position;
        protected Vector2 movement;
        protected int health;


        protected bool CheckBound(Vector2 postion, GraphicsDevice graphicsDevice)
        {
            if (postion.X > graphicsDevice.Viewport.Width - texture.Width)
            {
                position.X = graphicsDevice.Viewport.Width - texture.Width;
                return false;
            }
            else if (postion.X < 0)
            {
                position.X = 0;
                return false;
            }
            else if (postion.Y > graphicsDevice.Viewport.Height - texture.Height)
            {
                position.Y = graphicsDevice.Viewport.Height - texture.Height;
                return false;
            }
            else if (postion.Y < 0)
            {
                position.Y = 0;
                return false;
            }
            return true;
        } 
    }
}
