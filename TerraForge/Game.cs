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
    private int _elementBuffer;

    private Shader _shader;
    private Texture _texture;
    private Texture _texture2;

    private float[] _vertices =
    {
        // Position          // Texture coordinates

        // Four vertices
        -0.5f,  0.5f, 0.0f,  0.0f, 1.0f, // top left
         0.5f,  0.5f, 0.0f,  1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f,  1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f,  0.0f, 0.0f  // bottom left
    };

    private readonly uint[] _indices =
    {
        0, 1, 2,
        0, 2, 3
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

        // Create EBO
        _elementBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBuffer);

        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            _indices.Length * sizeof(uint),
            _indices,
            BufferUsageHint.StaticDraw
        );
    }

    private void InitializeShader()
    {
        _shader = new Shader("shader.vert", "shader.frag");
        _shader.Use();
        _shader.SetInt("texture0", 0);
        _shader.SetInt("texture1", 1);
    }

    private void InitializeTexture()
    {
        _texture = new Texture("Textures/ChatGPT Image 28 авг. 2026 г., 12_06_38.png");
        _texture2 = new Texture("Textures/bricks.jpeg");
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
    
        GL.Clear(ClearBufferMask.ColorBufferBit); // Clear previous frame
        GL.BindVertexArray(_vertexArray); // Use vertex configuration

        // Use shader
        _shader.Use();

        // Use texture
        _texture.Use(TextureUnit.Texture0);
        _texture2.Use(TextureUnit.Texture1);

        GL.DrawElements(
            PrimitiveType.Triangles,
            _indices.Length,
            DrawElementsType.UnsignedInt,
            0
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
        GL.DeleteBuffer(_elementBuffer);
        GL.DeleteVertexArray(_vertexArray);

        _shader.Dispose();

        base.OnUnload();
    }
}