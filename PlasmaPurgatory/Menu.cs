using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Threading;

namespace PlasmaPurgatory
{
    class Menu
    {   
        struct Button
        {
            public Vector2 position;
            public Texture2D currentTexture;
            public Texture2D normalTexture;
            public Texture2D hoverTexture;
            public Color color;
            public Vector2 origin;
            public Rectangle hitbox;
        }
        
        public const float scale = .3f;
        private const int BUTTONS_COUNT = 2;
        
        private Button[] buttons;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private SceneManager sceneManager;
        private MouseState mouseState;
        private Point mousePosition;

        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager, SceneManager sceneManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            buttons = new Button[BUTTONS_COUNT];
            PopulateButtonArray();
        }

        public void Initialize()
        {
            // Start button properties
            buttons[0].position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 3);
            buttons[0].color = Color.White;


            buttons[1].position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height /2);
            buttons[1].color = Color.White;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            buttons[0].normalTexture = contentManager.Load<Texture2D>("LargeButtons\\LargeButtons\\PlayButton");
            buttons[0].hoverTexture = contentManager.Load<Texture2D>("LargeButtons\\ColoredLargeButtons\\Playcol_Button");
            buttons[0].origin = new Vector2(buttons[0].normalTexture.Width / 2, buttons[0].normalTexture.Height / 2);
            buttons[0].currentTexture = buttons[0].normalTexture;

            buttons[1].normalTexture = contentManager.Load<Texture2D>("LargeButtons\\LargeButtons\\OptionsButton");
            buttons[1].hoverTexture = contentManager.Load<Texture2D>("LargeButtons\\ColoredLargeButtons\\Optionscol_Button");
            buttons[1].origin = new Vector2(buttons[1].normalTexture.Width / 2, buttons[1].normalTexture.Height / 2);
            buttons[1].currentTexture = buttons[1].normalTexture;
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);

            for (int i = 0; i < BUTTONS_COUNT; i++)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && buttons[i].hitbox.Contains(mousePosition))
                {
                    buttons[i].currentTexture = buttons[i].hoverTexture;
                    if (i == 0)
                    {
                        Debug.WriteLine("1");
                        Draw(gameTime);
                        Debug.WriteLine("2");
                        sceneManager.ChangeScene(SceneManager.SceneType.LEVEL);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            for (int i = 0; i < BUTTONS_COUNT; i++)
            {
                buttons[i].hitbox = new Rectangle((int)buttons[i].position.X - 90, (int)buttons[i].position.Y - 80,
                                                  (int)(buttons[i].normalTexture.Width / 3.2f), (buttons[i].normalTexture.Height / 2) + i * 20);

                spriteBatch.Draw(buttons[i].currentTexture, buttons[i].position, null, buttons[i].color,
                                 0, buttons[i].origin, scale, SpriteEffects.None, 0f);
            }
            Debug.WriteLine("Draw");
            spriteBatch.End();
        }

        private void PopulateButtonArray()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i] = new Button();
        }
    }
}
