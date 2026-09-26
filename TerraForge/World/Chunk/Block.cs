namespace TerraForge.World.Chunk;

public struct Block
{
    public BlockType Type;

    public Block(BlockType type)
    {
        Type = type;
    }
}