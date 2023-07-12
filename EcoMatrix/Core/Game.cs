using EcoMatrix.Core.ArrayObjects;
using EcoMatrix.Core.BufferObjects;
using EcoMatrix.Core.Components;
using EcoMatrix.Core.Engine;
using EcoMatrix.Core.Entities;
using EcoMatrix.Core.Utils;
using EcoMatrix.Core.WorldGeneration;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;
using System.Drawing;

namespace EcoMatrix.Core
{
    public class Game : GameWindow
    {
        private Renderer renderer;
        private Vector3 lightPosition;

        private Texture2D sunTexture;
        private Texture2D defaultTexture;
        private Texture2D catTexture;

        // 0 - 360
        private float time;

        private Mesh sun;
        private Mesh terrain;
        private Mesh cube;

        public Player player;
        public VertexArrayObject vertexArrayObject;

        public Game(string title, int width, int height)
                : base(GameWindowSettings.Default,
                        new NativeWindowSettings()
                        {
                            Title = title,
                            Size = (width, height)
                        })
        {
            // Settings
            CenterWindow();

            Global.windowWidth = width;
            Global.windowHeight = height;

            GL.Enable(EnableCap.DepthTest);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            StbImage.stbi_set_flip_vertically_on_load(1);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            renderer = new Renderer(new Shader(vertexShaderPath: Path.GetFullPath("shaders/default.vert"),
                                               geometryShaderPath: Path.GetFullPath("shaders/default.geom"),
                                               fragmentShaderPath: Path.GetFullPath("shaders/default.frag")));

            renderer.Shader.Use();
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.ambient"), 1.25f, 1.25f, 1.25f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.diffuse"), 1f, 1f, 1f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.specular"), 1f, 1f, 1f);
            GL.Uniform1(renderer.Shader.GetUniformLocation("material.shininess"), 32f);

            GL.Uniform3(renderer.Shader.GetUniformLocation("dirLight.ambient"), 1.25f, 1.25f, 1.25f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("dirLight.diffuse"), 1f, 1f, 1f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("dirLight.specular"), 1f, 1f, 1f);
            renderer.Shader.Unuse();

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);


            // Initialization
            player = new Player(position: Global.playerSpawnPosition,
                                rotation: Vector3.Zero,
                                scale: Vector3.One,
                                cameraSize: new Vector2(Global.windowWidth, Global.windowHeight));

            cube = new Mesh(player.Position - new Vector3(0, 500, 0), Vector3.Zero, new Vector3(100, 100, 100), BufferUsageHint.StaticDraw, MeshInstance.Cube);

            sunTexture = new Texture2D(Global.sunImage);
            defaultTexture = new Texture2D(Global.defaultImage);
            catTexture = new Texture2D(Global.catImage);

            lightPosition = new Vector3(0, 100, 0);
            vertexArrayObject = new DefaultVertexArray();

            sun = new Mesh(Vector3.Zero, Vector3.Zero, new Vector3(500, 1, 500), BufferUsageHint.StaticDraw, MeshInstance.Quad);

            Tuple<float[], uint[]> spawnChunks = WorldGenerator.GenerateAround(player.Position.X, player.Position.Z);

            terrain = new Mesh(Vector3.Zero, Vector3.Zero, Vector3.One, BufferUsageHint.DynamicDraw, spawnChunks.Item1, spawnChunks.Item2);

        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnResize(ResizeEventArgs resizeEventArgs)
        {
            base.OnResize(resizeEventArgs);

            Global.windowWidth = resizeEventArgs.Width;
            Global.windowHeight = resizeEventArgs.Height;

            GL.Viewport(0, 0, resizeEventArgs.Width, resizeEventArgs.Height);

            player.Camera.Resize(resizeEventArgs.Width, resizeEventArgs.Height);
            renderer.Projection = player.Camera.ProjectionMatrix;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            renderer.Dispose();
            terrain.Dispose();
            sun.Dispose();

            sunTexture.Dispose();
            defaultTexture.Dispose();
            catTexture.Dispose();
        }

        protected override void OnUpdateFrame(FrameEventArgs frameEventArgs)
        {
            base.OnUpdateFrame(frameEventArgs);


            KeyboardState keyboardState = KeyboardState;

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (Vector2.DistanceSquared(new Vector2(player.Position.X, player.Position.Z),
                                        new Vector2(Global.worldCenterX, Global.worldCenterZ)) > Global.regenerateTriggerDistance2)
            {
                (terrain.Vertices, terrain.TriangleIndices) = WorldGenerator.UpdateAround(player.Position.X, player.Position.Z);
            }

            player.Update(KeyboardState, MouseState, (float)frameEventArgs.Time);

            renderer.View = player.Camera.ViewMatrix;

            Vector3 sunDirection = Vector3.Normalize(lightPosition - player.Position);

            renderer.Shader.Use();


            GL.Uniform3(renderer.Shader.GetUniformLocation("lightColor"), 1f, 1f, 1f);

            GL.Uniform3(renderer.Shader.GetUniformLocation("uViewPos"), player.Position);
            GL.Uniform3(renderer.Shader.GetUniformLocation("uLightPos"), lightPosition);
            GL.Uniform3(renderer.Shader.GetUniformLocation("dirLight.direction"), sunDirection.X, sunDirection.Y, sunDirection.Z);
            renderer.Shader.Unuse();

            lightPosition = player.Position + new Vector3((float)MathHelper.Cos(MathHelper.DegreesToRadians(time)), (float)MathHelper.Sin(MathHelper.DegreesToRadians(time)), 0) * 5000;

            sun.Position = lightPosition + new Vector3((float)MathHelper.Cos(MathHelper.DegreesToRadians(time)), (float)MathHelper.Sin(MathHelper.DegreesToRadians(time)), 0) * 100;
            sun.Rotation = new Vector3(0, 90, -MathHelper.RadiansToDegrees((float)MathHelper.Atan2(MathHelper.Cos(MathHelper.DegreesToRadians(time)), MathHelper.Sin(MathHelper.DegreesToRadians(time)))));

            cube.Rotation = new Vector3(cube.Rotation.X + 0.5f, 0, cube.Rotation.Z + 0.5f);

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                time += (float)frameEventArgs.Time * 20;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                time -= (float)frameEventArgs.Time * 20;
            }

            if (keyboardState.IsKeyDown(Keys.P))
            {
                CursorState = CursorState.Normal;
            }
            else
            {
                CursorState = CursorState.Grabbed;
            }

            if (time > 360)
            {
                time = 0;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Begin();
            vertexArrayObject.Bind();

            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);

            sunTexture.Bind();
            renderer.Draw(sun, vertexArrayObject);

            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.CullFace);
            GL.Disable(EnableCap.Blend);

            defaultTexture.Bind();
            renderer.Draw(terrain, vertexArrayObject);

            catTexture.Bind();

            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);

            renderer.Draw(cube, vertexArrayObject);

            renderer.End();

            

            SwapBuffers();
        }
    }
}