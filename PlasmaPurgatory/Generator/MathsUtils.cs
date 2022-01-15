using System;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PlasmaPurgatory.Generator
{
    public static class MathsUtils
    {
        // (r, phi)
        public struct Polar
        {
            public float magnitude; 
            public float phase;
        }

        public static Vector2 ComplexToVector(Complex complex)
        {
            return new Vector2((float)complex.Real, (float)complex.Imaginary);
        }

        public static Complex VectorToComplex(Vector2 vector)
        {
            return new Complex(vector.X, vector.Y);
        }

        public static Polar ComplexToPolar(Complex complex)
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

        public static Complex PolarToComplex(Polar polar)
        {
            /*
             * x = r cos φ
             * y = r sin φ
             */
            float x = polar.magnitude * MathF.Cos(polar.phase);
            float y = polar.magnitude * MathF.Sin(polar.phase);

            return new Complex(x, y);
        }

        public static float DegresToRadians(float degres)
        {
            return degres * (MathF.PI / 180);
        }

        public static float RadiansToDegres(float radians)
        {
            return radians * (180 / MathF.PI);
        }
        
        public static Polar VectorToPolar(Vector2 vector)
        {
            Complex complex = VectorToComplex(vector);
            return ComplexToPolar(complex);
        }
        
        public static Vector2 PolarToVector(Polar polar)
        {
            Complex complex = PolarToComplex(polar);
            return ComplexToVector(complex);
        }
    }
}
