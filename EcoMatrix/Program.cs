using EcoMatrix.Core;

namespace EcoMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game("Eco Matrix", 960, 540))
            {
                game.Run();
            }
        }
    }
}