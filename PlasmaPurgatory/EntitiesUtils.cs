using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory
{
    public class EntitiesUtils
    {
        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;

        protected Texture2D texture;
        protected SpriteBatch spriteBatch;
        protected Rectangle rectangle;
        protected SoundEffect soundEffect;

        protected Vector2 position;
        protected Vector2 movement;
        protected int health;
        protected bool isDead;

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }
        
        public Texture2D Texture
        {
            get { return texture; }
        }
        
        public int Health
        {
            get { return health; }
        }
        
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        protected bool CheckBound(Vector2 postion, GraphicsDevice gDevice, Texture2D tex)
        {
            if (postion.X > gDevice.Viewport.Width - tex.Width / 8f)
            {
                position.X = gDevice.Viewport.Width - tex.Width / 8f;
                return false;
            }
            else if (postion.Y > gDevice.Viewport.Height - tex.Height / 4f)
            {
                position.Y = gDevice.Viewport.Height - tex.Height / 4f;
                return false;
            }
            else if (postion.X - (tex.Width / 9f) < 0)
            {
                position.X = tex.Width / 9f;
                return false;
            }
            else if (postion.Y - (tex.Height / 5f) < 0)
            {
                position.Y = tex.Height / 5f;
                return false;
            }

            return true;
        } 
    }
}
