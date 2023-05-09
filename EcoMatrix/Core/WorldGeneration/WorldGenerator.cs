using EcoMatrix.Core.Utils;

namespace EcoMatrix.Core.WorldGeneration
{
    public class WorldGenerator
    {
        private static List<string> chunkIDs = new List<string>(Global.renderDistance * Global.renderDistance);

        public WorldGenerator(float x, float z)
        {
            float centerX = Helpers.Snap(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.Snap(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

            for (int i = 0; i < Global.renderDistance; i++)
            {
                for (int j = 0; j < Global.renderDistance; j++)
                {
                    Chunk chunk = new Chunk(centerX + j * Global.chunkFullSize, centerZ + i * Global.chunkFullSize);

                    Global.chunks.Add(chunk);
                    chunkIDs.Add(chunk.ID);
                }

            }

            Global.worldCenterX = x;
            Global.worldCenterZ = z;
        }

    }
}