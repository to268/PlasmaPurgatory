using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SceneManager sceneManager;
        /*private Menu menu;
        private PatternGenerator patternGenerator;
        private PatternPreset patternPreset;*/

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //menu = new Menu(_graphics, _spriteBatch);

            // TODO: Initialize the player when loading the level
            //player = new Player(Content, GraphicsDevice);
            //player.Initialize();
            // Change Window size
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            
            sceneManager = new SceneManager(Content, GraphicsDevice);
            
            /*Vector2 origin = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);

            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(15f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = -1.8f;
            
            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            bulletProperties.movementSpeed = 0.12f;
            bulletProperties.rotationSpeed = MathsUtils.DegresToRadians(0.14f);

            patternPreset = new PatternPreset(PatternPreset.PresetName.SPIRAL, polarProperties, bulletProperties, Content, GraphicsDevice, origin, 80);
            patternGenerator = new PatternGenerator(patternPreset);*/
            
            sceneManager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //menu.LoadContent();
            //player.LoadContent();
            //patternGenerator.LoadContent();
            
            sceneManager.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //player.Update(gameTime);
            //patternGenerator.Update(gameTime);
            
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //player.Draw(gameTime);
            //patternGenerator.Draw(gameTime);
            
            sceneManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
