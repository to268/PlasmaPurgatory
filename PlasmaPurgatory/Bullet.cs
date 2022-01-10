using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory
{
    class Bullet {

        // TODO: Implements the 2 types of bullets and the moving calculation for later
        public enum BulletType { BREAKABLE, UNBREAKABLE };
        public enum BulletState { HIDDEN, MOVING, DEAD };

        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private Vector2 postion;
        private Texture2D texture;
        private Color color;
        private BulletType type; 
        private BulletState state; 

        public Vector2 Position
        {
            get { return postion; }
            set { postion = value; }
        }

        public Bullet(ContentManager contentManager, GraphicsDevice graphicsDevice, Vector2 initialPosition)
        {
            Position = initialPosition;
            color = Color.White;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;

            // TODO: Implement the 2 types of bullets and the moving calculation 
            type = BulletType.UNBREAKABLE;
            state = BulletState.MOVING;
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            texture = contentManager.Load <Texture2D>("bullet");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, color);
            spriteBatch.End();
        }
    }
}
