using OpenTK.Mathematics;

// perlin noise tutorial i follow https://rtouti.github.io/graphics/perlin-noise-algorithm
namespace EcoMatrix.Core.Utils
{
    public class PerlinNoise
    {
        private List<int> permutationTable = new List<int>(512);
        private Random random;
        private int seed;

        public int Seed
        {
            get
            {
                return seed;
            }
        }

        public PerlinNoise(int seed)
        {
            this.seed = seed;
            random = new Random(seed);

            for (int i = 0; i < 256; i++)
            {
                permutationTable.Add(i);
            }

            permutationTable = permutationTable.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < 256; i++)
            {
                permutationTable.Add(permutationTable[i]);
            }
        }

        private static double Fade(double t)
        {
            return ((6 * t - 15) * t + 10) * t * t * t;
        }

        private Vector2d GetConstantVector(int v)
        {
            // v is the value from the permutation table
            int h = v & 3;

            if (h == 0)
            {
                return new Vector2d(1.0f, 1.0f);
            }
            else if (h == 1)
            {
                return new Vector2d(-1.0f, 1.0f);
            }
            else if (h == 2)
            {
                return new Vector2d(-1.0f, -1.0f);
            }
            else
            {
                return new Vector2d(1.0f, -1.0f);
            }
        }

        public double Noise2D(double x, double y)
        {
            // cap the x and y value to be between 0 - 255
            int X = (int)MathHelper.Floor(x) & 255;
            int Y = (int)MathHelper.Floor(y) & 255;

            // get the double reminder
            double xf = (double)(x - MathHelper.Floor(x));
            double yf = (double)(y - MathHelper.Floor(y));

            // construct the square
            Vector2d topRight = new Vector2d(xf - 1.0, yf - 1.0);
            Vector2d topLeft = new Vector2d(xf, yf - 1.0);
            Vector2d bottomRight = new Vector2d(xf - 1.0, yf);
            Vector2d bottomLeft = new Vector2d(xf, yf);

            int valueTopRight = permutationTable[permutationTable[X + 1] + Y + 1];
            int valueTopLeft = permutationTable[permutationTable[X] + Y + 1];
            int valueBottomRight = permutationTable[permutationTable[X + 1] + Y];
            int valueBottomLeft = permutationTable[permutationTable[X] + Y];

            double dotTopRight = Vector2d.Dot(topRight, GetConstantVector(valueTopRight));
            double dotTopLeft = Vector2d.Dot(topLeft, GetConstantVector(valueTopLeft));
            double dotBottomRight = Vector2d.Dot(bottomRight, GetConstantVector(valueBottomRight));
            double dotBottomLeft = Vector2d.Dot(bottomLeft, GetConstantVector(valueBottomLeft));

            double u = Fade(xf);
            double v = Fade(yf);

            return MathHelper.Lerp(MathHelper.Lerp(dotBottomLeft, dotTopLeft, v), MathHelper.Lerp(dotBottomRight, dotTopRight, v), u);
        }
    }
}