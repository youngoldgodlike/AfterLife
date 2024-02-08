using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Inventory
{
    public class Inventory : Singleton<Inventory>
    {
        [SerializeField] private List<InventoryItem> _items = new List<InventoryItem>();

        public Action<int> OnChanged;
        
        public void Add(EItemName id, int count)
        {
            foreach (var item in _items)
            {
                if (item.Name != id) continue;
                
                item.Count += count;
                OnChanged?.Invoke(Count(id));
                
                return;
            }
        }

        public void Remove(EItemName id, int count)
        {
            foreach (var item in _items)
            {
                if (item.Name != id) continue;
                
                item.Count -= count;
                OnChanged?.Invoke(Count(id));
                
                return;
            }
        }

        public int Count(EItemName id)
        {
            foreach (var item in _items)
            {
                if (item.Name == id)
                    return item.Count;
            }
            return 0;
        }

        public bool Check(EItemName id, int count)
        {
            foreach (var item in _items)
            {
                if (id != item.Name) continue;

                if (item.Count >= count)
                    return true;
                else
                    return false;
            }

            return false;
        }
    }

    [Serializable]
    public class InventoryItem
    {
        [SerializeField] private EItemName _name;
        [SerializeField] private EItemAttribute _attribute;
        [SerializeField] private int _maxSize;

        public int Count;
        public int MaxSize => _maxSize;
        public EItemAttribute Attribute => _attribute;
        public EItemName Name => _name;
    }

    public enum EItemName
    {
        RedDiamond,
        PurpleDiamond,
        GreenDiamond,
        Stone,
        Wood,
        Key
    }

    public enum EItemAttribute
    {
        Rotating,
        Taking,
        Required
    }
    
}