using OpenTK.Mathematics;

namespace EcoMatrix.Core.Utils
{
    public static class PerlinNoiseUtils
    {
        private static PerlinNoise perlinNoise = new PerlinNoise((int)DateTime.Now.Ticks);

        public static float GetFractalNoise(float x, float y)
        {
            float worldNoiseLacunarity = Global.worldNoiseLacunarity;
            float worldNoisePersistance = Global.worldNoisePersistance;

            float maxSum = 0;
			float sum = 0;

			for (int i = 0; i < Global.worldNoiseOctaves; i++)
            {
				sum += (float)(perlinNoise.Noise2D(x / Global.worldNoiseScale * worldNoiseLacunarity, y / Global.worldNoiseScale * worldNoiseLacunarity) * worldNoisePersistance);

				maxSum += worldNoisePersistance;

                worldNoiseLacunarity *= worldNoiseLacunarity;
                worldNoisePersistance *= worldNoisePersistance;
            }

            sum = MathHelper.MapRange(sum, -maxSum, maxSum, -1, 1);

            return sum;
        }
    }
}