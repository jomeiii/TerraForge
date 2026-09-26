using OpenTK.Mathematics;

namespace TerraForge.World.Blocks.Behaviors;

public interface IBlockBehavior
{
    void OnPlaced(World world, Vector3i position);
    
    void OnBroken(World world, Vector3i position);

    void OnNeighborChanged(
        World world,
        Vector3i position,
        Vector3i neighborPosition);
}