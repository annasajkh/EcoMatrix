using OpenTK.Mathematics;

namespace EcoMatrix.Core.Abstracts
{
    public abstract class GameObject
    {
        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public GameObject(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}