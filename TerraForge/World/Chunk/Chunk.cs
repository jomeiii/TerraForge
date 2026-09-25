using OpenTK.Graphics.OpenGL4;
using TerraForge.World.Textures.Atlas;

namespace TerraForge.World.Chunk;

public class Chunk
{
    public const int Stride = 16 * sizeof(float);
    public const int Size = 16;

    public Block[,,] Blocks { get; }
    public ChunkMesh Mesh { get; private set; }

    public int Vao { get; }
    public int Vbo { get; }
    public int Ebo { get; }

    public Chunk(AtlasConfig atlas)
    {
        Blocks = new Block[Size, Size, Size];

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int z = 0; z < Size; z++)
                {
                    if (x > 4 && x < Size - 4 &&
                        y > 6  && 
                        z > 4 && z < Size - 4)
                    {
                        Blocks[x, y, z] = new Block(BlockType.Air);
                    }
                    else if (x > 4 && x < Size - 4 &&
                             y == 6 &&
                             z > 4 && z < Size - 4)
                    {
                        Blocks[x, y, z] = new Block(BlockType.Cobblestone);
                    }
                    else
                    {
                        Blocks[x, y, z] = new Block(BlockType.Grass);
                    }
                }
            }
        }

        Mesh = ChunkMeshCreator.CreateChunkMesh(this, atlas);

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

        // pos
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Stride, 0);
        GL.EnableVertexAttribArray(0);

        // local uv
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Stride, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // base uv
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Stride, 5 * sizeof(float));
        GL.EnableVertexAttribArray(2);

        // overlay uv
        GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, Stride, 9 * sizeof(float));
        GL.EnableVertexAttribArray(3);

        // color vector3
        GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, Stride, 13 * sizeof(float));
        GL.EnableVertexAttribArray(4);
    }
}