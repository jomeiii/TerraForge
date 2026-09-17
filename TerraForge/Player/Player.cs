using OpenTK.Mathematics;

namespace TerraForge.Player;

public class Player
{
    public Vector3 Position { get; set; }
    public float Yaw { get; set; } = -MathHelper.PiOver2;
    public Vector3 Velocity { get; set; }

    public bool IsGrounded { get; set; }

    public Camera.Camera Camera { get; private set; }

    public Player(Vector3 position, float aspectRatio)
    {
        Position = position;
        Yaw = MathHelper.RadiansToDegrees(Yaw);
        Velocity = Vector3.Zero;

        Camera = new Camera.Camera(position, aspectRatio);
        UpdatePosition();
    }

    public void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Camera.Position = Position;
        Camera.Yaw = Yaw;
    }
}