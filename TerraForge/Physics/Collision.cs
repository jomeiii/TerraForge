using OpenTK.Mathematics;
using TerraForge.World;
using TerraForge.World.Chunk;

namespace TerraForge.Physics;

public static class Collision
{
    public static bool Check(AABB a, AABB b)
    {
        return a.Intersects(b);
    }

    public static bool Check(Player.Player player, World.World world, Vector3 movement)
    {
        AABB newAABB = new AABB(
            player.Position + movement,
            player.Size
        );

        int minX = (int)MathF.Floor(newAABB.Min.X + 0.5f);
        int maxX = (int)MathF.Floor(newAABB.Max.X + 0.5f);

        int minY = (int)MathF.Floor(newAABB.Min.Y);
        int maxY = (int)MathF.Floor(newAABB.Max.Y);

        int minZ = (int)MathF.Floor(newAABB.Min.Z + 0.5f);
        int maxZ = (int)MathF.Floor(newAABB.Max.Z + 0.5f);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    Block block = world.GetBlock(x, y, z);

                    if (block.Type == BlockType.Air)
                        continue;

                    AABB blockAABB = new AABB(
                        new Vector3(x, y, z),
                        Vector3.One
                    );

                    if (Check(newAABB, blockAABB))
                        return true;
                }
            }
        }

        return false;
    }

    public static bool IsStandingOn(AABB player, AABB block)
    {
        bool xOverlap =
            player.Max.X > block.Min.X &&
            player.Min.X < block.Max.X;

        bool zOverlap =
            player.Max.Z > block.Min.Z &&
            player.Min.Z < block.Max.Z;

        bool onTop =
            MathF.Abs(player.Min.Y - block.Max.Y) < 0.01f;

        return xOverlap && zOverlap && onTop;
    }
}