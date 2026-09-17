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
    private UI.UI _ui = null!;

    private Player.Player _player = null!;
    private PlayerController _playerController = null!;
    private Keyboard _keyboard = null!;
    private Mouse _mouse = null!;

    private World.World _world = null!;
    private WorldRenderer _worldRenderer = null!;
    private WorldReference _worldReference = null!;

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
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        CursorState = CursorState.Grabbed;

        _ui = new UI.UI(Size.X, Size.Y);

        _player = new Player.Player(
            new Vector3(0, 0, 10),
            Size.X / (float)Size.Y
        );

        _worldReference = new WorldReference();
        _world = new World.World(_worldReference);
        _worldRenderer = new WorldRenderer(_player.Camera);

        _keyboard = new Keyboard(_player.Camera, _playerController);
        _mouse = new Mouse(_player.Camera);
        _fpsCounter = new FpsCounter();
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

        _fpsCounter.Update(e.Time);
        _worldRenderer.Draw(_world);

        Color consoleColor = new Color(211, 211, 211, 0.7f);
        uint lineUpperCount = 0;
        _ui.DrawText("TerraForge", 5f, 10f, lineUpperCount++, consoleColor);
        _ui.DrawText($"FPS: {_fpsCounter.FPS}", 5f, 10f, lineUpperCount++, consoleColor);
        _ui.DrawText($"X: {_player.Position.X}", 5f, 10f, lineUpperCount++, consoleColor);
        _ui.DrawText($"Y: {_player.Position.Y}", 5f, 10f, lineUpperCount++, consoleColor);
        _ui.DrawText($"Z: {_player.Position.Z}", 5f, 10f, lineUpperCount, consoleColor);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(
        FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        _worldReference.Dispose();
        base.OnUnload();
    }
}