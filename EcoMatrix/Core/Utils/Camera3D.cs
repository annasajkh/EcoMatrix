using OpenTK.Mathematics;

namespace EcoMatrix.Core.Utils
{
    public class Camera3D
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public Vector2 Size { get; set; }

        public float Fov { get; set; }
        public float Near { get; set; }
        public float Far { get; set; }

        public Matrix4 ViewMatrix
        {
            get
            {
                return Matrix4.LookAt(Position, Position + Direction, Vector3.UnitY);
            }
        }

        public Matrix4 ProjectionMatrix
        {
            get
            {
                return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), Size.X / Size.Y, Near, Far);
            }
        }

        public Camera3D(Vector3 position, Vector3 direction, Vector2 size, float fov, float near, float far)
        {
            Position = position;
            Direction = direction;
            Size = size;

            Fov = fov;
            Near = near;
            Far = far;
        }

        public void Resize(float width, float height)
        {
            Size = new Vector2(width, height);
        }
    }
}