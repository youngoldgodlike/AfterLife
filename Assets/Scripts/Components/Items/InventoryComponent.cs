using System;
using Assets.Scripts.Components.Audio;
using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Items
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField] private PlayAudioComponent _audio;
        [SerializeField] private GameObject _particle;
        [SerializeField] private InventoryItem _item;
        [SerializeField] private UnityEvent _onTakeItem;

        private Animator _animator;

        private Action _onEndAnimation;
        
        private readonly int IsTake = Animator.StringToHash("isTake");

        private bool _isTaken;
        
        public InventoryItem Item => _item;
        
        private void Awake()
        {
            if (_item.Attribute == EItemAttribute.Taking)
                _animator = GetComponent<Animator>();
        }

        public void TakeItem(InventoryComponent resource)
        {
            if (_isTaken) return;
            if (_animator == null) return;

            _onEndAnimation = (() =>
            {
                Assets.Scripts.Inventory.Inventory.Instance.Add(resource.Item.Name, 1);
                Destroy(resource.gameObject);
                _audio?.Play();
            });

            _isTaken = true;
            _particle.SetActive(true);
            _audio?.Play();
            _animator.SetTrigger(IsTake);
            _onTakeItem?.Invoke();
        }

        private void OnEndAnimation() => _onEndAnimation?.Invoke();
    }
}
