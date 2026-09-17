using OpenTK.Mathematics;
using TerraForge.Physics;

namespace TerraForge.Player;

public class Player
{
    public AABB AABB { get; private set; }
    public Vector3 Movement { get; set; }
    public Vector3 Position { get; set; }
    public float Yaw { get; set; } = -MathHelper.PiOver2;
    public Vector3 Velocity { get; set; }
    public bool IsGrounded { get; set; }
    public Camera.Camera Camera { get; private set; }

    private readonly Vector3 _size = new(0.6f, 1.8f, 0.6f);
    private readonly float _offsetCameraY = -0.2f;

    public Vector3 Size => _size;

    public Player(Vector3 position, float aspectRatio)
    {
        Position = position;
        Yaw = MathHelper.RadiansToDegrees(Yaw);
        Velocity = Vector3.Zero;

        AABB = new AABB(Position, _size);

        Camera = new Camera.Camera(position, aspectRatio);
        UpdatePosition();
    }

    public void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Camera.Position = Position + _size.Y * new Vector3(0, 1f, 0) + new Vector3(0f, _offsetCameraY, 0f);
        Camera.Yaw = Yaw;

        AABB = new AABB(Position, _size);
    }
}