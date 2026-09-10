using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TerraForge;

public class Game : GameWindow
{
    // OpenGL resources
    private int _vertexBuffer;
    private int _vertexArray;

    private Shader _shader;
    private Texture _texture;

    private float[] _vertices =
    {
        // Position          // Texture coordinates

        // First triangle
        -0.5f,  0.5f, 0.0f,  0.0f, 1.0f, // top left
         0.5f,  0.5f, 0.0f,  1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f,  1.0f, 0.0f, // bottom right

        // Second triangle
        -0.5f,  0.5f, 0.0f,  0.0f, 1.0f, // top left
         0.5f, -0.5f, 0.0f,  1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f,  0.0f, 0.0f  // bottom left
    };

    public Game(int width, int height, string title)
        : base(
            GameWindowSettings.Default,
            new NativeWindowSettings
            {
                Size = (width, height),
                Title = title
            })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        InitializeVertexData();
        InitializeShader();
        InitializeTexture();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    private void InitializeVertexData()
    {
        // Create VAO
        _vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArray);

        // Create VBO
        _vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        // Upload vertex data to GPU
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw
        );

        // Position: attribute 0
        // X, Y, Z = 3 floats
        // One vertex = 5 floats
        GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            0
        );

        GL.EnableVertexAttribArray(0);

        // Texture coordinates: attribute 1
        // U, V = 2 floats
        GL.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            3 * sizeof(float)
        );

        GL.EnableVertexAttribArray(1);
    }

    private void InitializeShader()
    {
        _shader = new Shader("shader.vert", "shader.frag");
    }

    private void InitializeTexture()
    {
        _texture = new Texture("Textures/bricks.jpeg");
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Clear previous frame
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Use shader
        _shader.Use();

        // Use texture
        _texture.Use();

        // Use vertex configuration
        GL.BindVertexArray(_vertexArray);

        // Draw 6 vertices = 2 triangles = 1 square
        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            6
        );

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
    {
        base.OnFramebufferResize(args);

        GL.Viewport(0, 0, args.Width, args.Height);
    }

    protected override void OnUnload()
    {
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);

        _shader.Dispose();

        base.OnUnload();
    }
}