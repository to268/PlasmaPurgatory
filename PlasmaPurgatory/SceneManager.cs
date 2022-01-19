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
            LEVEL,
            GAMEOVER
        }

        private const int BEDTIME = 500;

        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private Menu menu;
        private Level level;
        private GameOver gameOver;
        private Game1 game1;
        private SceneType currentScene;

        public SceneManager(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game1)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.game1 = game1;

            currentScene = SceneType.MENU;
        }

        public void Initialize()
        {
            menu = new Menu(graphicsDevice, contentManager, this, game1);

            menu.Initialize();
            if (level != null)
            {
                level.Initialize();
            }
            else if (gameOver != null)
            {
                gameOver.Initialize();
            }
        }

        public void LoadContent()
        {
            menu.LoadContent();
            if (level != null)
            {
                level.LoadContent();
            } 
            else if (gameOver != null)
            {
                gameOver.LoadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (currentScene)
            {
                case SceneType.MENU:
                    menu.Update(gameTime);
                    break;
                case SceneType.LEVEL:
                    level.Update(gameTime);
                    break;
                case SceneType.GAMEOVER:
                    gameOver.Update(gameTime);
                    break;
            }
            
        }

        public void Draw(GameTime gameTime)
        {
            switch (currentScene)
            {
                case SceneType.MENU:
                    menu.Draw(gameTime);
                    break;
                case SceneType.LEVEL:
                    level.Draw(gameTime);
                    break;
                case SceneType.GAMEOVER:
                    gameOver.Draw(gameTime);
                    break;
            }
        }

        public void ChangeScene(SceneType scene)
        {
            Thread.Sleep(BEDTIME);
            
            if (scene == SceneType.LEVEL)
            {
                level = new Level(graphicsDevice, contentManager, this);
                level.Initialize();
                level.LoadContent();
                gameOver = null;
            } 
            else if (scene == SceneType.GAMEOVER)
            {
                gameOver = new GameOver(graphicsDevice, contentManager, this, game1);
                gameOver.Initialize();
                gameOver.LoadContent();
                level = null;
            }
            else if (scene == SceneType.MENU)
            {
                menu = null;   
                menu = new Menu(graphicsDevice, contentManager, this, game1);
                menu.Initialize();
                menu.LoadContent();
            }
            
            currentScene = scene;
        }
    }
}
