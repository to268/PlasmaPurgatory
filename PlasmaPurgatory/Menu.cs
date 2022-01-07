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
        struct Button
        {
            public Texture2D tex;
            public Vector2 itex;
            //TODO: Add callback field
        }

        private Button bStart;
        private GraphicsDeviceManager gdm;
        private SpriteBatch sp;
        

        public Menu(GraphicsDeviceManager gdm, SpriteBatch sp)
        {
            this.gdm = gdm;
            this.sp = sp;
        }

        public void Initialize()
        {
            gdm = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            bStart.itex = new Vector2(0, 0);
            bStart = new Button();
            base.Initialize();
        }

        public void LoadContent()
        {
            bStart.tex = Content.Load<Texture2D>("PlayButton");
            base.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            sp.Begin();
            sp.Draw(bStart.tex, bStart.itex, Color.White);
            sp.End();
            base.Draw(gameTime);
        }

    }
}
