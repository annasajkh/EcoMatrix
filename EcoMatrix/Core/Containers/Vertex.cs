using OpenTK.Mathematics;

namespace EcoMatrix.Core.Containers
{
    public class Vertex
    {
        public Vector3 Position { get; set; }
        public Color4 Color { get; set; }
        public Vector3 Normal { get; set; }
        public Vector2 TextureCoordinates { get; set; }

        public Vertex(Vector3 position, Color4 color, Vector3 normal, Vector2 textureCoordinates)
        {
            Position = position;
            Color = color;
            Normal = normal;
            TextureCoordinates = textureCoordinates;
        }
    }
}