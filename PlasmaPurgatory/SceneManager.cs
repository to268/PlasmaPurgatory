using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PlasmaPurgatory
{
    class SceneManager
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


            menu.Initialize();
        }

        public void LoadContent()
        {
            menu.LoadContent();
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
