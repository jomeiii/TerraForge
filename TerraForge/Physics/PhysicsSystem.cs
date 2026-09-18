using OpenTK.Mathematics;

namespace TerraForge.Physics;

public class PhysicsSystem
{
    public void Update(Player.Player player, World.World world, double deltaTime)
    {
        float dt = (float)deltaTime;

        // Gravity
        player.Velocity -= new Vector3(0, PhysicsSettings.Gravity * dt, 0);

        Vector3 xMovement = new Vector3(player.Movement.X, 0, 0);
        Vector3 yMovement = new Vector3(0, player.Velocity.Y * dt, 0);
        Vector3 zMovement = new Vector3(0, 0, player.Movement.Z);

        if (!Collision.Check(player, world, xMovement))
        {
            player.Position += xMovement;
        }
        
        if (!Collision.Check(player, world, yMovement))
        {
            player.Position += yMovement;
        }
        else if (player.Velocity.Y < 0)
        {
            player.IsGrounded = true;

            player.Velocity = new Vector3(
                player.Velocity.X,
                0,
                player.Velocity.Z
            );
        }
        
        if (!Collision.Check(player, world, zMovement))
        {
            player.Position += zMovement;
        }

        player.Update();
    }
}