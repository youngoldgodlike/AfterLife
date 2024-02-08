using Assets.Scripts.Character.Interactable;
using Assets.Scripts.Components;
using Assets.Scripts.Utils;
using Components.Items;
using Minions;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Minions
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] private GameObject _info;
        [SerializeField] private MinionStorage _storage;
        [SerializeField] private CharacterTakeSystem _characterTakeSystem;
        [SerializeField] private LayerMask _playerLayer;

        private bool _isAbleToTake;

        private void Start()
        {
            _info.SetActive(true);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.E) && _storage.IsAbleToTake()) {
                if (_characterTakeSystem.TakenItems.Count < 3)
                    _characterTakeSystem.TakeFromStorage(_storage.TakeResource().GetComponent<InventoryComponent>());
            }
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.IsInLayer("MainCharacter")) {
                Debug.Log("COMMITED");
                _info.SetActive(true);
                _characterTakeSystem = other.GetComponent<CharacterTakeSystem>();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!other.gameObject.IsInLayer(_playerLayer)) return;

            _info.SetActive(false);
        }
    }
    
}
