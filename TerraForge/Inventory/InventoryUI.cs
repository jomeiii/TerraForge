using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TerraForge.Graphics;
using TerraForge.Inventory.Items;
using TerraForge.UI;
using TerraForge.World.Textures.Atlas;
using TerraForge.World.Textures.TextureFace;

namespace TerraForge.Inventory;

public class InventoryUI : Canvas
{
    private readonly Vector2i _itemSize;
    private readonly Vector2i _spacing;

    private readonly TextureAtlas _textureAtlas;
    private readonly Inventory _inventory;

    private readonly UI.UI _ui;

    private List<float> _vertices = null!;

    public InventoryUI(Inventory inventory, Shader shader, Vector2i pivot, Vector2i itemSize, Vector2i offset,
        Vector2i spacing, TextureAtlas textureAtlas, UI.UI ui) : base(shader, pivot, offset)
    {
        _inventory = inventory;
        _itemSize = itemSize;
        _spacing = spacing;

        _textureAtlas = textureAtlas;

        _ui = ui;

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
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f, 0f, 0f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f, 0f, 1f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f, 1f, 1f,

            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y + _canvasSize.Y / 2f, 1f, 1f,
            canvasCenter.X + _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f, 1f, 0f,
            canvasCenter.X - _canvasSize.X / 2f, canvasCenter.Y - _canvasSize.Y / 2f, 0f, 0f
        ];


        Vector2i itemCenter = new Vector2i(_pivot.X + _offset.X, _pivot.Y + _offset.Y - _canvasSize.Y / 2);
        itemCenter.X += -_spacing.X / 2 - _itemSize.X - _spacing.X - _itemSize.X / 2;
        for (int i = 0; i < inventory.SlotsCount; i++)
        {
            var stack = inventory.GetSlot(i).Stack;
            if (stack is { Count: > 0, Item: BlockItem blockItem })
            {
                var faceTextureConfig = _textureAtlas.Config.Blocks[blockItem.BlockType].GetFace(BlockFace.Front);
                float uMin = 0f, uMax = 0f, vMin = 0f, vMax = 0f;
                if (faceTextureConfig != null && stack.Count > 0)
                {
                    var baseUv = faceTextureConfig.GetBaseUV(_textureAtlas.Config).Value;

                    uMin = baseUv.X;
                    vMin = baseUv.Y;
                    uMax = baseUv.Z;
                    vMax = baseUv.W;
                }

                _vertices.AddRange([
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, uMin, vMax,
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, uMin, vMin,
                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, uMax, vMin,

                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, uMax, vMin,
                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, uMax, vMax,
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, uMin, vMax
                ]);
            }
            else
            {
                _vertices.AddRange([
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, 0f, 1f,
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, 0f, 0f,
                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, 1f, 0f,

                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y + _itemSize.Y / 2f, 1f, 0f,
                    itemCenter.X + _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, 1f, 1f,
                    itemCenter.X - _itemSize.X / 2f, itemCenter.Y - _itemSize.Y / 2f, 0f, 1f
                ]);
            }

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

    public override void Render()
    {
        _shader.Use();
        GL.BindVertexArray(_vertexArray);

        // background
        _shader.SetBool("useSolidColor", true);
        _shader.SetBool("useTexture", false);
        _shader.SetColor("textColor", Color.White);
        _shader.SetFloat("solidColorAlpha", 1f);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // items
        _shader.SetBool("useTexture", true);
        _shader.SetBool("useSolidColor", false);
        _textureAtlas.Texture.Use();
        GL.DrawArrays(PrimitiveType.Triangles, 6, _inventory.ActiveSlotCount * 6);

        // empty slots
        _shader.SetBool("useSolidColor", true);
        _shader.SetBool("useTexture", false);
        _shader.SetColor("textColor", Color.Gray);
        _shader.SetFloat("solidColorAlpha", 0.33f);
        GL.DrawArrays(
            PrimitiveType.Triangles,
            6 + _inventory.ActiveSlotCount * 6,
            _vertices.Count / 4 - 6 - _inventory.ActiveSlotCount * 6
        );

        // count of items
        var font = _ui.Font;
        Vector2i textPosition = new Vector2i(_pivot.X + _offset.X - _itemSize.X / 2,
            _pivot.Y + _offset.Y - _spacing.Y - (int)font.LineHeight);
        textPosition.X += -_spacing.X / 2 - _itemSize.X - _spacing.X - _itemSize.X / 2;
        for (int i = 0; i < _inventory.ActiveSlotCount; i++)
        {
            _ui.DrawText(_inventory.GetSlot(i).Stack.Count.ToString(), textPosition.X, textPosition.Y, 0, Color.Red);
            textPosition.X += _itemSize.X + _spacing.X;
        }
    }
}