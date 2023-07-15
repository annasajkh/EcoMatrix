using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Shapes;
using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public class Chunk
    {
        private Rectangle[,] rectangles;
        public Vertex[] Vertices { get; }

        public float X { get; private set; }
        public float Z { get; private set; }
        public string ID
        {
            get
            {
                return $"{X}, {Z}";
            }
        }


        public Chunk(float x, float z)
        {
            rectangles = new Rectangle[Global.chunkSize, Global.chunkSize];

            Vertices = new Vertex[rectangles.GetLength(0) * rectangles.GetLength(1) * 4];

            uint index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    if (rectangles[i, j] == null)
                    {
                        Vertex vertexTopRight = new Vertex(new Vector3(), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1f, 1f));

                        Vertex vertexBottomRight = new Vertex(new Vector3(), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1f, 0f));

                        Vertex vertexBottomLeft = new Vertex(new Vector3(), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0f, 0f));

                        Vertex vertexTopLeft = new Vertex(new Vector3(), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0f, 1f));

                        rectangles[i, j] = new Rectangle(vertexTopRight, vertexBottomRight, vertexBottomLeft, vertexTopLeft);
                    }

                    index++;
                }
            }

            UpdatePosition(x, z);
        }

        public void UpdatePosition(float x, float z)
        {
            X = x;
            Z = z;

            uint index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    Vector3 vertexPosition = new Vector3(X + j * Global.chunkResolution, 0, Z + i * Global.chunkResolution);

                    float[] noises = new float[4];
                    Color4[] colors = new Color4[4];

                    noises[0] = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.01f, (vertexPosition.Z + Global.chunkResolution) * 0.01f);
                    noises[1] = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.01f, vertexPosition.Z * 0.01f);
                    noises[2] = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.01f, vertexPosition.Z * 0.01f);
                    noises[3] = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.01f, (vertexPosition.Z + Global.chunkResolution) * 0.01f);

                    for (int k = 0; k < 4; k++)
                    {
                        noises[k] = MathHelper.MapRange(noises[k], -1, 1, 0, 1);
                        colors[k] = Helpers.Lerp3Color(Global.terrainColors[0], Global.terrainColors[1], Global.terrainColors[2], noises[k]);
                    }

                    rectangles[i, j].VertexTopRight.UpdateVertex(vertexPosition + new Vector3(Global.chunkResolution, noises[0] * Global.worldMaxHeight, Global.chunkResolution),
                                                                 colors[0],
                                                                 new Vector3(),
                                                                 new Vector2(1f, 1f));

                    rectangles[i, j].VertexBottomRight.UpdateVertex(vertexPosition + new Vector3(Global.chunkResolution, noises[1] * Global.worldMaxHeight, 0),
																	colors[1],
                                                                    new Vector3(),
                                                                    new Vector2(1f, 0f));

                    rectangles[i, j].VertexBottomLeft.UpdateVertex(vertexPosition + new Vector3(0, noises[2] * Global.worldMaxHeight, 0),
																   colors[2],
                                                                   new Vector3(),
                                                                   new Vector2(0f, 0f));

                    rectangles[i, j].VertexTopLeft.UpdateVertex(vertexPosition + new Vector3(0, noises[3] * Global.worldMaxHeight, Global.chunkResolution),
																colors[3],
                                                                new Vector3(),
                                                                new Vector2(0f, 1f));
                    index++;
                }
            }

            Helpers.MergeVertices(Vertices, rectangles);
        }
    }
}