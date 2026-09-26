using TerraForge.World.Chunk;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World;

public class World
{
    public List<Chunk.Chunk> Chunks { get; }

    public World(AtlasConfig atlas)
    {
        Chunks = new List<Chunk.Chunk>();
        Chunks.Add(new Chunk.Chunk(atlas));
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
}