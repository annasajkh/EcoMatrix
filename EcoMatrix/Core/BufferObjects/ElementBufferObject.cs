using EcoMatrix.Core.Abstracts;
using OpenTK.Graphics.OpenGL4;

namespace EcoMatrix.Core.BufferObjects
{
    public class ElementBufferObject : BufferObject
    {
        public ElementBufferObject()
        {
            Handle = GL.GenBuffer();
        }

        public void ChangeData(uint[] bufferData, BufferUsageHint bufferUsageHint)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, bufferData.Length * sizeof(uint), bufferData, bufferUsageHint);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        }
        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public override void Dispose()
        {
            Console.WriteLine($"ElementBufferObject: {Handle} is Unloaded");

            GL.DeleteBuffer(Handle);
            GC.SuppressFinalize(this);
        }
    }
}