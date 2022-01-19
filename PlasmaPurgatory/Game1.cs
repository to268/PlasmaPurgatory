using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SceneManager sceneManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Change Window size
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            
            sceneManager = new SceneManager(Content, GraphicsDevice, this);
            sceneManager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        public void ExitGame()
        {
            Exit();
        }

    }
}
