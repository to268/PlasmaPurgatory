using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PlasmaPurgatory
{
    public class Level
    {
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private SceneManager sceneManager;

        private Texture2D map;

        private Vector2 mapPos;
        //private TiledMap _tiledMap;
        //private TiledMapRenderer _tiledMapRenderer;


        public Level(GraphicsDevice graphicsDevice, ContentManager contentManager, SceneManager sceneManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Initialize()
        {
            mapPos = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            map = contentManager.Load<Texture2D>("Map");

            //_tiledMap = contentManager.Load<TiledMap>("map");
            //_tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, mapPos, Color.White);
            spriteBatch.End();
        }
    }
}