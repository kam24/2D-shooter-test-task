using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public uint Id { get; }
    public string Name { get; }

    public InventoryItem(uint id, string name)
    {
        Id = id;
        Name = name;
    }
}
