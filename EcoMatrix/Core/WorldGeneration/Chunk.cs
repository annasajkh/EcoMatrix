using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Shapes;
using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.WorldGeneration
{
    public class Chunk
    {
        private Rectangle[,] rectangles;

        public float X { get; private set; }
        public float Z { get; private set; }

        public Vertex[] Vertices { get; private set; }
        public Indices[] Indices { get; private set; }


        public string ID
        {
            get
            {
                return $"{X}, {Z}";
            }
        }


        public Chunk(float x, float z, Matrix4 modelMatrix)
        {
            X = x;
            Z = z;

            rectangles = new Rectangle[Global.chunkSize, Global.chunkSize];

            (Vertices, Indices) = GenerateChunk(x, z, modelMatrix);
        }

        private Tuple<Vertex[], Indices[]> GenerateChunk(float x, float z, Matrix4 modelMatrix)
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

                    Vector3 vertexPosition = new Vector3(x + j * Global.chunkResolution, 0, z + i * Global.chunkResolution);

                    float noise1 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                    (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;

                    float noise2 = PerlinNoiseUtils.GetFractalNoise((vertexPosition.X + Global.chunkResolution) * 0.005f,
                                                                    vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise3 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                    vertexPosition.Z * 0.005f) * Global.worldMaxHeight;

                    float noise4 = PerlinNoiseUtils.GetFractalNoise(vertexPosition.X * 0.005f,
                                                                    (vertexPosition.Z + Global.chunkResolution) * 0.005f) * Global.worldMaxHeight;



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

                    index++;
                }
            }

            Vertex[] verticesMerged = MergeVertices(rectangles);
            Indices[] indicesMerged = MergeIndices(rectangles);

            Helpers.ApplyNormals(verticesMerged, indicesMerged, modelMatrix);

            return Tuple.Create(verticesMerged, indicesMerged);
        }

        private Vertex[] MergeVertices(Rectangle[,] rectangles)
        {
            Vertex[] vertices = new Vertex[rectangles.GetLength(0) * rectangles.GetLength(1) * 4];

            int index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    vertices[index] = rectangles[i, j].VertexTopRight;
                    vertices[index + 1] = rectangles[i, j].VertexBottomRight;
                    vertices[index + 2] = rectangles[i, j].VertexBottomLeft;
                    vertices[index + 3] = rectangles[i, j].VertexTopLeft;

                    index += 4;
                }
            }

            return vertices;
        }

        private Indices[] MergeIndices(Rectangle[,] rectangles)
        {
            Rectangle[] rectanglesFlatten = new Rectangle[rectangles.GetLength(0) * rectangles.GetLength(1)];

            int index = 0;

            for (int i = 0; i < rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < rectangles.GetLength(1); j++)
                {
                    rectanglesFlatten[index] = rectangles[i, j];

                    index++;
                }
            }

            Indices[] indices = new Indices[rectanglesFlatten.Length * 2];

            index = 0;

            for (int i = 0; i < indices.Length; i += 2)
            {
                indices[i] = rectanglesFlatten[index].Indices[0];
                indices[i + 1] = rectanglesFlatten[index].Indices[1];

                index++;
            }

            return indices;
        }
    }
}