using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PlasmaPurgatory
{
    class Bullet : Game
    {
        private Vector2 postion;
        private Texture2D texture;

        public Vector2 position
        {
            get { return postion; }
            set { postion = value; }
        }

        public Bullet(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //texture = Content.Load <Texture2D>("path");
            base.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
