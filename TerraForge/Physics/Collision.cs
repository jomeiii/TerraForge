using OpenTK.Mathematics;
using TerraForge.World;

namespace TerraForge.Physics;

public static class Collision
{
    public static bool Check(AABB a, AABB b)
    {
        return a.Intersects(b);
    }

    public static bool Check(Player.Player player, World.World world, Vector3 movement)
    {
        AABB newAABB = new AABB(player.Position +  movement, player.Size);
        foreach (var cube in world.Cubes)
        {
            if (Check(newAABB, cube.AABB))
            {
                return true;
            }
        }
        
        return false;
    }
    
    public static bool Check(Player.Player player, World.World world)
    {
        foreach (Cube cube in world.Cubes)
        {
            if (Check(player.AABB, cube.AABB))
                return true;
        }

        return false;
    }
}