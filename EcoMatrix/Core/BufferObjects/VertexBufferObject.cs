using EcoMatrix.Core.Abstracts;
using OpenTK.Graphics.OpenGL4;

namespace EcoMatrix.Core.BufferObjects
{
    public class VertexBufferObject : BufferObject
    {
        public VertexBufferObject()
        {
            Handle = GL.GenBuffer();
        }

        public void ChangeData(float[] bufferData, BufferUsageHint bufferUsageHint)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ArrayBuffer, bufferData.Length * sizeof(float), bufferData, bufferUsageHint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        }
        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public override void Dispose()
        {
            Console.WriteLine($"VertexBufferObject: {Handle} is Unloaded");

            GL.DeleteBuffer(Handle);
            GC.SuppressFinalize(this);
        }
    }
}