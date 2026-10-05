using TerraForge.World;

namespace TerraForge.Inventory.Items;

public class BlockItem : Item
{
    public BlockType BlockType { get; }

    public BlockItem(string id, string name, BlockType blockType, int maxStackSize = 500)
        : base(id, name, maxStackSize)
    {
        BlockType = blockType;
    }
}