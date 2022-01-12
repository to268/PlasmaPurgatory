using Xunit;
using System;
using PlasmaPurgatory;
using Microsoft.Xna.Framework;

namespace Tests
{
    public class MathsUtilsTests
    {
        [Theory]
        [InlineData(20, 0)]
        [InlineData(0, 86)]
        [InlineData(-87, 53)]
        [InlineData(25, -87)]
        public void ComplexToVectorTest(double real, double imaginary)
        {
            System.Numerics.Complex complex = new System.Numerics.Complex(real, imaginary);
            Vector2 res = MathsUtils.ComplexToVector(complex);
            Assert.Equal(new Vector2((float)real, (float)imaginary), res);
        }

        [Theory]
        [InlineData(20, 0)]
        [InlineData(0, 86)]
        [InlineData(-87, 53)]
        [InlineData(25, -87)]
        public void VectorToComplexTest(double real, double imaginary)
        {
            Vector2 vec = new Vector2((float)real, (float)imaginary);
            System.Numerics.Complex res = MathsUtils.VectorToComplex(vec);
            Assert.Equal(new System.Numerics.Complex(real, imaginary), res);
        }

        [Theory(Skip = "ComplexToPolar test is broken, need to be fixed")]
        [InlineData(14, 43)]
        public void ComplexToPolarTest(double real, double imaginary)
        {
            System.Numerics.Complex complex = new System.Numerics.Complex(real, imaginary);
            MathsUtils.Polar res = MathsUtils.ComplexToPolar(complex);

            // Check if the phase is in the correct range of values if r != 0
            if (res.magnitude != 0)
            {
                bool isInRange = false;
                if (res.phase >= -MathF.PI && res.phase < MathF.PI)
                    isInRange = true;

                Assert.True(isInRange);
            }

            Assert.Equal(45.22f, MathF.Round(res.magnitude, 2));
            Assert.Equal(71.97f, MathF.Round(res.phase, 2));
        }

        [Theory(Skip = "PolarToComplex test is broken, need to be fixed")]
        [InlineData(45.22f, 71.97f)]
        public void PolarToComplexTest(float magnitude, float phase)
        {
            MathsUtils.Polar polar = new MathsUtils.Polar();
            polar.magnitude = magnitude;
            polar.phase = phase;

            System.Numerics.Complex res = MathsUtils.PolarToComplex(polar);

            Assert.Equal(14, MathF.Floor((float)res.Real));
            Assert.Equal(43, MathF.Floor((float)res.Imaginary));
        }

        [Theory]
        [InlineData(90)]
        public void DegresToRadiansTest(float degres)
        {
            float radians = MathsUtils.DegresToRadians(degres);
            Assert.Equal(MathF.PI / 2, radians);
        }

        [Theory]
        [InlineData(-MathF.PI / 2)]
        public void RadiansToDegresTest(float radians)
        {
            float degres = MathsUtils.RadiansToDegres(radians);
            Assert.Equal(-90, degres);
        }
    }
}
