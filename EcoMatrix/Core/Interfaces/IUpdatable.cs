using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EcoMatrix.Core.Interfaces
{
    public interface IUpdatable
    {
        public void Update(KeyboardState keyboardState, MouseState mouseState, float delta);
    }
}