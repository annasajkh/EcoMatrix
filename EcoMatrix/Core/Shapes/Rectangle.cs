using EcoMatrix.Core.Containers;

namespace EcoMatrix.Core.Shapes
{
    public class Rectangle
    {
        // OpenGL order
        // 4-----1
        // |     |
        // |     |
        // 3-----2

        public Vertex VertexTopRight { get; set; }
        public Vertex VertexBottomRight { get; set; }
        public Vertex VertexBottomLeft { get; set; }
        public Vertex VertexTopLeft { get; set; }

        public Rectangle(Vertex vertexTopRight, Vertex vertexBottomRight, Vertex vertexBottomLeft, Vertex vertexTopLeft)
        {
            VertexTopRight = vertexTopRight;
            VertexBottomRight = vertexBottomRight;
            VertexBottomLeft = vertexBottomLeft;
            VertexTopLeft = vertexTopLeft;
        }


        public void UpdateRectangle(Vertex vertexTopRight, Vertex vertexBottomRight, Vertex vertexBottomLeft, Vertex vertexTopLeft)
        {
            VertexTopRight = vertexTopRight;
            VertexBottomRight = vertexBottomRight;
            VertexBottomLeft = vertexBottomLeft;
            VertexTopLeft = vertexTopLeft;
        }
    }
}