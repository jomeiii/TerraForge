namespace TerraForge.World.Chunk;

public class ChunkMesh
{
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    public ChunkMesh(float[] vertices, uint[] indices)
    {
        Vertices = vertices;
        Indices = indices;
    }
}