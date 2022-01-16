using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace PlasmaPurgatory
{
    public class SceneManager
    {
        public enum SceneType
        {
            MENU,
            LEVEL
        }

        private const int BEDTIME = 500;

        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private Menu menu;
        private Level level;
        private SceneType currentScene;

        public SceneManager(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;

            currentScene = SceneType.MENU;
        }

        public void Initialize()
        {
            // Load SubClasses
            menu = new Menu(graphicsDevice, contentManager, this);
            level = new Level(graphicsDevice, contentManager);


            menu.Initialize();
            level.Initialize();
        }

        public void LoadContent()
        {
            menu.LoadContent();
            level.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            UpdateCurrentScene(gameTime);
            
        }

        public void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            DrawCurrentScene(gameTime);
        }

        private void DrawCurrentScene(GameTime gameTime)
        {
            switch (currentScene)
            {
                case SceneType.MENU:
                    menu.Draw(gameTime);
                    break;
                case SceneType.LEVEL:
                    level.Draw(gameTime);
                    break;
                default:
                    break;
            }
        }

        private void UpdateCurrentScene(GameTime gameTime)
        {
            switch (currentScene)
            {
                case SceneType.MENU:
                    menu.Update(gameTime);
                    break;
                case SceneType.LEVEL:
                    level.Update(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void ChangeScene(SceneType scene)
        {
            Thread.Sleep(BEDTIME);
            currentScene = scene;
        }
    }
}
