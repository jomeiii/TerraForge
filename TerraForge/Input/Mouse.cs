using System.Numerics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TerraForge.Input;

public class Mouse
{
    private readonly TerraForge.Camera.Camera _camera;

    private Vector2 _lastPosition;
    private bool _firstMove = true;

    private const float Sensitivity = 0.1f;

    public Mouse(TerraForge.Camera.Camera camera)
    {
        _camera = camera;
    }

    public void Update(MouseState mouse)
    {
        if (_firstMove)
        {
            _lastPosition = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
            return;
        }

        float deltaX = mouse.X - _lastPosition.X;
        float deltaY = mouse.Y - _lastPosition.Y;

        _lastPosition = new Vector2(mouse.X, mouse.Y);

        _camera.Yaw += deltaX * Sensitivity;
        _camera.Pitch -= deltaY * Sensitivity;
    }
}