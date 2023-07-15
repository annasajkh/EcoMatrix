using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Shapes;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Utils
{
    public static class Helpers
    {
        public static float SnapToGrid(float value, float gridSize)
        {
            return (float)(MathHelper.Round(value / gridSize) * gridSize);
        }

        public static Color4 LerpColor(Color4 color1, Color4 color2, float t)
        {
            return new Color4(color1.R + (color2.R - color1.R) * t,
                              color1.G + (color2.G - color1.G) * t,
                              color1.B + (color2.B - color1.B) * t,
                              color1.A + (color2.A - color1.A) * t);
        }

        // t is between 0 - 1
        public static Color4 Lerp3Color(Color4 color1, Color4 color2, Color4 color3, float t)
        {
            if (t < 0.5f)
            {
                return LerpColor(color1, color2, t * 2);
            }
            else
            {
                return LerpColor(color2, color3, (t - 0.5f) * 2);
            }
        }

        public static void ApplyNormals(Vertex[] vertices, Indices[] triangleIndices, Matrix4 modelMatrix)
        {
            for (int i = 0; i < triangleIndices.Length; i++)
            {
                Vertex vertexA = vertices[triangleIndices[i].FirstIndex];
                Vertex vertexB = vertices[triangleIndices[i].SecondIndex];
                Vertex vertexC = vertices[triangleIndices[i].ThirdIndex];

                Vector3 positionA = (new Vector4(vertexA.Position, 1.0f) * modelMatrix).Xyz;
                Vector3 positionB = (new Vector4(vertexB.Position, 1.0f) * modelMatrix).Xyz;
                Vector3 positionC = (new Vector4(vertexC.Position, 1.0f) * modelMatrix).Xyz;

                Vector3 positionAB = positionB - positionA;
                Vector3 positionAC = positionC - positionA;

                Vector3 crossedABAC = Vector3.Cross(positionAB, positionAC).Normalized();

                vertexA.Normal = crossedABAC;
                vertexB.Normal = crossedABAC;
                vertexC.Normal = crossedABAC;
            }
        }

        public static void MergeVertices(Vertex[] Vertices, Rectangle[,] rectangles)
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
    }
}