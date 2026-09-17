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
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        GL.VertexAttribPointer(
            0,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            0
        );
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            2 * sizeof(float)
        );
        GL.EnableVertexAttribArray(1);
    }

    public void Draw(string text, float x, float y, Font font)
    {
        List<float> _vertices = new();
        float cursorX = x;

        foreach (char c in text)
        {
            if (!font.TryGetGlyph(c, out Glyph glyph))
                throw new Exception($"Glyph '{c}' not found.");

            float quadX = cursorX + glyph.Bearing.X;
            float quadY = y - glyph.Bearing.Y;

            _vertices.AddRange(new[]
            {
                // position uv
                quadX, quadY, glyph.UVMin.X, glyph.UVMax.Y,
                quadX, quadY + glyph.Size.Y, glyph.UVMin.X, glyph.UVMin.Y,
                quadX + glyph.Size.X, quadY, glyph.UVMax.X, glyph.UVMax.Y,

                quadX, quadY + glyph.Size.Y, glyph.UVMin.X, glyph.UVMin.Y,
                quadX + glyph.Size.X, quadY, glyph.UVMax.X, glyph.UVMax.Y,
                quadX + glyph.Size.X, quadY + glyph.Size.Y, glyph.UVMax.X, glyph.UVMin.Y
            });

            cursorX += glyph.Advance;
        }
        
        GL.BindVertexArray(_vertexArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Count * sizeof(float),
            _vertices.ToArray(),
            BufferUsageHint.DynamicDraw
        );
        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            _vertices.Count / 4
        );
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteVertexArray(_vertexArray);
    }
}