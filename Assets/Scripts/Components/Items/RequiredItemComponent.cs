using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.Items
{
    public class RequiredItemComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEnough;
        [SerializeField] private UnityEvent _onNotEnough;
        
        [SerializeField] private EItemName _itemName;
        [SerializeField] private int _count;

        public void Check()
        {
            if (Inventory.Inventory.Instance.Check(_itemName, _count))
            {
                Inventory.Inventory.Instance.Remove(_itemName, _count);
                _onEnough?.Invoke();
            }
            else
                _onNotEnough?.Invoke();
        }
    }
}
