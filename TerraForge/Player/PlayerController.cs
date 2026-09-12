using OpenTK.Mathematics;

namespace TerraForge.Player;

public class PlayerController
{
    private const float Speed = 5f;

    public void Update(
        Player player,
        Vector2 movement,
        Camera.Camera camera,
        double deltaTime)
    {
        Vector3 forward = camera.Front;
        forward.Y = 0;
        forward.Normalize();

        Vector3 right = camera.Right;
        right.Y = 0;
        right.Normalize();

        Vector3 direction =
            forward * movement.Y +
            right * movement.X;

        if (direction.LengthSquared > 0)
            direction.Normalize();

        player.Position +=
            direction * Speed * (float)deltaTime;
    }
}