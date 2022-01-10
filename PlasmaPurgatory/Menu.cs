using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PlasmaPurgatory
{
    class Menu
    {   
        struct Button
        {
            public Vector2 position;
            public Texture2D texture;
            public Color color;
            public Action callback;
            public Vector2 origin;
        }

        
        public const float scale = .3f;
        private Button[] buttons;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private MouseState mouseState;
        private Point mousePosition;
        private const int BUTTONS_COUNT = 2;

        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;

            buttons = new Button[BUTTONS_COUNT];
            PopulateButtonArray();
        }

        public void Initialize()
        {
            // Start button properties
           
            buttons[0].position = new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/3);
            buttons[0].color = Color.White;

            buttons[1].position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height /2);
            buttons[1].color = Color.White;
            
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            // Start button texture loading
            buttons[0].texture = contentManager.Load<Texture2D>("PlayButton");
            buttons[0].origin = new Vector2(buttons[0].texture.Width / 2, buttons[0].texture.Height / 2);

            buttons[1].texture = contentManager.Load<Texture2D>("LargeButtons\\LargeButtons\\OptionsButton");
            buttons[1].origin = new Vector2(buttons[1].texture.Width / 2, buttons[1].texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            if (mouseState.LeftButton == ButtonState.Pressed )
            {
                Debug.WriteLine("Boutton clické");
                buttons[1].texture = contentManager.Load<Texture2D>("LargeButtons\\ColoredLargeButtons\\Optionscol_Button");
                
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Button button in buttons)
            {
                spriteBatch.Draw(button.texture, button.position, null, button.color,
                                 0, button.origin, scale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }


        private void PopulateButtonArray()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
            }
        }

    }
}
