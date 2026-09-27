using TerraForge.Inventory.Items;

public class ItemStack
{
    public Item Item { get; }
    public int Count { get; private set; }

    public ItemStack(Item item, int count)
    {
        Item = item;
        Count = count;
    }
    
    public int Add(int count)
    {
        int space = Item.MaxStackSize - Count;
        int added = Math.Min(space, count);

        Count += added;

        return added;
    }

    public int Remove(int count)
    {
        int removed = Math.Min(Count, count);

        Count -= removed;

        return removed;
    }

    public bool IsEmpty => Count <= 0;
}