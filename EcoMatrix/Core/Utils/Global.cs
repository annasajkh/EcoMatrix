using EcoMatrix.Core.Containers;
using EcoMatrix.Core.WorldGeneration;
using OpenTK.Mathematics;
using StbImageSharp;

namespace EcoMatrix.Core.Utils
{
    public static class Global
    {
        public static int windowWidth;
        public static int windowHeight;

        public static int chunkSize = 30;
        public static float chunkResolution = 50;
        public static float chunkFullSize = chunkSize * chunkResolution;

        public static int renderDistance = 8;

        public static int worldNoiseOctaves = 5;
        public static float worldNoiseLacunarity = 3f;
        public static float worldNoisePersistance = 0.25f;
        public static float worldNoiseScale = 50;

        public static float worldCenterX = 0;
        public static float worldCenterZ = 0;

        public static float worldMaxHeight = 2000;
        public static Random random = new Random();

        public static int PositionAttributeSize { get; } = 3;
        public static int ColorAttributeSize { get; } = 4;
        public static int NormalAttributeSize { get; } = 3;
        public static int TextureCoordinateAttributeSize { get; } = 2;

        public static int AllShaderAttributeSize { get; } = PositionAttributeSize +
                                                            ColorAttributeSize +
                                                            NormalAttributeSize +
                                                            TextureCoordinateAttributeSize;

        public static Vector3 playerSpawnPosition = new Vector3(0, 2000, 0);

        public static float regenerateTriggerDistance2 = (chunkFullSize * renderDistance * 0.1f) *
                                                         (chunkFullSize * renderDistance * 0.1f);

        public static Color4[] terrainColors = new Color4[]
        {
            new Color4(0.0f, 0.6f, 0.0f, 1.0f),
            new Color4(114f / 255f, 84f / 255f, 40f / 255f, 1f),
            new Color4(0.9019607843137255f, 0.8980392156862745f, 1f, 1f)
        };


        public static List<Chunk> chunks = new List<Chunk>(renderDistance * renderDistance);
        public static List<string> chunkIDs = new List<string>(renderDistance * renderDistance);
        public static List<Vertex[]> builderTerrainVertices = new List<Vertex[]>();

        public static ImageResult catImage = ImageResult.FromStream(File.OpenRead("textures/cat.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult sunImage = ImageResult.FromStream(File.OpenRead("textures/sun.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult defaultImage = ImageResult.FromStream(File.OpenRead("textures/default_texture.png"), ColorComponents.RedGreenBlueAlpha);
    }
}