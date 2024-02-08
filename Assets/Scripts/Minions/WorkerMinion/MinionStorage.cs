using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Minions
{
    public class MinionStorage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resourcesType;
        [SerializeField] private TextMeshProUGUI _textResources;
        [SerializeField] private GameObject _resource;
        [SerializeField] private GameObject _owner;

        private const int MaxResources = 10;

        [SerializeField, Min(0f)] private int _resources = 0;
        public Action OnNotEmpty;

        public bool IsAbleToTake() => _resources > 0;

        public void ChangeResourcesType(JobType jobType,GameObject resource) {
            switch (jobType) {
                case JobType.Lumberjack:
                    _resourcesType.text = "Wood";
                    break;
                case JobType.Miner:
                    _resourcesType.text = "Stone";
                    break;
            }

            _resource = resource;
        }
    
        public void PutResources(out bool storageFull) {
            Debug.Log("resources before put:" + _resources);
            if (_resources != MaxResources) {
                _resources += 1;
                ChangeText();
            }
            Debug.Log("resources after put:" + _resources);
        
            storageFull = _resources >= MaxResources;
        }

        public GameObject TakeResource() {
            if (_resources == 0) return null;
            _resources -= 1;
            ChangeText();
        
            return _resource;
        }

        private void ChangeText() {
            _textResources.text = $"{_resources}/{MaxResources}";
        
            if(_resources != MaxResources) OnNotEmpty?.Invoke();
        }

        public void SetOwner(GameObject minionOwner) => _owner = minionOwner;
        public GameObject GetOwner() => _owner;
    }
}
