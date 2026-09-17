using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TerraForge.Graphics;
using TerraForge.Input;
using TerraForge.Player;
using TerraForge.UI;
using TerraForge.World;

namespace TerraForge;

public class Game : GameWindow
{
    private Font _font = null!;

    private Shader _shader = null!;
    private Texture _sideTexture = null!;
    private Texture _sideOverlayTexture = null!;
    private Texture _topDirtTexture = null!;
    private Texture _bottomDirtTexture = null!;
    private Texture _stoneTexture = null!;

    private Shader _uiShader = null!;
    private TextRenderer _textRenderer = null!;

    private Colormap _grassColorMap = null!;

    private Player.Player _player = null!;
    private PlayerController _playerController = null!;
    private Keyboard _keyboard = null!;
    private Mouse _mouse = null!;

    private Mesh _cubeMesh = null!;
    private World.World _world = null!;

    private FpsCounter _fpsCounter = null!;

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
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(
            BlendingFactor.SrcAlpha,
            BlendingFactor.OneMinusSrcAlpha
        );

        CursorState = CursorState.Grabbed;

        _font = new Font("Resources/Roboto-Bold.ttf", 64);
        
        Console.WriteLine("FONT CREATED");

        _uiShader = new Shader(
            "ui.vert",
            "ui.frag"
        );

        _textRenderer = new TextRenderer();

        _shader = new Shader(
            "shader.vert",
            "shader.frag"
        );

        _grassColorMap = new Colormap(
            "Resources/minecraft/textures/colormap/grass.png"
        );

        _sideTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_side.png"
        );

        _sideOverlayTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_side_overlay.png"
        );

        _bottomDirtTexture = new Texture(
            "Resources/minecraft/textures/block/dirt.png"
        );

        _topDirtTexture = new Texture(
            "Resources/minecraft/textures/block/grass_block_top.png"
        );

        _stoneTexture = new Texture(
            "Resources/minecraft/textures/block/cobblestone.png"
        );

        _cubeMesh = CubeMesh.Create();

        _world = new World.World(
            mesh: _cubeMesh,
            texture: _stoneTexture,
            sideTexture: _sideTexture,
            bottonTexture: _bottomDirtTexture,
            topTexture: _topDirtTexture,
            sideOverlay: _sideOverlayTexture,
            shader: _shader,
            colormap: _grassColorMap
        );

        _player = new Player.Player(
            new Vector3(0, 0, 10),
            Size.X / (float)Size.Y
        );

        _keyboard = new Keyboard(
            _player.Camera
        );

        _mouse = new Mouse(
            _player.Camera
        );

        _fpsCounter = new FpsCounter();

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

        _shader.SetMatrix4("view", _player.Camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _player.Camera.GetProjectionMatrix());

        _world.Draw(_shader);

        _uiShader.Use();
        _uiShader.SetMatrix4(
            "projection",
            Matrix4.CreateOrthographicOffCenter(
                0,
                Size.X,
                Size.Y,
                0,
                -1,
                1
            )
        );
        _uiShader.SetInt("textTexture", 0);
        _uiShader.SetVector4("color", new Vector4(1f, 1f, 1f, 1f));
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        _font.Bind();
        _textRenderer.Draw("Hello pidoras", 100f, 100f, _font);
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

        _sideTexture.Dispose();
        _sideOverlayTexture.Dispose();
        _topDirtTexture.Dispose();
        _bottomDirtTexture.Dispose();
        _stoneTexture.Dispose();

        _shader.Dispose();

        base.OnUnload();
    }
}