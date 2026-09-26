namespace TerraForge.World.Blocks.Behaviors;

public class BlockBehaviorRegistry
{
    private readonly Dictionary<BlockType, BlockBehavior> _behaviors = new();

    public BlockBehaviorRegistry()
    {
        Register(BlockType.Grass, new GrassBehavior());
    }

    public BlockBehavior Get(BlockType type)
    {
        return _behaviors.TryGetValue(type, out BlockBehavior? behavior)
            ? behavior
            : EmptyBlockBehavior.Instance;
    }

    private void Register(BlockType type, BlockBehavior behavior)
    {
        _behaviors[type] = behavior;
    }
}