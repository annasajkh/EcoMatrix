using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public static class WorldGenerator
    {
        public static Tuple<float[], uint[]> GenerateAroundPlayer(float x, float z)
        {
            float centerX = Helpers.Snap(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.Snap(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

            for (int i = 0; i < Global.renderDistance; i++)
            {
                for (int j = 0; j < Global.renderDistance; j++)
                {
                    Chunk chunk = new Chunk(centerX + j * Global.chunkFullSize, centerZ + i * Global.chunkFullSize);

                    Global.chunks.Add(chunk);
                    Global.chunkIDs.Add(chunk.ID);
                }
            }

            Global.worldCenterX = x;
            Global.worldCenterZ = z;

            return BuildChunks();
        }

        public static Tuple<float[], uint[]> UpdateAroundPlayer(float x, float z)
        {
            float centerX = Helpers.Snap(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.Snap(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

            for (int i = 0; i < Global.renderDistance; i++)
            {
                for (int j = 0; j < Global.renderDistance; j++)
                {
                    float chunkX = centerX + j * Global.chunkFullSize;
                    float chunkZ = centerZ + i * Global.chunkFullSize;

                    string chunkID = $"{chunkX}, {chunkZ}";

                    if (!Global.chunkIDs.Contains(chunkID))
                    {
                        Chunk chunkFarthestFromPlayer = Global.chunks[0];

                        for (int k = 0; k < Global.chunks.Count; k++)
                        {
                            if (Vector2.DistanceSquared(new Vector2(Global.player.Position.X, Global.player.Position.Z),
                                                        new Vector2(chunkFarthestFromPlayer.X, chunkFarthestFromPlayer.Z)) <
                                Vector2.DistanceSquared(new Vector2(Global.player.Position.X, Global.player.Position.Z),
                                                        new Vector2(Global.chunks[k].X, Global.chunks[k].Z)))
                            {
                                chunkFarthestFromPlayer = Global.chunks[k];
                            }

                        }

                        Global.chunks.Remove(chunkFarthestFromPlayer);
                        Global.chunkIDs.Remove(chunkFarthestFromPlayer.ID);

                        chunkFarthestFromPlayer.UpdatePosition(chunkX, chunkZ);

                        Global.chunks.Add(chunkFarthestFromPlayer);
                        Global.chunkIDs.Add(chunkFarthestFromPlayer.ID);
                    }
                }
            }

            Global.worldCenterX = x;
            Global.worldCenterZ = z;

            return BuildChunks();
        }

        private static Tuple<float[], uint[]> BuildChunks()
        {
            Global.builderTerrainVertices.Clear();
            Global.builderTerrainIndices.Clear();

            for (int i = 0; i < Global.chunks.Count; i++)
            {
                Global.builderTerrainVertices.Add(Global.chunks[i].Vertices);
                Global.builderTerrainIndices.Add(Global.chunks[i].Indices);
            }

            return Builders.BuildAll(Global.builderTerrainVertices, Global.builderTerrainIndices);
        }

    }
}