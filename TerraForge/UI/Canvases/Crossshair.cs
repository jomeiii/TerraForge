using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;

namespace TerraForge.UI.Canvases;

public class Crossshair : Canvas
{
    private readonly float[] _vertices;
    private readonly Vector2i _rectangleSize;

    public Crossshair(Shader shader, Vector2i pivot, Vector2i offset, Vector2i rectangleSize) : base(shader, pivot,
        offset)
    {
        GL.BindVertexArray(_vertexArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        _rectangleSize = rectangleSize;
        Vector2i canvasCenter = pivot + offset;

        _vertices =
        [
            // first
            canvasCenter.X - _rectangleSize.X / 2f, canvasCenter.Y - _rectangleSize.Y / 2f,
            canvasCenter.X - _rectangleSize.X / 2f, canvasCenter.Y + _rectangleSize.Y / 2f,
            canvasCenter.X + _rectangleSize.X / 2f, canvasCenter.Y + _rectangleSize.Y / 2f,

            canvasCenter.X + _rectangleSize.X / 2f, canvasCenter.Y + _rectangleSize.Y / 2f,
            canvasCenter.X + _rectangleSize.X / 2f, canvasCenter.Y - _rectangleSize.Y / 2f,
            canvasCenter.X - _rectangleSize.X / 2f, canvasCenter.Y - _rectangleSize.Y / 2f,
            
            // second
            canvasCenter.X - _rectangleSize.Y / 2f, canvasCenter.Y - _rectangleSize.X / 2f,
            canvasCenter.X - _rectangleSize.Y / 2f, canvasCenter.Y + _rectangleSize.X / 2f,
            canvasCenter.X + _rectangleSize.Y / 2f, canvasCenter.Y + _rectangleSize.X / 2f,

            canvasCenter.X + _rectangleSize.Y / 2f, canvasCenter.Y + _rectangleSize.X / 2f,
            canvasCenter.X + _rectangleSize.Y / 2f, canvasCenter.Y - _rectangleSize.X / 2f,
            canvasCenter.X - _rectangleSize.Y / 2f, canvasCenter.Y - _rectangleSize.X / 2f
        ];

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw
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

    public override void Render()
    {
        _shader.Use();
        GL.BindVertexArray(_vertexArray);

        _shader.SetBool("useSolidColor", true);
        _shader.SetBool("useTexture", false);
        _shader.SetColor("textColor", Color.White);
        _shader.SetFloat("solidColorAlpha", 1f);
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 2);
    }

    public override void Dispose()
    {
        GL.DeleteVertexArray(_vertexArray);
        GL.DeleteBuffer(_vertexBuffer);
    }
}