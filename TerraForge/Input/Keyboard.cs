using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TerraForge.Input;

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

        var playerFront = new Vector3(_camera.Front.X, 0, _camera.Front.Z).Normalized();
        Vector3 right = Vector3.Normalize(Vector3.Cross(playerFront, _camera.Up));

        if (input.IsKeyDown(Keys.Space))
        {
            var cameraPosition = _camera.Position;
            cameraPosition.Y += Speed * dt;
            _camera.Position = cameraPosition;
        }

        if (input.IsKeyDown(Keys.LeftShift))
        {
            var cameraPosition = _camera.Position;
            cameraPosition.Y -= Speed * dt;
            _camera.Position = cameraPosition;
        }

        Vector3 direction = Vector3.Zero;
        if (input.IsKeyDown(Keys.W))
            direction += playerFront;

        if (input.IsKeyDown(Keys.S))
            direction -= playerFront;

        if (input.IsKeyDown(Keys.A))
            direction -= right;

        if (input.IsKeyDown(Keys.D))
            direction += right;

        if (direction.LengthSquared > 0)
            direction = direction.Normalized();

        _camera.Position += direction * Speed * dt;
    }
}