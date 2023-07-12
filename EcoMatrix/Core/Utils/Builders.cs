using EcoMatrix.Core.Containers;

namespace EcoMatrix.Core.Utils
{
    public static class Builders
    {
        public static float[] VerticesBuilder(Vertex[] vertices)
        {
            float[] verticesResult = new float[Global.AllShaderAttributeSize * vertices.Length];

            int index = 0;

            for (int i = 0; i < verticesResult.Length; i += Global.AllShaderAttributeSize)
            {
                verticesResult[i] = vertices[index].Position.X;
                verticesResult[i + 1] = vertices[index].Position.Y;
                verticesResult[i + 2] = vertices[index].Position.Z;

                verticesResult[i + 3] = vertices[index].Color.R;
                verticesResult[i + 4] = vertices[index].Color.G;
                verticesResult[i + 5] = vertices[index].Color.B;
                verticesResult[i + 6] = vertices[index].Color.A;

                verticesResult[i + 7] = vertices[index].Normal.X;
                verticesResult[i + 8] = vertices[index].Normal.Y;
                verticesResult[i + 9] = vertices[index].Normal.Z;

                verticesResult[i + 10] = vertices[index].TextureCoordinate.X;
                verticesResult[i + 11] = vertices[index].TextureCoordinate.Y;

                index++;
            }


            return verticesResult;

        }

        // offset is for offsetting the indices
        public static uint[] IndicesBuilder(Indices[] indices)
        {
            uint[] indicesResult = new uint[3 * indices.Length];

            int index = 0;

            for (int i = 0; i < indicesResult.Length; i += 3)
            {
                indicesResult[i] = indices[index].FirstIndex;
                indicesResult[i + 1] = indices[index].SecondIndex;
                indicesResult[i + 2] = indices[index].ThirdIndex;

                index++;
            }

            return indicesResult;
        }
    }
}