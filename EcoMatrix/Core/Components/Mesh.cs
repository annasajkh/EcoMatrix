using EcoMatrix.Core.Abstracts;
using EcoMatrix.Core.BufferObjects;
using EcoMatrix.Core.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Components
{
    public class Mesh : GameObject, IDisposable
    {
        public VertexBufferObject VertexBufferObject { get; private set; }

        public ElementBufferObject ElementBufferObject { get; private set; }

        public BufferUsageHint BufferUsageHint { get; set; }

        private float[] vertices;

        private uint[] triangleIndices;


        public float[] Vertices
        {
            get
            {
                return vertices;
            }

            set
            {
                vertices = value;
                VertexBufferObject.Data(value);
            }
        }

        public uint[] TriangleIndices
        {
            get
            {
                return triangleIndices;
            }

            set
            {
                triangleIndices = value;
                ElementBufferObject.Data(value);
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

        public Mesh(Vector3 position, 
                    Vector3 rotation, 
                    Vector3 scale, 
                    BufferUsageHint bufferUsageHint,
                    float[] vertices,
                    uint[] triangleIndices)
            : base(position, rotation, scale)
        {
            this.vertices = vertices;
            this.triangleIndices = triangleIndices;

            VertexBufferObject = new VertexBufferObject(bufferUsageHint);
            ElementBufferObject = new ElementBufferObject(bufferUsageHint);

            Vertices = vertices;
            TriangleIndices = triangleIndices;

            BufferUsageHint = bufferUsageHint;
        }


        public Mesh(Vector3 position,
            Vector3 rotation,
            Vector3 scale,
            BufferUsageHint bufferUsageHint,
            MeshInstance meshInstance)
        : this(position, rotation, scale, bufferUsageHint, Builders.VerticesBuilder(meshInstance.Vertices), Builders.IndicesBuilder(meshInstance.TriangleIndices))
        {

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