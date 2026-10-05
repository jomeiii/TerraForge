using TerraForge.Inventory.Items;
using TerraForge.World;

public class ItemRegistry
{
    public BlockItem Grass { get; }
    public BlockItem Dirt { get; }
    public BlockItem Cobblestone { get; }

    public ItemRegistry()
    {
        Grass = new BlockItem(
            "grass",
            "Grass",
            BlockType.Grass
        );

        Dirt = new BlockItem(
            "dirt",
            "Dirt",
            BlockType.Dirt
        );

        Cobblestone = new BlockItem(
            "cobblestone",
            "Cobblestone",
            BlockType.Cobblestone
        );
    }
}