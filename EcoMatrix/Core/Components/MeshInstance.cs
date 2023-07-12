using EcoMatrix.Core.Containers;
using OpenTK.Mathematics;

namespace EcoMatrix.Core.Components
{
    public class MeshInstance
    {
        public static MeshInstance Quad { get; } = new MeshInstance(
        
        new Vertex[]
        {
            new Vertex(new Vector3(-1, 0, -1), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0, 0)),
            new Vertex(new Vector3(-1, 0, 1), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1, 0)),
            new Vertex(new Vector3(1, 0, 1), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1, 1)),
            new Vertex(new Vector3(1, 0, -1), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0, 1))
        },

        new Indices[]
        {
            new Indices(0, 1, 3),
            new Indices(1, 2, 3)
        });

        public static MeshInstance Cube { get; } = new MeshInstance(

        new Vertex[]
        {
            new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 1.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 1.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 1.0f)),
            new Vertex(new Vector3(0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0.0f, 0.0f)),
            new Vertex(new Vector3(0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3(0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(0.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 1.0f)),
            new Vertex(new Vector3(0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f, -0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 1.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 1.0f, 1.0f)),
            new Vertex(new Vector3(0.5f,  0.5f,  0.5f), new Color4(1f, 1f, 1f, 1f), Vector3.Zero, new Vector2( 0.0f, 1.0f))
        },

        new Indices[]
        {
            new Indices(0, 3, 2),
            new Indices(2, 1, 0),
            new Indices(4, 5, 6),
            new Indices(6, 7 ,4),
            new Indices(11, 8, 9),
            new Indices(9, 10, 11),
            new Indices(12, 13, 14),
            new Indices(14, 15, 12),
            new Indices(16, 17, 18),
            new Indices(18, 19, 16),
            new Indices(20, 21, 22),
            new Indices(22, 23, 20)
        });


        public Vertex[] Vertices { get; private set; }
        public Indices[] TriangleIndices { get; private set; }

        public MeshInstance(Vertex[] vertices, Indices[] triangleIndices)
        {
            Vertices = vertices;
            TriangleIndices = triangleIndices;
        }
    }
}