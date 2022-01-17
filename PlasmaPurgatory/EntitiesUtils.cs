using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace PlasmaPurgatory
{
    class EntitiesUtils
    {
        protected enum AnimationState { IDLE, WALKNORTH, WALKSOUTH, WALKEAST, WALKWEST, ATTACK };

        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;

        protected AnimationState animationState;
        protected Texture2D texture;
        protected SpriteBatch spriteBatch;
        protected Rectangle rectangle;

        protected Vector2 position;
        protected Vector2 movement;
        protected int health;

        protected bool CheckBound(Vector2 postion, GraphicsDevice graphicsDevice, Texture2D texture)
        {
            if (postion.X > graphicsDevice.Viewport.Width - texture.Width / 8f)
            {
                position.X = graphicsDevice.Viewport.Width - texture.Width / 8f;
                return false;
            }
            else if (postion.Y > graphicsDevice.Viewport.Height - texture.Height / 4f)
            {
                position.Y = graphicsDevice.Viewport.Height - texture.Height / 4f;
                return false;
            }
            else if (postion.X - (texture.Width / 9f) < 0)
            {
                position.X = texture.Width / 9f;
                return false;
            }
            else if (postion.Y - (texture.Height / 5f) < 0)
            {
                position.Y = texture.Height / 5f;
                return false;
            }

            return true;
        } 

        // TODO: Make a generic function to handle collisions
        protected bool CheckCollision(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (rectangle1.Intersects(rectangle2))
                return false;

            return true;
        }
    }
}
