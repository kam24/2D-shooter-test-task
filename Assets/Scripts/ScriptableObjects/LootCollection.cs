using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot Collection")]
public class LootCollection : ScriptableObject
{
    [SerializeField] private uint _bulletsId;
    [SerializeField] private List<Loot> _loots;

    public IReadOnlyCollection<Loot> Loots => _loots;
    public uint BulletsId => _bulletsId;
}
