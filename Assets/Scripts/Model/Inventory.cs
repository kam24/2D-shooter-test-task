using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Inventory : IDisposable
{
    [field: NonSerialized] public event Action CellChanged;

    public IReadOnlyList<InventoryCell> Cells => _cells;

    private List<InventoryCell> _cells;
    private uint _bulletsId;

    public Inventory(uint bulletsId)
    {
        _cells = new List<InventoryCell>();
        _bulletsId = bulletsId;
    }

    public Inventory(uint bulletsId, List<InventoryCell> cells)
    {
        _cells = cells;
        _cells.ForEach(cell => cell.Depleted += RemoveItem);
        _bulletsId = bulletsId;
    }

    public void AddItem(InventoryItem item, int count)
    {
        var newCell = new InventoryCell(item, count);

        InventoryCell cell = _cells.FirstOrDefault(cell => cell.Item.Id == item.Id);

        if (cell == null)
            _cells.Add(newCell);
        else
            cell.Merge(newCell);

        newCell.Depleted += RemoveItem;

        CellChanged?.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        int itemIndex = _cells.FindIndex(cell => cell.Item.Id == item.Id);

        if (itemIndex != -1)
            _cells.RemoveAt(itemIndex);
        else
            throw new ArgumentOutOfRangeException(nameof(item.Id));

        CellChanged?.Invoke();
    }

    public void RemoveItem(uint id, int count)
    {
        int itemIndex = _cells.FindIndex(cell => cell.Item.Id == id);

        if (itemIndex != -1)
            _cells[itemIndex].Remove(count);
        else
            throw new ArgumentOutOfRangeException(nameof(id));

        CellChanged?.Invoke();
    }

    public int GetBulletsCount()
    {
        InventoryCell cell = _cells.FirstOrDefault(cell => cell.Item.Id == _bulletsId);
        return cell != null ? cell.Count : 0;
    }

    public void RemoveBullet()
    {
        RemoveItem(_bulletsId, 1);
    }

    public void Dispose()
    {
        _cells.ForEach(cell => cell.Depleted -= RemoveItem);
    }
}

