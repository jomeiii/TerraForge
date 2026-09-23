using OpenTK.Graphics.OpenGL4;
using TerraForge.World.Chunk;

namespace TerraForge.World.Chunk;

public class ChunkRenderer
{
    public void Draw(Chunk chunk)
    {
        GL.BindVertexArray(chunk.Vao);

        GL.DrawElements(
            PrimitiveType.Triangles,
            chunk.Mesh.Indices.Length,
            DrawElementsType.UnsignedInt,
            0
        );

        GL.BindVertexArray(0);
    }
}