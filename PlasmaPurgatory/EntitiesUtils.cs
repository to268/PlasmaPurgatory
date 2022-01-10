using Microsoft.Xna.Framework;
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
    }
}
