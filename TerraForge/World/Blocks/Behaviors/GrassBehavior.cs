using OpenTK.Mathematics;

namespace TerraForge.World.Blocks.Behaviors;

public class GrassBehavior : BlockBehavior
{
    public override void OnPlaced(World world, Vector3i position)
    {
        Vector3i below = position + Vector3i.UnitY * -1;

        world.SetBlock(below.X, below.Y, below.Z, BlockType.Dirt);
    }
}