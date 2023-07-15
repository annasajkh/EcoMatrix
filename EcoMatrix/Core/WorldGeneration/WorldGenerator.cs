using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public static class WorldGenerator
    {
        public static Tuple<float[], uint[]> GenerateAround(float x, float z)
        {
            float centerX = Helpers.SnapToGrid(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.SnapToGrid(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

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

        public static Tuple<float[], uint[]> UpdateAround(float x, float z)
        {
            float centerX = Helpers.SnapToGrid(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
            float centerZ = Helpers.SnapToGrid(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

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
                            if (Vector2.DistanceSquared(new Vector2(x, z),
                                                        new Vector2(Global.chunks[k].X, Global.chunks[k].Z)) >
                                Vector2.DistanceSquared(new Vector2(x, z),
                                                        new Vector2(chunkFarthestFromPlayer.X, chunkFarthestFromPlayer.Z)))
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

        public static Tuple<float[], uint[]> BuildTerrain(List<Vertex[]> allVertices)
        {
            List<Vertex> verticesResult = new List<Vertex>(allVertices.Count * allVertices[0].Length);
            List<Indices> indicesResult = new List<Indices>(allVertices.Count * allVertices[0].Length / 4);

            for (int i = 0; i < allVertices.Count; i++)
            {
                verticesResult.AddRange(allVertices[i]);
            }

            Indices[] quadIndices = new Indices[]
            {
                new Indices(0, 1, 3),
                new Indices(1, 2, 3)
            };

            for (int i = 0; i < verticesResult.Count / 4; i++)
            {
                for (int j = 0; j < quadIndices.Length; j++)
                {
                    quadIndices[j].FirstIndex += (uint)(4 * i);
                    quadIndices[j].SecondIndex += (uint)(4 * i);
                    quadIndices[j].ThirdIndex += (uint)(4 * i);
                }

                indicesResult.AddRange(quadIndices);

                quadIndices[0].FirstIndex = 0;
                quadIndices[0].SecondIndex = 1;
                quadIndices[0].ThirdIndex = 3;

                quadIndices[1].FirstIndex = 1;
                quadIndices[1].SecondIndex = 2;
                quadIndices[1].ThirdIndex = 3;
            }
            
            return Tuple.Create(Builders.VerticesBuilder(verticesResult.ToArray()), Builders.IndicesBuilder(indicesResult.ToArray()));
        }

        private static Tuple<float[], uint[]> BuildChunks()
        {
            Global.builderTerrainVertices.Clear();

            for (int i = 0; i < Global.chunks.Count; i++)
            {
                Global.builderTerrainVertices.Add(Global.chunks[i].Vertices);
            }

            return BuildTerrain(Global.builderTerrainVertices);
        }

    }
}