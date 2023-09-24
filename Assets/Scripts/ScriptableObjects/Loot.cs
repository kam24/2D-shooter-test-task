using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName ="Inventory Item")]
    public class Loot : ScriptableObject
    {
        [SerializeField] private uint _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;

        public uint Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
    }
}
