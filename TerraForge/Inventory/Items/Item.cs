namespace TerraForge.Inventory.Items;

public abstract class Item
{
    public string Id { get; }
    public string Name { get; }
    public int MaxStackSize { get; }

    protected Item(string id, string name, int maxStackSize)
    {
        Id = id;
        Name = name;
        MaxStackSize = maxStackSize;
    }
}