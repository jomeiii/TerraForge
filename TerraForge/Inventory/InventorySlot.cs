using TerraForge.Inventory.Items;

namespace TerraForge.Inventory;

public class InventorySlot
{
    public ItemStack? Stack { get; private set; }

    public bool IsEmpty => Stack == null;

    public bool CanAdd(Item item)
    {
        if (Stack == null)
            return false;
        
        return Stack.Count < item.MaxStackSize;
    }

    public int Add(Item item, int count)
    {
        if (Stack == null)
        {
            int added = Math.Min(count, item.MaxStackSize);

            Stack = new ItemStack(item, added);

            return added;
        }

        if (item == Stack.Item)
        {
            return Stack.Add(count);
        }

        return 0;
    }

    public int Remove(int count)
    {
        if (Stack == null)
            return 0;

        int removed = Stack.Remove(count);

        if (Stack.IsEmpty)
            Stack = null;

        return removed;
    }
}