using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Menu menu;
        private PatternGenerator patternGenerator;
        private PatternPreset patternPreset;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //menu = new Menu(graphics, spriteBatch);

            Vector2 origin = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 4f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(15f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = -1.8f;

            patternPreset = new PatternPreset(PatternPreset.PresetName.SPIRAL, polarProperties, Content, GraphicsDevice, origin, 80);
            patternGenerator = new PatternGenerator(patternPreset);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //menu.LoadContent();
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
