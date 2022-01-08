using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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
        }

        private Button[] buttons;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;

        private const int BUTTONS_COUNT = 1;

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
            buttons[0].position = new Vector2(0, 0);
            buttons[0].color = Color.White;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            // Start button texture loading
            buttons[0].texture = contentManager.Load<Texture2D>("PlayButton");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Button button in buttons)
            {
                DrawButton(button);
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

        private void DrawButton(Button button)
        {
            spriteBatch.Draw(button.texture, button.position, button.color);
        }

    }
}
