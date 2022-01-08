using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Sprites;

namespace PlasmaPurgatory
{
    class PlayerUtils
    {
        protected enum AnimationState { idle, walkNorth, walkSouth, walkEast, walkWest };

        protected ContentManager contentManager;

        protected AnimatedSprite animatedSprite;
        protected AnimationState animationState;
        protected SpriteSheet animationSheet;

        protected Vector2 position;
        protected Vector2 movement;
        protected int health;
    }
}
