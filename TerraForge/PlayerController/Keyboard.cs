using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TerraForge.PlayerController;

public class Keyboard
{
    private readonly TerraForge.Camera.Camera _camera;

    public float Speed { get; set; } = 5.0f;

    public Keyboard(TerraForge.Camera.Camera camera)
    {
        _camera = camera;
    }

    public void Update(KeyboardState input, double deltaTime)
    {
        float dt = (float)deltaTime;

        if (input.IsKeyDown(Keys.W))
        {
            _camera.Position += _camera.Front * Speed * dt;
        }

        if (input.IsKeyDown(Keys.S))
        {
            _camera.Position -= _camera.Front * Speed * dt;
        }

        Vector3 right = Vector3.Normalize(
            Vector3.Cross(_camera.Front, _camera.Up)
        );

        if (input.IsKeyDown(Keys.A))
        {
            _camera.Position -= right * Speed * dt;
        }

        if (input.IsKeyDown(Keys.D))
        {
            _camera.Position += right * Speed * dt;
        }

        if (input.IsKeyDown(Keys.Space))
        {
            _camera.Position += _camera.Up * Speed * dt;
        }

        if (input.IsKeyDown(Keys.LeftShift))
        {
            _camera.Position -= _camera.Up * Speed * dt;
        }
    }
}