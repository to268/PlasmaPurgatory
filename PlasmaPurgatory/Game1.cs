using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Menu menu;
        private PatternGenerator patternGenerator;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu = new Menu(graphics, spriteBatch);

            patternGenerator = new PatternGenerator(Content, GraphicsDevice,
                                                    GraphicsDevice.Viewport.Width,
                                                    GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent();
            patternGenerator.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            patternGenerator.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            patternGenerator.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
