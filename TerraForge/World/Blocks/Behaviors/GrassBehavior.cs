using OpenTK.Mathematics;
using TerraForge.World.Chunk;

namespace TerraForge.World.Blocks.Behaviors;

public class GrassBehavior : BlockBehavior
{
    public override void OnSpawn(World world, Vector3i position)
    {
        MakeBlockBelowDirt(world, position);
    }

    public override void OnPlaced(World world, Vector3i position)
    {
        MakeBlockBelowDirt(world, position);
    }

    private static void MakeBlockBelowDirt(World world, Vector3i position)
    {
        Vector3i below = position - Vector3i.UnitY;
        if (below.Y < 0)
            return;

        Block blockBelow = world.GetBlock(below.X, below.Y, below.Z);
        if (blockBelow.Type != BlockType.Grass)
            return;

        world.SetBlock(below.X, below.Y, below.Z, BlockType.Dirt);
    }
}