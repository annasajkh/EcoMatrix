using EcoMatrix.Core.Containers;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Utils
{
    public class Helpers
    {
        public static float Snap(float value, float snapSize)
        {
            return (float)(MathHelper.Round(value / snapSize) * snapSize);
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

                vertexA.Normal += crossedABAC;
                vertexB.Normal += crossedABAC;
                vertexC.Normal += crossedABAC;
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = Vector3.Normalize(vertices[i].Normal);
            }
        }
    }
}