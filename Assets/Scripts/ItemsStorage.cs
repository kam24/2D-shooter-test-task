using Assets.Scripts;
using System;
using System.Linq;
using UnityEngine;

public class ItemsStorage  
{
    public uint BulletsId => _lootCollection.BulletsId;

    private LootCollection _lootCollection;

    public ItemsStorage(LootCollection lootCollection)
    {
        _lootCollection = lootCollection;
    }

    public Loot GetLoot(uint id)
    {
        Loot loot = _lootCollection.Loots.FirstOrDefault(loot => loot.Id == id);
        if (loot != null)
            return loot;
        else
            throw new ArgumentOutOfRangeException(nameof(id));
    }

    public Loot GetLoot(InventoryItem item)
    {
        return GetLoot(item.Id);
    }

}