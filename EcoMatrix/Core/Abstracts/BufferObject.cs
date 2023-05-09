namespace EcoMatrix.Core.Abstracts
{
    public abstract class BufferObject : IDisposable
    {
        public int Handle { get; protected set; }

        public abstract void Bind();
        public abstract void Unbind();

        // Unload this buffer object from the GPU memory
        public abstract void Dispose();

        ~BufferObject()
        {
            Dispose();
        }
    }
}