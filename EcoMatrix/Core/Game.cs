using EcoMatrix.Core.BufferObjects;
using EcoMatrix.Core.Components;
using EcoMatrix.Core.Containers;
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
        private Player player;
        private Chunk chunk;
        private Renderer renderer;
        private Vector3 lightPosition;

        private Vertex[] vertices = {
            new Vertex(new Vector3(-100f, 0, -100f), new Color4(1f, 1f, 1f, 1f), new Vector3(), new Vector2(1f, 1f)),
            new Vertex(new Vector3(-100f, 0f, 100f), new Color4(1f, 1f, 1f, 1f), new Vector3(), new Vector2(1f, 0f)),
            new Vertex(new Vector3(100f, 0, 100f), new Color4(1f, 1f, 1f, 1f), new Vector3(), new Vector2(0f, 0f)),
            new Vertex(new Vector3(100f, 0, -100f), new Color4(1f, 1f, 1f, 1f), new Vector3(), new Vector2(0f, 1f))
        };

        private Indices[] triangleIndices = {
            new Indices(0, 1, 3),
            new Indices(1, 2, 3)
        };

        private Texture2D texture;

        private float time;

        private Mesh sun;

        public Game(string title, int width, int height)
                : base(GameWindowSettings.Default,
                        new NativeWindowSettings()
                        {
                            Title = title,
                            Size = (width, height)
                        })
        {
            // Settings
            CursorState = CursorState.Grabbed;
            CenterWindow();

            Global.windowWidth = width;
            Global.windowHeight = height;

            GL.Enable(EnableCap.DepthTest);

            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            StbImage.stbi_set_flip_vertically_on_load(1);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            renderer = new Renderer(new Shader(vertexShaderPath: Path.GetFullPath("shaders/vertex.vert"),
                                               fragmentShaderPath: Path.GetFullPath("shaders/fragment.frag")));

            renderer.Shader.Use();
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.ambient"), 1f, 1f, 1f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.diffuse"), 1f, 1f, 1f);
            GL.Uniform3(renderer.Shader.GetUniformLocation("material.specular"), 1f, 1f, 1f);
            GL.Uniform1(renderer.Shader.GetUniformLocation("material.shininess"), 32f);
            renderer.Shader.Unuse();




            // Initialization
            texture = new Texture2D(Global.grassImage);
            texture.Bind();


            player = new Player(position: new Vector3(1000, 1000, 1000),
                    rotation: Vector3.Zero,
                    scale: Vector3.One,
                    cameraSize: new Vector2(Global.windowWidth, Global.windowHeight));

            lightPosition = new Vector3(0, 100, 0);

            sun = new Mesh(Vector3.Zero, Vector3.Zero, new Vector3(15, 1, 15));

            Helpers.ApplyNormals(vertices, triangleIndices, sun.ModelMatrix);


            sun.Vertices = Builders.VerticesBuilder(vertices);
            sun.Indices = Builders.IndicesBuilder(triangleIndices);

            chunk = new Chunk(0, 0);
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
            chunk.Dispose();
            sun.Dispose();
        }

        protected override void OnUpdateFrame(FrameEventArgs frameEventArgs)
        {
            base.OnUpdateFrame(frameEventArgs);


            KeyboardState keyboardState = KeyboardState;

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }


            player.Update(KeyboardState, MouseState, (float)frameEventArgs.Time);

            Helpers.ApplyNormals(vertices, triangleIndices, sun.ModelMatrix);

            sun.Vertices = Builders.VerticesBuilder(vertices);
            sun.Indices = Builders.IndicesBuilder(triangleIndices);

            renderer.View = player.Camera.ViewMatrix;

            renderer.Shader.Use();
            GL.Uniform3(renderer.Shader.GetUniformLocation("uViewPos"), player.Position);
            GL.Uniform3(renderer.Shader.GetUniformLocation("uLightPos"), lightPosition);
            renderer.Shader.Unuse();

            lightPosition = player.Position + new Vector3((float)MathHelper.Cos(time), (float)MathHelper.Sin(time), 0) * 10000;

            sun.Position = lightPosition - new Vector3((float)MathHelper.Cos(time), (float)MathHelper.Sin(time), 0) * 100;

            sun.Rotation = new Vector3(0, -90, -MathHelper.RadiansToDegrees((float)MathHelper.Atan2(MathHelper.Cos(time), MathHelper.Sin(time))));

            time += (float)frameEventArgs.Time * 0.1f;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Begin();

            texture.ChangeTexture(Global.catImage);
            renderer.Draw(sun);

            texture.ChangeTexture(Global.grassImage);
            renderer.Draw(chunk.Mesh);

            renderer.End();

            SwapBuffers();
        }
    }
}