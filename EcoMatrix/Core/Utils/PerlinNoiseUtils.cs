using OpenTK.Mathematics;

namespace EcoMatrix.Core.Utils
{
    public static class PerlinNoiseUtils
    {
        private static List<float> noises = new List<float>(Global.worldNoiseOctaves);
        private static PerlinNoise perlinNoise = new PerlinNoise((int)DateTime.Now.Ticks);

        public static float GetFractalNoise(float x, float y)
        {
            float worldNoiseLacunarity = Global.worldNoiseLacunarity;
            float worldNoisePersistance = Global.worldNoisePersistance;

            float maxSum = 0;

            for (int i = 0; i < Global.worldNoiseOctaves; i++)
            {
                noises.Add((float)(perlinNoise.Noise2D(x / Global.worldNoiseScale * worldNoiseLacunarity,
                                                       y / Global.worldNoiseScale * worldNoiseLacunarity) * worldNoisePersistance));

                maxSum += 1 * worldNoisePersistance;

                worldNoiseLacunarity *= worldNoiseLacunarity;
                worldNoisePersistance *= worldNoisePersistance;
            }

            float sum = 0;

            for (int i = 0; i < noises.Count; i++)
            {
                sum += noises[i];
            }

            noises.Clear();

            sum = MathHelper.MapRange(sum, -maxSum, maxSum, -1, 1);

            return sum;
        }
    }
}