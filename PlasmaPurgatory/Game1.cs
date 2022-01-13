using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Menu menu;
        private Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //menu = new Menu(_graphics, _spriteBatch);

            // TODO: Initialize the player when loading the level
            //player = new Player(Content, GraphicsDevice);
            //player.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //menu.LoadContent();
            //player.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //player.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
