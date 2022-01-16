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

        private Player player;


        public Level(GraphicsDevice graphicsDevice, ContentManager contentManager, SceneManager sceneManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Initialize()
        {
            mapPos = new Vector2(0, 0);

            // TODO: Initialize the player when loading the level
            player = new Player(contentManager, graphicsDevice);
            player.Initialize();
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            map = contentManager.Load<Texture2D>("Map");

            //_tiledMap = contentManager.Load<TiledMap>("map");
            //_tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);

            player.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, mapPos, Color.White);
            
            spriteBatch.End();
            player.Draw(gameTime);
        }
    }
}