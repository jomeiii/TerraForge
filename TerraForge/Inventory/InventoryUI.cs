using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;
using TerraForge.UI;

namespace TerraForge.Inventory;

public class InventoryUI : Canvas
{
    private readonly Vector2i _itemSize;
    private readonly Vector2i _spacing;

    private float[] _vertices = null!;

    public InventoryUI(Shader shader, Vector2i pivot, Vector2i itemSize, Vector2i offset, Vector2i spacing) : base(shader, pivot, offset)
    {
        _itemSize = itemSize;
        CreateVertexArrays();
    }

    private void CreateVertexArrays()
    {
        GL.BindVertexArray(_vertexArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        Vector2i canvasCenter = new Vector2i(_pivot.X + _offset.X, _pivot.Y + _offset.Y - _canvasSize.Y / 2);
        _vertices =
        [
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,

            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f,
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
        _shader.SetBool("useSolidColor", true);
        _shader.SetVector4("textColor", new Vector4(1f, 1f, 1f, 1f));
        GL.BindVertexArray(_vertexArray);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    public override void Dispose()
    {
        GL.DeleteVertexArray(_vertexArray);
        GL.DeleteBuffer(_vertexBuffer);
    }
}