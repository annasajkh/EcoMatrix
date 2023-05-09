using EcoMatrix.Core.WorldGeneration;
using StbImageSharp;

namespace EcoMatrix.Core.Utils
{
    public static class Global
    {
        public static int windowWidth;
        public static int windowHeight;

        public static int chunkSize = 50;
        public static float chunkResolution = 50;
        public static float chunkFullSize = chunkSize * chunkResolution;

        public static int renderDistance = 5;

        public static int worldNoiseOctaves = 3;
        public static float worldNoiseLacunarity = 2f;
        public static float worldNoisePersistance = 0.25f;
        public static float worldNoiseScale = 10;

        public static float worldCenterX = 0;
        public static float worldCenterZ = 0;

        public static float worldMaxHeight = 1000;

        public static int PositionAttributeSize { get; } = 3;
        public static int ColorAttributeSize { get; } = 4;
        public static int NormalAttributeSize { get; } = 3;
        public static int TextureCoordinatesAttributeSize { get; } = 2;

        public static int AllShaderAttributeSize { get; } = PositionAttributeSize +
                                                            ColorAttributeSize +
                                                            NormalAttributeSize +
                                                            TextureCoordinatesAttributeSize;



        public static List<Chunk> chunks = new List<Chunk>(Global.renderDistance * Global.renderDistance);

        public static ImageResult grassImage = ImageResult.FromStream(File.OpenRead("textures/grass.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult catImage = ImageResult.FromStream(File.OpenRead("textures/cat.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult sunImage = ImageResult.FromStream(File.OpenRead("textures/sun.png"), ColorComponents.RedGreenBlueAlpha);
    }
}