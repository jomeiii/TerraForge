namespace TerraForge.World.Chunk;

public class Block
{
    public BlockType Type { get; set; }

    public Block(BlockType type)
    {
        Type = type;
    }
}