using EcoMatrix.Core.Containers;
using EcoMatrix.Core.Entities;
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

        public static int renderDistance = 7;

        public static int worldNoiseOctaves = 3;
        public static float worldNoiseLacunarity = 2f;
        public static float worldNoisePersistance = 0.25f;
        public static float worldNoiseScale = 20;

        public static float worldCenterX = 0;
        public static float worldCenterZ = 0;

        public static float worldMaxHeight = 1000;
        public static Random random = new Random();

        public static int PositionAttributeSize { get; } = 3;
        public static int ColorAttributeSize { get; } = 4;
        public static int NormalAttributeSize { get; } = 3;
        public static int TextureCoordinatesAttributeSize { get; } = 2;

        public static int AllShaderAttributeSize { get; } = PositionAttributeSize +
                                                            ColorAttributeSize +
                                                            NormalAttributeSize +
                                                            TextureCoordinatesAttributeSize;

        public static Vector3 playerSpawnPosition = new Vector3(0, 1000, 0);

        public static float regenerateTriggerDistance2 = (chunkFullSize * renderDistance * 0.25f) *
                                                         (chunkFullSize * renderDistance * 0.25f);


        public static List<Chunk> chunks = new List<Chunk>(renderDistance * renderDistance);
        public static List<string> chunkIDs = new List<string>(renderDistance * renderDistance);
        public static List<Vertex[]> builderTerrainVertices = new List<Vertex[]>();
        public static List<Indices[]> builderTerrainIndices = new List<Indices[]>();

        public static ImageResult grassImage = ImageResult.FromStream(File.OpenRead("textures/grass.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult catImage = ImageResult.FromStream(File.OpenRead("textures/cat.png"), ColorComponents.RedGreenBlueAlpha);
        public static ImageResult sunImage = ImageResult.FromStream(File.OpenRead("textures/sun.png"), ColorComponents.RedGreenBlueAlpha);

        public static Player player;
    }
}