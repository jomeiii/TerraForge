using OpenTK.Graphics.OpenGL4;

namespace TerraForge.Graphics;

public class Mesh : IDisposable
{
    private readonly int _vertexArray;
    private readonly int _vertexBuffer;

    public int VertexCount { get; }

    public Mesh(float[] vertices)
    {
        VertexCount = vertices.Length / 5;

        _vertexArray = GL.GenVertexArray();
        _vertexBuffer = GL.GenBuffer();

        GL.BindVertexArray(_vertexArray);

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
    }

    public void Bind()
    {
        GL.BindVertexArray(_vertexArray);
    }

    public void Draw()
    {
        Bind();

        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            VertexCount
        );
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);
    }
}