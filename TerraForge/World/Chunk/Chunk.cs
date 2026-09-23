using OpenTK.Graphics.OpenGL4;

namespace TerraForge.World.Chunk;

public class Chunk
{
    public const int Size = 16;

    public Block[,,] Blocks { get; }
    public ChunkMesh Mesh { get; private set; }

    public int Vao { get; }
    public int Vbo { get; }
    public int Ebo { get; }

    public Chunk()
    {
        Blocks = new Block[Size, Size, Size];

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int z = 0; z < Size; z++)
                {
                    Blocks[x, y, z] = new Block(BlockType.Grass);
                }
            }
        }

        Mesh = ChunkMeshCreator.CreateChunkMesh(this);

        Vao = GL.GenVertexArray();
        Vbo = GL.GenBuffer();
        Ebo = GL.GenBuffer();

        GL.BindVertexArray(Vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            Mesh.Vertices.Length * sizeof(float),
            Mesh.Vertices,
            BufferUsageHint.StaticDraw
        );

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Ebo);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            Mesh.Indices.Length * sizeof(uint),
            Mesh.Indices,
            BufferUsageHint.StaticDraw
        );

        GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            0
        );
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            3 * sizeof(float)
        );
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
    }
}