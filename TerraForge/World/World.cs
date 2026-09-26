using OpenTK.Mathematics;
using TerraForge.World.Blocks.Behaviors;
using TerraForge.World.Chunk;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World;

public class World
{
    public List<Chunk.Chunk> Chunks { get; }
    private readonly BlockBehaviorRegistry _blockBehaviorRegistry;

    public World(AtlasConfig atlas)
    {
        Chunks = new List<Chunk.Chunk>();
        Chunks.Add(new Chunk.Chunk(atlas));
        
        _blockBehaviorRegistry = new BlockBehaviorRegistry();
    }
    
    public Block GetBlock(int x, int y, int z)
    {
        if (x < 0 || x >= Chunk.Chunk.Size ||
            y < 0 || y >= Chunk.Chunk.Size ||
            z < 0 || z >= Chunk.Chunk.Size)
        {
            return new Block(BlockType.Air);
        }

        return Chunks[0].Blocks[x, y, z];
    }

    public void SetBlock(int x, int y, int z, BlockType type)
    {
        if (x < 0 || x >= Chunk.Chunk.Size ||
            y < 0 || y >= Chunk.Chunk.Size ||
            z < 0 || z >= Chunk.Chunk.Size)
        {
            return;
        }

        Block block = Chunks[0].Blocks[x, y, z];

        block.Type = type;

        Vector3i position = new(x, y, z);

        BlockBehavior? behavior = _blockBehaviorRegistry.Get(type);
        behavior?.OnPlaced(this, position);
    }
}