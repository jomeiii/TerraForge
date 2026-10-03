using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;
using TerraForge.UI;

namespace TerraForge.Inventory;

public class InventoryUI : Canvas
{
    private readonly Vector2i _itemSize;
    private readonly Vector2i _spacing;

    private List<float> _vertices = null!;

    public InventoryUI(Inventory inventory, Shader shader, Vector2i pivot, Vector2i itemSize, Vector2i offset,
        Vector2i spacing) : base(shader, pivot, offset)
    {
        _itemSize = itemSize;
        _spacing = spacing;

        _canvasSize = new Vector2i(_itemSize.X * inventory.SlotsCount + (inventory.SlotsCount + 1) * _spacing.X,
            _itemSize.Y + 2 * _spacing.Y);

        CreateVertexArrays(inventory);
    }

    private void CreateVertexArrays(Inventory inventory)
    {
        GL.BindVertexArray(_vertexArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);

        Vector2i canvasCenter = new Vector2i(_pivot.X + _offset.X, _pivot.Y + _offset.Y - _canvasSize.Y / 2);
        _vertices = new List<float>();
        _vertices =
        [
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,

            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f
        ];


        Vector2i itemCenter = new Vector2i(_pivot.X + _offset.X, _pivot.Y + _offset.Y - _canvasSize.Y / 2);
        itemCenter.X += -_spacing.X / 2 - _itemSize.X - _spacing.X - _itemSize.X / 2;
        for (int i = 0; i < inventory.SlotsCount; i++)
        {
            _vertices.AddRange([
                itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f,
                itemCenter.X - _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f,
                itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f,

                itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f,
                itemCenter.X + _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f,
                itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f
            ]);

            itemCenter.X += _itemSize.X + _spacing.X;
        }

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Count * sizeof(float),
            _vertices.ToArray(),
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
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count / 2);
    }

    public override void Dispose()
    {
        GL.DeleteVertexArray(_vertexArray);
        GL.DeleteBuffer(_vertexBuffer);
    }
}