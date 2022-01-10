using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlasmaPurgatory.Generator
{
    class PatternPreset
    {
        public enum PresetName
        {
            SPIRAL,
        }

        private int bulletCount;
        private int windowWidth;
        private int windowHeight;

        private float paddingMultiplier;
        private float xPaddingDiv;
        private float yPaddingDiv;

        // TODO: Do i need to change the location of the list ?
        private List<Bullet> bullets;
        private System.Numerics.Complex complex;

        public PatternPreset(PresetName pattern, int bulletCount)
        {
            this.bulletCount = bulletCount;

            if (bulletCount < 20 || bulletCount > 120)
                throw new ArgumentOutOfRangeException("The bullet count needs to be between 20 and 120 included");

            FillPresetData(pattern);
        }

        // TODO: Refactor this function
        private Vector2 CenterPoint(Vector2 point, Vector2 target)
        {
            target -= new Vector2(windowWidth / xPaddingDiv, windowHeight / yPaddingDiv);
            float resX = target.X + (point.X * (bulletCount * paddingMultiplier));
            float resY = target.Y + (point.Y * (bulletCount * paddingMultiplier));

            return new Vector2(resX, resY);
        }

        private void FillPresetData(PresetName pattern)
        {
            switch (pattern)
            {
                case PresetName.SPIRAL:
                    SpiralCalculation();
                    break;

                default:
                    break;
            }
        }

        /*
         * x: bullet count
         * multiplier: f(x) = ((20)/(x))+0.6
         * x padding: g(x) = log(10,x)+0.9 
         * y padding: h(x) = 3.2 tan(((x)/(48)))+cos(((x)/(14)))+0.7
         */
        private void SpiralCalculation()
        {
            complex = new System.Numerics.Complex(-0.05, -0.63);
            paddingMultiplier = (20 / paddingMultiplier) + 0.6f;
            xPaddingDiv = MathF.Log(paddingMultiplier) + 0.9f;
            yPaddingDiv = MathF.Tan(paddingMultiplier / 48) + MathF.Cos(paddingMultiplier / 14) + 0.7f;
        }
    }
}
