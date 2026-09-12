using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TerraForge.Graphics;
using TerraForge.PlayerController;
using TerraForge.PlayerController.Camera;
using TerraForge.World;

namespace TerraForge;

public class Game : GameWindow
{
    private Shader _shader = null!;
    private Texture _texture = null!;

    private Camera.Camera _camera = null!;
    private Keyboard _keyboard = null!;
    private Mouse _mouse = null;

    private Mesh _cubeMesh = null!;
    private World.World _world = null!;

    private Vector2 _lastPos;
    private bool _firstMove = true;
    private float _sensitivity = 1 / 5.0f;

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

        GL.Enable(EnableCap.DepthTest);

        CursorState = CursorState.Grabbed;

        _shader = new Shader(
            "shader.vert",
            "shader.frag"
        );

        _texture = new Texture(
            "Textures/ChatGPT Image 28 авг. 2026 г., 12_06_38.png"
        );

        _cubeMesh = CubeMesh.Create();

        _world = new World.World(
            _cubeMesh,
            _texture
        );

        _camera = new Camera.Camera(
            new Vector3(5, 5, 20),
            16 / 9.0f
        );

        _keyboard = new PlayerController.Keyboard(
            _camera
        );

        _mouse = new Mouse(
            _camera
        );

        _shader.Use();
        _shader.SetInt("texture0", 0);

        GL.ClearColor(
            0.2f,
            0.3f,
            0.3f,
            1.0f
        );
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (!IsFocused)
            return;

        _keyboard.Update(KeyboardState, e.Time);
        _mouse.Update(MouseState);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
            Close();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(
            ClearBufferMask.ColorBufferBit |
            ClearBufferMask.DepthBufferBit
        );

        _shader.Use();
        _texture.Use(TextureUnit.Texture0);

        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        _world.Draw(_shader);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(
        FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(
            0,
            0,
            e.Width,
            e.Height
        );
    }

    protected override void OnUnload()
    {
        _cubeMesh.Dispose();
        _texture.Dispose();
        _shader.Dispose();

        base.OnUnload();
    }
}