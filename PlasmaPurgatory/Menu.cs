using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.TextureAtlases;

namespace PlasmaPurgatory
{
    class Menu : Game
    {
        private GraphicsDeviceManager gdm;
        private Texture2D sStart;
        private SpriteBatch sp;
        private Vector2 iStart;

        public Menu(GraphicsDeviceManager gdm, SpriteBatch sp)
        {
            this.gdm = gdm;
            this.sp = sp;
        }

        public void Initialize()
        {
            iStart = new Vector2(0, 0);
            base.Initialize();
        }

        public void LoadContent()
        {
            sStart = Content.Load<Texture2D>("PlayButton");
            base.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            sp.Begin();
            sp.Draw(sStart, iStart, Color.White);
            sp.End();
            base.Draw(gameTime);
        }

    }
}
