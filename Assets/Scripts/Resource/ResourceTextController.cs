using Assets.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Resource
{
    public class ResourceTextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private EItemName _item;
        
        private void Start()
        {
            Inventory.Inventory.Instance.OnChanged += SetText;
        }

        private void SetText(int text)
        {
            _text.text = Inventory.Inventory.Instance.Count(_item).ToString();
        }

        private void OnDestroy()
        {
            Inventory.Inventory.Instance.OnChanged -= SetText;
        }
    }
}
