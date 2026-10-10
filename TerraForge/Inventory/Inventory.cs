using TerraForge.Inventory.Items;

namespace TerraForge.Inventory;

public class Inventory
{
    public const int SlotCount = 4;

    private readonly InventorySlot[] _slots;

    public int SelectedSlot { get; private set; }
    public int ActiveSlotCount { get; set; } = 0;

    public int SlotsCount => _slots.Length;

    public Inventory()
    {
        _slots = new InventorySlot[SlotCount];

        for (int i = 0; i < SlotCount; i++)
        {
            _slots[i] = new InventorySlot();
        }
    }

    public InventorySlot GetSlot(int index)
    {
        return _slots[index];
    }

    public bool Add(Item item, int count = 1)
    {
        int remaining = count;

        foreach (InventorySlot slot in _slots)
        {
            if (!slot.IsEmpty && slot.CanAdd(item))
            {
                int added = slot.Add(item, remaining);
                remaining -= added;

                if (remaining == 0)
                {
                    return true;
                }
            }
        }

        // Если не получилось — используем пустые slots
        foreach (InventorySlot slot in _slots)
        {
            if (slot.IsEmpty)
            {
                int added = slot.Add(item, remaining);
                remaining -= added;

                if (remaining == 0)
                {
                    ActiveSlotCount++;
                    return true;
                }
            }
        }

        return false;
    }
}