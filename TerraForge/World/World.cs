using OpenTK.Mathematics;
using TerraForge.World.Blocks.Behaviors;
using TerraForge.World.Chunk;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World;

public class World
{
    public List<Chunk.Chunk> Chunks { get; }
    private readonly BlockBehaviorRegistry _blockBehaviorRegistry;
    private readonly HashSet<Chunk.Chunk> _dirtyChunks = new();
    private readonly AtlasConfig _atlasConfig;

    public World(AtlasConfig atlas)
    {
        _blockBehaviorRegistry = new BlockBehaviorRegistry();
        
        _atlasConfig = atlas;
        Chunks = new List<Chunk.Chunk>();
        Chunks.Add(new Chunk.Chunk());
        Chunks[0].Generate(this, atlas);
    }

    public void Update()
    {
        foreach (Chunk.Chunk chunk in _dirtyChunks)
        {
            ChunkMeshCreator.CreateChunkMesh(chunk, _atlasConfig);
        }

        _dirtyChunks.Clear();
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

        Console.WriteLine(
            $"BEFORE: ({x},{y},{z}) = {block.Type}");

        block.Type = type;

        Console.WriteLine(
            $"AFTER: ({x},{y},{z}) = {Chunks[0].Blocks[x, y, z].Type}");

        MarkDirty(Chunks[0]);

        Vector3i position = new(x, y, z);

        BlockBehavior? behavior = _blockBehaviorRegistry.Get(type);
        behavior?.OnPlaced(this, position);
    }
    
    public void MarkDirty(Chunk.Chunk chunk)
    {
        chunk.IsDirty = true;
        _dirtyChunks.Add(chunk);
    }
    
    public void SpawnBlock(Vector3i position, BlockType type)
    {
        if (position.X < 0 || position.X >= Chunk.Chunk.Size ||
            position.Y < 0 || position.Y >= Chunk.Chunk.Size ||
            position.Z < 0 || position.Z >= Chunk.Chunk.Size)
        {
            return;
        }

        Chunks[0].Blocks[position.X, position.Y, position.Z] = new Block(type);

        BlockBehavior? behavior = _blockBehaviorRegistry.Get(type);
        behavior?.OnSpawn(this, position);
    }
}