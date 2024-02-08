using System;
using Assets.Scripts.Character.Interactable;
using Assets.Scripts.Components;
using Assets.Scripts.Components.Audio;
using Assets.Scripts.Inventory;
using Components.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Resource
{
    public class GraveBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject _repairGrave;
        [SerializeField] private GameObject _graveCanvas;
        
        [SerializeField] private InventoryItem[] _needItems;
        [SerializeField] private UnityEvent _onRepairGrave;

        public InventoryItem[] NeedItems => _needItems;
        
        public Action<EItemName, int, int> OnAddResource;
        
        private CharacterTakeSystem _character;
        private PlaySoundsComponent _audio;
        

        private void Awake()
        {
            _character = FindObjectOfType<CharacterTakeSystem>();
            _audio = GetComponent<PlaySoundsComponent>();
        }

        public void AddResource()
        {
            if (_character.TakenItems.Count <= 0) return;

            foreach (var resource in _character.TakenItems.ToArray())
            {
                var item = resource.GetComponent<InventoryComponent>();
                
                CheckNeedResource(item);
            }
        }

        private void CheckNeedResource(InventoryComponent resource)
        {
            foreach (InventoryItem item in _needItems)
            {
                if (item.Name != resource.Item.Name) continue;
                if (item.Count >= item.MaxSize) continue;
                
                _character.TakenItems.Remove(resource.gameObject);
                item.Count += resource.Item.Count;
                
                _audio.Play("Build");
                
                Inventory.Inventory.Instance.Remove(resource.Item.Name, resource.Item.Count);
                OnAddResource?.Invoke(item.Name, item.Count, item.MaxSize);
                
                Destroy(resource.gameObject);
                
                if (!IsDone()) return;
                
                CompletedBuild();
                return;
            }
        }

        private bool IsDone()
        {
            int count = 0;
            
            foreach (var item in _needItems)
            {
                if (item.Count != item.MaxSize) continue;

                count++;
            }

            return  count >= _needItems.Length;
        }

        public InventoryItem GetItem(EItemName id)
        {
            foreach (var item in _needItems)
            {
                if (item.Name != id) continue;
                return item;
            }

            return default;
        }

        [ContextMenu("Complete Build")]
        private void CompletedBuild()
        {
            _audio.Play("Complete");
            _onRepairGrave?.Invoke();
            gameObject.SetActive(false);
            _repairGrave.SetActive(true);
            _graveCanvas.SetActive(false);
            Destroy(GetComponent<ColliderEnterComponent>());
        }
        
    }
}
