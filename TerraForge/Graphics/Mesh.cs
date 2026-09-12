using OpenTK.Graphics.OpenGL4;

namespace TerraForge.Graphics;

public class Mesh : IDisposable
{
    private readonly int _vertexArray;
    private readonly int _vertexBuffer;
    private readonly int _elementBuffer;

    public int IndexCount { get; }

    public Mesh(float[] vertices, uint[] indices)
    {
        IndexCount = indices.Length;

        _vertexArray = GL.GenVertexArray();
        _vertexBuffer = GL.GenBuffer();
        _elementBuffer = GL.GenBuffer();

        GL.BindVertexArray(_vertexArray);

        // VBO
        GL.BindBuffer(
            BufferTarget.ArrayBuffer,
            _vertexBuffer
        );

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            vertices.Length * sizeof(float),
            vertices,
            BufferUsageHint.StaticDraw
        );

        // EBO
        GL.BindBuffer(
            BufferTarget.ElementArrayBuffer,
            _elementBuffer
        );

        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            indices.Length * sizeof(uint),
            indices,
            BufferUsageHint.StaticDraw
        );

        // Position
        GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            0
        );

        GL.EnableVertexAttribArray(0);

        // Texture coordinates
        GL.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            3 * sizeof(float)
        );
        
        GL.EnableVertexAttribArray(1);
    }

    public void Draw(int indexCount, int indexOffset)
    {
        GL.BindVertexArray(_vertexArray);

        GL.DrawElements(
            PrimitiveType.Triangles,
            indexCount,
            DrawElementsType.UnsignedInt,
            indexOffset * sizeof(uint)
        );
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_elementBuffer);
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);
    }
}