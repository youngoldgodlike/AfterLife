using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Character.Interactable
{
    [RequireComponent(typeof(CheckOverlapResourceComponent), typeof(CharacterTakeSystem),typeof(InteractableInputSystem))]
    public class CharacterInteract : MonoBehaviour
    {
        [SerializeField] private CheckOverlapResourceComponent _checkOverlapResource;

        public void Interact()
        {
            _checkOverlapResource.Check();
        }
    }
}
