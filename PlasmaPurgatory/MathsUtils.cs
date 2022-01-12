using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace PlasmaPurgatory
{
    public static class MathsUtils
    {
        // TODO: Do i need to create a class for that ?
        // (r, phi)
        public struct Polar
        {
            public float magnitude; 
            public float phase;
        }

        public static Vector2 ComplexToVector(System.Numerics.Complex complex)
        {
            return new Vector2((float)complex.Real, (float)complex.Imaginary);
        }

        // TODO: Maybe remove it
        public static System.Numerics.Complex VectorToComplex(Vector2 vector)
        {
            return new System.Numerics.Complex(vector.X, vector.Y);
        }

        public static Polar ComplexToPolar(System.Numerics.Complex complex)
        {
            Polar polar = new Polar();

            // Calculation of the magnitude using the Pythagorean theorem
            float xSquared = MathF.Pow((float)complex.Real, 2f);
            float ySquared = MathF.Pow((float)complex.Imaginary, 2f);
            polar.magnitude = MathF.Sqrt(xSquared + ySquared);

            /* 
             * The phase can be a real number if the magnitude is equal to 0
             * so the phase is initialized at 0.
             * Else the phase must be a value in the interval ]-π;π]
             */
            if (polar.magnitude != 0)
                polar.phase = MathF.Atan2((float)complex.Imaginary, (float)complex.Real);
            else
                polar.phase = 0;

            return polar;
        }

        public static System.Numerics.Complex PolarToComplex(Polar polar)
        {
            /*
             * x = r cos φ
             * y = r sin φ
             */
            float x = polar.magnitude * MathF.Cos(polar.phase);
            float y = polar.magnitude * MathF.Sin(polar.phase);

            return new System.Numerics.Complex(x, y);
        }

        public static float DegresToRadians(float degres)
        {
            return degres * (MathF.PI / 180);
        }

        public static float RadiansToDegres(float radians)
        {
            return radians * (180 / MathF.PI);
        }
    }
}
