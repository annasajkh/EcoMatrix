using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Shapes;
using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public class Chunk
    {
        private Rectangle[] rectanglesFlatten;

        private Rectangle[,] rectangles;
        public Vertex[] Vertices { get; private set; }
        public Indices[] Indices { get; private set; }

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
            X = x;
            Z = z;

            rectangles = new Rectangle[Global.chunkSize, Global.chunkSize];

            Vertices = new Vertex[rectangles.GetLength(0) * rectangles.GetLength(1) * 4];
            rectanglesFlatten = new Rectangle[rectangles.GetLength(0) * rectangles.GetLength(1)];
            Indices = new Indices[rectanglesFlatten.Length * 2];

            GenerateMesh();
        }

        private void GenerateMesh()
        {
            uint index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    // OpenGL order
                    // 4-----1
                    // |     |
                    // |     |
                    // 3-----2

                    Vector3 vertexPosition = new Vector3(X + j * Global.chunkResolution, 0, Z + i * Global.chunkResolution);

                    float noise1 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                    (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;

                    float noise2 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                     vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise3 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                    vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise4 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                   (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;

                    if (rectangles[i, j] == null)
                    {
                        Vertex vertexTopRight = new Vertex(vertexPosition + new Vector3(Global.chunkResolution, noise1, Global.chunkResolution),
                                                           new Color4(1f, 1f, 1f, 1f),
                                                           new Vector3(),
                                                           new Vector2(1f, 1f));

                        Vertex vertexBottomRight = new Vertex(vertexPosition + new Vector3(Global.chunkResolution, noise2, 0),
                                                              new Color4(1f, 1f, 1f, 1f),
                                                              new Vector3(),
                                                              new Vector2(1f, 0f));

                        Vertex vertexBottomLeft = new Vertex(vertexPosition + new Vector3(0, noise3, 0),
                                                             new Color4(1f, 1f, 1f, 1f),
                                                             new Vector3(),
                                                             new Vector2(0f, 0f));

                        Vertex vertexTopLeft = new Vertex(vertexPosition + new Vector3(0, noise4, Global.chunkResolution),
                                                          new Color4(1f, 1f, 1f, 1f),
                                                          new Vector3(),
                                                          new Vector2(0f, 1f));

                        Indices[] indices = new Indices[] {
                            new Indices(index * 4, 1 + index * 4, 3 + index * 4),
                            new Indices(1 + index * 4, 2 + index * 4, 3 + index * 4)
                        };

                        rectangles[i, j] = new Rectangle(vertexTopRight, vertexBottomRight, vertexBottomLeft, vertexTopLeft, indices);
                    }

                    index++;
                }
            }

            MergeVertices(rectangles);
            MergeIndices(rectangles);

            Helpers.ApplyNormals(Vertices, Indices, Matrix4.Identity);
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

                    float noise1 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                    (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;

                    float noise2 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                     vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise3 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                    vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise4 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                   (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;

                    rectangles[i, j].VertexTopRight.UpdateVertex(vertexPosition + new Vector3(Global.chunkResolution, noise1, Global.chunkResolution),
                                             new Color4(1f, 1f, 1f, 1f),
                                             new Vector3(),
                                             new Vector2(1f, 1f));

                    rectangles[i, j].VertexBottomRight.UpdateVertex(vertexPosition + new Vector3(Global.chunkResolution, noise2, 0),
                                                                    new Color4(1f, 1f, 1f, 1f),
                                                                    new Vector3(),
                                                                    new Vector2(1f, 0f));

                    rectangles[i, j].VertexBottomLeft.UpdateVertex(vertexPosition + new Vector3(0, noise3, 0),
                                                                   new Color4(1f, 1f, 1f, 1f),
                                                                   new Vector3(),
                                                                   new Vector2(0f, 0f));

                    rectangles[i, j].VertexTopLeft.UpdateVertex(vertexPosition + new Vector3(0, noise4, Global.chunkResolution),
                                                                new Color4(1f, 1f, 1f, 1f),
                                                                new Vector3(),
                                                                new Vector2(0f, 1f));
                    rectangles[i, j].Indices[0] = new Indices(index * 4, 1 + index * 4, 3 + index * 4);
                    rectangles[i, j].Indices[1] = new Indices(1 + index * 4, 2 + index * 4, 3 + index * 4);

                    index++;
                }
            }

            MergeVertices(rectangles);
            MergeIndices(rectangles);

            Helpers.ApplyNormals(Vertices, Indices, Matrix4.Identity);
        }

        private void MergeVertices(Rectangle[,] rectangles)
        {
            int index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    Vertices[index] = rectangles[i, j].VertexTopRight;
                    Vertices[index + 1] = rectangles[i, j].VertexBottomRight;
                    Vertices[index + 2] = rectangles[i, j].VertexBottomLeft;
                    Vertices[index + 3] = rectangles[i, j].VertexTopLeft;

                    index += 4;
                }
            }
        }

        private void MergeIndices(Rectangle[,] rectangles)
        {
            int index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    rectanglesFlatten[index] = rectangles[i, j];

                    index++;
                }
            }

            index = 0;

            for (int i = 0; i < Indices.Length; i += 2)
            {
                Indices[i] = rectanglesFlatten[index].Indices[0];
                Indices[i + 1] = rectanglesFlatten[index].Indices[1];

                index++;
            }
        }
    }
}