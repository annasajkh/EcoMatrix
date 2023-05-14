using EcoMatrix.Core.Abstracts;
using EcoMatrix.Core.BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Components
{
    public class Mesh : GameObject, IDisposable
    {
        public VertexBufferObject VertexBufferObject { get; private set; }

        public ElementBufferObject ElementBufferObject { get; private set; }


        private float[] vertices;

        private uint[] indices;


        public float[] Vertices
        {
            get
            {
                return vertices;
            }

            set
            {
                vertices = value;
                VertexBufferObject.ChangeData(value, BufferUsageHint.DynamicDraw);
            }
        }

        public uint[] Indices
        {
            get
            {
                return indices;
            }

            set
            {
                indices = value;
                ElementBufferObject.ChangeData(value, BufferUsageHint.DynamicDraw);
            }
        }

        public Matrix4 ModelMatrix
        {
            get
            {
                Vector3 rotationRadians = new Vector3(MathHelper.DegreesToRadians(Rotation.X),
                                                      MathHelper.DegreesToRadians(Rotation.Y),
                                                      MathHelper.DegreesToRadians(Rotation.Z));

                return Matrix4.CreateScale(Scale.X, Scale.Y, Scale.Z) *
                       Matrix4.CreateRotationX(rotationRadians.X) *
                       Matrix4.CreateRotationY(rotationRadians.Y) *
                       Matrix4.CreateRotationZ(rotationRadians.Z) *
                       Matrix4.CreateTranslation(Position.X, Position.Y, Position.Z);
            }
        }

        public Mesh(Vector3 position, Vector3 rotation, Vector3 scale)
            : base(position, rotation, scale)
        {
            vertices = new float[1];
            indices = new uint[1];

            VertexBufferObject = new VertexBufferObject();
            ElementBufferObject = new ElementBufferObject();
        }

        public void Bind()
        {
            VertexBufferObject.Bind();
            ElementBufferObject.Bind();
        }

        public void Unbind()
        {
            VertexBufferObject.Unbind();
            ElementBufferObject.Unbind();
        }

        public void Dispose()
        {
            VertexBufferObject.Dispose();
            ElementBufferObject.Dispose();

            GC.SuppressFinalize(this);
        }

        ~Mesh()
        {
            Dispose();
        }
    }
}