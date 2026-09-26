using OpenTK.Mathematics;

namespace TerraForge.World.Blocks.Behaviors;

public abstract class BlockBehavior
{
    public virtual void OnPlaced(World world, Vector3i position)
    {
    }

    public virtual void OnSpawn(World world, Vector3i position)
    {
    }
    
    public virtual void OnBroken(World world, Vector3i position)
    {
    }

    public virtual void OnNeighborChanged(World world, Vector3i position, Vector3i neighborPosition)
    {
    }
}