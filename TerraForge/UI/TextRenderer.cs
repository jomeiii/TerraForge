using OpenTK.Graphics.OpenGL4;
using TerraForge.Graphics;

namespace TerraForge.UI;

public class TextRenderer : IDisposable
{
    private readonly int _vertexArray;
    private readonly int _vertexBuffer;

    public TextRenderer()
    {
        _vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArray);

        _vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(
            BufferTarget.ArrayBuffer,
            _vertexBuffer
        );

        GL.VertexAttribPointer(
            0,
            2,
            VertexAttribPointerType.Float,
            false,
            2 * sizeof(float),
            0
        );

        GL.EnableVertexAttribArray(0);
    }

    public void Draw(
        string text,
        float x,
        float y
    )
    {
        List<float> vertices = new();

        float cursorX = x;

        foreach (char character in text)
        {
            float width = 100f;
            float height = 100f;

            vertices.AddRange(new float[]
            {
                cursorX,          y,
                cursorX + width,  y,
                cursorX + width,  y + height,

                cursorX + width,  y + height,
                cursorX,          y + height,
                cursorX,          y
            });

            cursorX += width;
        }

        GL.BindVertexArray(_vertexArray);

        GL.BindBuffer(
            BufferTarget.ArrayBuffer,
            _vertexBuffer
        );

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            vertices.Count * sizeof(float),
            vertices.ToArray(),
            BufferUsageHint.DynamicDraw
        );

        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            vertices.Count / 2
        );
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);
    }
}