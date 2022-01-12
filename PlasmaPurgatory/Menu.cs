using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using MenuBuddy;

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
            public Rectangle hitbox;

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
           
           /* buttons[0].position = new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/3);
            buttons[0].color = Color.White;
            

            buttons[1].position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height /2);
            buttons[1].color = Color.White;*/
            
            
            
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            /* Start button texture loading
            buttons[0].texture = contentManager.Load<Texture2D>("PlayButtonRS");
            buttons[0].origin = new Vector2(buttons[0].texture.Width / 2, buttons[0].texture.Height / 2);

            buttons[1].texture = contentManager.Load<Texture2D>("LargeButtons\\LargeButtons\\OptionsButton");
            buttons[1].origin = new Vector2(buttons[1].texture.Width / 2, buttons[1].texture.Height / 2);*/
        }

        bool EnterButton(Button button)
        {
            
            if (mousePosition.X < button.texture.Bounds.X + button.texture.Width &&
                    mousePosition.X > button.texture.Bounds.X &&
                    mousePosition.Y < button.texture.Bounds.Y + button.texture.Height &&
                    mousePosition.Y > button.texture.Bounds.Y)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            
            
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed && buttons[0].hitbox.Contains(mousePosition))
            {
                
                buttons[0].texture = contentManager.Load<Texture2D>("LargeButtons\\ColoredLargeButtons\\Optionscol_Button");
                Debug.WriteLine(buttons[0].texture.Width + " " + buttons[0].texture.Height);
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            for (int i = 0; i < BUTTONS_COUNT; i++)
            {
                /*buttons[i].hitbox = new Rectangle((int)buttons[i].position.X, (int)buttons[i].position.Y,
                                              buttons[i].texture.Width, buttons[i].texture.Height);*/
                spriteBatch.Draw(buttons[i].texture, buttons[i].position, null, buttons[i].color,
                                 0, buttons[i].origin, scale, SpriteEffects.None, 0f);
                
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
