using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public static class WorldGenerator
    {
        public static void GenerateAroundPlayer(float x, float z, Matrix4 modelMatrix)
        {
            float centerX = Helpers.Snap(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.Snap(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

            for (int i = 0; i < Global.renderDistance; i++)
            {
                for (int j = 0; j < Global.renderDistance; j++)
                {
                    Chunk chunk = new Chunk(centerX + j * Global.chunkFullSize, centerZ + i * Global.chunkFullSize, modelMatrix);

                    Global.chunks.Add(chunk);
                    Global.chunkIDs.Add(chunk.ID);
                }
            }

            Global.worldCenterX = x;
            Global.worldCenterZ = z;
        }

    }
}