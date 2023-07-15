using OpenTK.Graphics.OpenGL4;

namespace EcoMatrix.Core.Abstracts
{
    public abstract class VertexArrayObject : BufferObject
    {

        public VertexArrayObject()
        {
            Handle = GL.GenVertexArray();
        }
        public abstract void ApplyAttributes();

        public override void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);
        }


        public override void Dispose()
        {
            Console.WriteLine($"VertexArrayObject: {Handle} is Unloaded");

            GL.DeleteBuffer(Handle);
            GC.SuppressFinalize(this);
        }
    }
}