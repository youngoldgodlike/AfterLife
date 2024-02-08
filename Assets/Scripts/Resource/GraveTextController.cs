using Assets.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Resource
{
    public class GraveTextController : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private EItemName _name;
        
        private GraveBehavior _grave;

        private void Awake()
        {
            _grave = GetComponentInParent<GraveBehavior>();
            _grave.OnAddResource += ShowText;

            var item = _grave.GetItem(_name);
            ShowText(item.Name, item.Count, item.MaxSize);
        }
        
        private void ShowText(EItemName id, int count, int maxSize)
        {
            if (_name != id) return;

            _text.text = $"{_name}: {count}/{maxSize}";
        }

        private void OnDestroy()
        {
            _grave.OnAddResource -= ShowText;
        }
    }
}