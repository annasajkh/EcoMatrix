using EcoMatrix.Core.Abstracts;
using EcoMatrix.Core.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EcoMatrix.Core.Entities
{
    public class Player : Entity
    {
        private float speed = 500f;

        private Vector2 lastMousePosition;

        private float yaw;
        private float pitch;

        private float sensitivity = 0.4f;

        public Camera3D Camera { get; }

        public Player(Vector3 position, Vector3 rotation, Vector3 scale, Vector2 cameraSize)
            : base(position, rotation, scale)
        {
            Camera = new Camera3D(position: position,
                                  direction: Vector3.Normalize(new Vector3(0, 0, 0) - position),
                                  size: cameraSize,
                                  fov: 45,
                                  near: 0.1f,
                                  far: 10000f);
        }

        public void GetInput(KeyboardState keyboardState, MouseState mouseState, float delta)
        {

            Vector2 mousePosition = mouseState.Position;

            float deltaX = mousePosition.X - lastMousePosition.X;
            float deltaY = mousePosition.Y - lastMousePosition.Y;

            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            else if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }

            Vector3 cameraDirection = new Vector3();

            cameraDirection.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
            cameraDirection.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            cameraDirection.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));

            Camera.Direction = Vector3.Normalize(cameraDirection);

            lastMousePosition = new Vector2(mousePosition.X, mousePosition.Y);

            Vector3 dir = new Vector3();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                dir += Camera.Direction;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                dir -= Camera.Direction;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                dir -= Vector3.Normalize(Vector3.Cross(Camera.Direction, Vector3.UnitY));
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                dir += Vector3.Normalize(Vector3.Cross(Camera.Direction, Vector3.UnitY));
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                dir += Vector3.UnitY;
            }
            else if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                dir -= Vector3.UnitY;
            }

            dir.Normalize();

            Velocity = dir * speed;

            if (!(keyboardState.IsKeyDown(Keys.W) ||
                  keyboardState.IsKeyDown(Keys.A) ||
                  keyboardState.IsKeyDown(Keys.S) ||
                  keyboardState.IsKeyDown(Keys.D) ||
                  keyboardState.IsKeyDown(Keys.Space) ||
                  keyboardState.IsKeyDown(Keys.LeftShift)))
            {
                Velocity = Vector3.Zero;
            }
        }


        public override void Update(KeyboardState keyboardState, MouseState mouseState, float delta)
        {
            GetInput(keyboardState, mouseState, delta);

            Camera.Position = Position;

            base.Update(keyboardState, mouseState, delta);
        }
    }
}