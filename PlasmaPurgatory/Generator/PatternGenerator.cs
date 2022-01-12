using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PlasmaPurgatory.Generator
{
    class PatternGenerator
    {
        private PatternPreset preset;
        private List<PatternPreset> presets;
        private bool isListInitialised;

        public PatternGenerator(PatternPreset preset)
        {
            this.preset = preset;
            isListInitialised = false;

            preset.ApplyPattern();
        }

        public PatternGenerator(List<PatternPreset> presets)
        {
            this.presets = presets;
            isListInitialised = true;

            foreach (PatternPreset preset in presets)
            {
                preset.ApplyPattern();
            }
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            if (!isListInitialised)
            {
                for (int i = 0; i < preset.Bullets.Count; i++)
                    preset.Bullets[i].LoadContent();
            }
            else
            {
                foreach (PatternPreset preset in presets)
                {
                    for (int i = 0; i < preset.Bullets.Count; i++)
                        preset.Bullets[i].LoadContent();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!isListInitialised)
            {
                for (int i = 0; i < preset.Bullets.Count; i++)
                    preset.Bullets[i].Update(gameTime);
            }
            else
            {
                foreach(PatternPreset preset in presets)
                {
                    for (int i = 0; i < preset.Bullets.Count; i++)
                        preset.Bullets[i].LoadContent();
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (!isListInitialised)
            {
                for (int i = 0; i < preset.Bullets.Count; i++)
                    preset.Bullets[i].Draw(gameTime);
            }
            else
            {
                foreach(PatternPreset preset in presets)
                {
                    for (int i = 0; i < preset.Bullets.Count; i++)
                        preset.Bullets[i].Draw(gameTime);
                }
            }
        }
    }
}
