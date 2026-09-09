using System.Diagnostics;
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
    private Stopwatch _timer;

    // Each vertex contains:
    // position (X, Y, Z) + color (R, G, B)
    private readonly float[] _vertices =
    {
        // Position        // Color
        0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom right - red
        -0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom left  - green
        0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f // top          - blue
    };
    
    private float[] _texCoords = {
        0.0f, 0.0f,  // lower-left corner  
        1.0f, 0.0f,  // lower-right corner
        0.5f, 1.0f   // top-center corner
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
        InitializeTimer();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    // Creates VAO and VBO and tells OpenGL
    // how to interpret the data inside the VBO.
    private void InitializeVertexData()
    {
        // VAO stores the configuration of our vertex data. (Vertex Array Object)
        _vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArray);

        // VBO stores the actual vertex data. (Vertex Buffer Object)
        _vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw
        );

        // Position: attribute 0
        // X, Y, Z = 3 floats
        // One complete vertex = 6 floats
        GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            6 * sizeof(float),
            0
        );

        GL.EnableVertexAttribArray(0);

        // Color: attribute 1
        // R, G, B = 3 floats
        // Color starts after the first 3 floats.
        GL.VertexAttribPointer(
            1,
            3,
            VertexAttribPointerType.Float,
            false,
            6 * sizeof(float),
            3 * sizeof(float)
        );

        GL.EnableVertexAttribArray(1);
    }

    // Creates and compiles the shader program.
    private void InitializeShader()
    {
        _shader = new Shader("shader.vert", "shader.frag");
    }

    // Starts the timer used for animations.
    private void InitializeTimer()
    {
        _timer = Stopwatch.StartNew();
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

        // Clear the previous frame.
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Select our shader program.
        _shader.Use();

        // Select our vertex configuration.
        GL.BindVertexArray(_vertexArray);

        // Draw 3 vertices as one triangle.
        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            3
        );

        // Show the rendered frame.
        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
    {
        base.OnFramebufferResize(args);

        GL.Viewport(0, 0, args.Width, args.Height);
    }

    protected override void OnUnload()
    {
        // Free GPU resources.
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);

        _shader.Dispose();

        base.OnUnload();
    }
}