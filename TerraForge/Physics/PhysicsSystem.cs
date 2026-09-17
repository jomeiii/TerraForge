namespace TerraForge.Physics;

public class PhysicsSystem
{
    private const float Gravity = 9.81f;
    
    public void Update(Player.Player player, World.World world, double deltaTime)
    {
        if (!Collision.Check(player, world, player.Movement))
        {
            player.Position += player.Movement;
        }

        player.Update();
    }
}