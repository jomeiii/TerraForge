using OpenTK.Mathematics;
using TerraForge.World;

namespace TerraForge.Physics;

public class PhysicsSystem
{
    private const float Gravity = 9.81f;

    public void Update(Player.Player player, World.World world, double deltaTime)
    {
        player.Velocity -= new Vector3(0f, 1f, 0f)
                           * Gravity
                           * (float)deltaTime;

        Vector3 movement = player.Movement + player.Velocity * (float)deltaTime;

        if (!Collision.Check(player, world, movement))
        {
            player.Position += movement;
        }

        foreach (Cube cube in world.Cubes)
        {
            if (Collision.IsStandingOn(player.AABB, cube.AABB))
            {
                player.IsGrounded = true;

                player.Velocity = new Vector3(
                    player.Velocity.X,
                    0,
                    player.Velocity.Z
                );
            }
        }

        player.Update();
    }
}