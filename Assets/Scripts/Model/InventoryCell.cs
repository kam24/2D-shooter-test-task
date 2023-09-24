using System;

[Serializable]
public class InventoryCell
{
    [field: NonSerialized] public event Action<InventoryItem> Depleted;

    public InventoryItem Item { get; private set; }
    public int Count { get; private set; }

    public InventoryCell(InventoryItem item, int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count));

        Item = item;
        Count = count;
    }

    public void Merge(InventoryCell cell)
    {
        if (cell.Item.Id != Item.Id)
            throw new InvalidOperationException();

        Count += cell.Count;
    }

    public void Remove(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count -= count;
        if (Count == 0)
            Depleted?.Invoke(Item);
    }
}

