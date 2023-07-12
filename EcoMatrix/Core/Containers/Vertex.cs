using OpenTK.Mathematics;

namespace EcoMatrix.Core.Containers
{
    public class Vertex
    {
        public Vector3 Position { get; set; }
        public Color4 Color { get; set; }
        public Vector3 Normal { get; set; }
        public Vector2 TextureCoordinate { get; set; }

        public Vertex(Vector3 position, Color4 color, Vector3 normal, Vector2 textureCoordinate)
        {
            Position = position;
            Color = color;
            Normal = normal;
            TextureCoordinate = textureCoordinate;
        }

        public void UpdateVertex(Vector3 position, Color4 color, Vector3 normal, Vector2 textureCoordinate)
        {
            Position = position;
            Color = color;
            Normal = normal;
            TextureCoordinate = textureCoordinate;
        }

        public override string ToString()
        {
            return $"Position: [{Position.X}, {Position.Y}, {Position.Z}]\nColor: [{Color.R}, {Color.G}, {Color.B}, {Color.A}]\nNormal: [{Normal.X}, {Normal.Y}, {Normal.Z}]\nTextureCoordinate: [{TextureCoordinate.X}, {TextureCoordinate.Y}]";
        }
    }
}