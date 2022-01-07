using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlasmaPurgatory
{
    class Menu : Game
    {
        private GraphicsDeviceManager gdm;

        public Menu(GraphicsDeviceManager gdm)
        {
            this.gdm = gdm;
        }

        public void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent()
        {
            base.LoadContent();
            sStart = Content.Load("Large Buttons\Large Buttons\Play Button");
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
