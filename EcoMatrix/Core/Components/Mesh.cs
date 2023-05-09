using EcoMatrix.Core.Abstracts;
using EcoMatrix.Core.BufferObjects;
using EcoMatrix.Core.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Components
{
    public class Mesh : GameObject, IDisposable
    {
        public VertexArrayObject VertexArrayObject { get; private set; }

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

            VertexArrayObject = new VertexArrayObject();
            VertexArrayObject.Bind();

            VertexBufferObject = new VertexBufferObject();
            VertexBufferObject.Bind();

            ElementBufferObject = new ElementBufferObject();
            ElementBufferObject.Bind();


            // position attribute
            GL.VertexAttribPointer(index: 0,
                                   size: Global.PositionAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: 0);
            GL.EnableVertexAttribArray(0);

            // color attribute
            GL.VertexAttribPointer(index: 1,
                                   size: Global.ColorAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: Global.PositionAttributeSize * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // normal attribute
            GL.VertexAttribPointer(index: 2,
                                   size: Global.NormalAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: (Global.PositionAttributeSize + Global.ColorAttributeSize) * sizeof(float));
            GL.EnableVertexAttribArray(2);


            // texture coordinates attribute
            GL.VertexAttribPointer(index: 3,
                                   size: Global.TextureCoordinatesAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: (Global.PositionAttributeSize + Global.ColorAttributeSize + Global.NormalAttributeSize) * sizeof(float));
            GL.EnableVertexAttribArray(3);

            VertexArrayObject.Unbind();
            VertexBufferObject.Unbind();
            ElementBufferObject.Unbind();
        }

        public void Dispose()
        {
            VertexBufferObject.Dispose();
            ElementBufferObject.Dispose();
            VertexArrayObject.Dispose();

            GC.SuppressFinalize(this);
        }

        ~Mesh()
        {
            Dispose();
        }
    }
}