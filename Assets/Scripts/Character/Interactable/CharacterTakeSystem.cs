using System.Collections.Generic;
using Assets.Scripts.Components;
using Assets.Scripts.Components.Audio;
using Assets.Scripts.Inventory;
using Components.Items;
using UnityEngine;

namespace Assets.Scripts.Character.Interactable
{
    public class CharacterTakeSystem : MonoBehaviour
    {
        [SerializeField] private Transform _storagePosition;
        
        private List<GameObject> _takenItems = new List<GameObject>(3);
        private PlaySoundsComponent _audio;

        private void Awake()
        {
            _audio = GetComponent<PlaySoundsComponent>();
        }

        public List<GameObject> TakenItems => _takenItems;
        
        public void TakeResource(GameObject go)
        {
           var resource = go.GetComponent<InventoryComponent>();

           switch (resource.Item.Attribute)
           {
                case EItemAttribute.Rotating:
                    AddRotatingResource(resource);
                    break;
                
                case EItemAttribute.Taking:
                    AddTakingResource(resource);
                    break;
           }
        }
        
        public void TakeFromStorage(InventoryComponent resource)
        {
            if (_takenItems.Count >= 3)  return;
            
            Inventory.Inventory.Instance.Add(resource.Item.Name, 1);
            _audio.Play("Take");
            
            var itemTaken = Instantiate(resource.gameObject, _storagePosition,false);
            itemTaken.transform.SetParent(_storagePosition);
            itemTaken.transform.localPosition = new Vector3(1f, 0.5f, 1f);
            _takenItems.Add(itemTaken);
        }

        public void DropItem()
        {
            if (_takenItems.Count <= 0) return;
            
            var itemCount = _takenItems.Count - 1;

            SetRigidBodyParam(_takenItems[itemCount].gameObject, false);
            _takenItems[itemCount].transform.SetParent(null);
            _takenItems[itemCount].layer = LayerMask.NameToLayer("Interactable");
                
            var item = _takenItems[itemCount].GetComponent<InventoryComponent>();
            Inventory.Inventory.Instance.Remove(item.Item.Name, 1);
                
            _takenItems.RemoveAt(_takenItems.Count - 1 );
            _audio.Play("Drop");
        }

        public void DestroyItem(GameObject item)
        {
            Destroy(item);
            _takenItems.Remove(item);
        }

        private void AddTakingResource(InventoryComponent resource)
        {
            resource.TakeItem(resource);
        }

        private void AddRotatingResource(InventoryComponent resource)
        {
            if (_takenItems.Count >= 3) return;
            
            resource.gameObject.layer = LayerMask.NameToLayer("NonInteractable");
            SetRigidBodyParam(resource.gameObject, true);
            _audio.Play("Take");
            _takenItems.Add(resource.gameObject);

            resource.gameObject.transform.position = new Vector3(resource.gameObject.transform.position.x,
                resource.gameObject.transform.position.y + 1,
                resource.gameObject.transform.position.z);
            resource.gameObject.transform.SetParent(_storagePosition);
            
            Inventory.Inventory.Instance.Add(resource.Item.Name, 1);
        }

        private void SetRigidBodyParam(GameObject go, bool isEnable)
        {
            go.GetComponent<Rigidbody>().isKinematic = isEnable;
            go.GetComponent<MeshCollider>().isTrigger = isEnable;
        }
    }
}
