using Microsoft.Xna.Framework;

namespace PlasmaPurgatory.Generator
{
    class PatternGenerator
    {
        private PatternPreset preset;

        // TODO: Maybe remove this class completely
        public PatternGenerator(PatternPreset preset)
        {
            this.preset = preset;
            preset.ApplyPattern();
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            for (int i = 0; i < preset.Bullets.Count; i++)
                preset.Bullets[i].LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < preset.Bullets.Count; i++)
                preset.Bullets[i].Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < preset.Bullets.Count; i++)
                preset.Bullets[i].Draw(gameTime);
        }
    }
}