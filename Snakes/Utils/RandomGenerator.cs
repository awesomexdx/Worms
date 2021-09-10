using System;

namespace Snakes.Utils
{
    internal static class RandomGenerator
    {
        public static int NextNormal(this Random r, double mu = 0, double sigma = 5)
        {
            double u1 = r.NextDouble();

            double u2 = r.NextDouble();

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            double randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);

        }
    }
}
