using UnityEngine;

namespace Assets.Scripts.Character.Interactable
{
    public class InteractableInputSystem : MonoBehaviour
    {
        private CharacterInteract _interact;
        private CharacterTakeSystem _characterResources;

        private void Awake()
        {
            _interact = GetComponent<CharacterInteract>();
            _characterResources = GetComponent<CharacterTakeSystem>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _interact.Interact();
            if (Input.GetMouseButtonDown(1))
                _characterResources.DropItem();
        }
    }
}
