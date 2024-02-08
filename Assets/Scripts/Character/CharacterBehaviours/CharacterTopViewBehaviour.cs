using KinematicCharacterController.Examples;
using UnityEngine;

namespace Assets.Scripts.Character.CharacterBehaviours
{
    public class TopViewBehaviour : ICharacterBehaviour
    {
        public void Set(Transform character)
        {
            var camera = character.gameObject.GetComponentInChildren<ExampleCharacterCamera>();
            camera.DefaultDistance = 10f;
            camera.MinDistance = 5f;
            camera.MaxDistance = 15f;
            camera.DefaultVerticalAngle = 50f;
            camera.MinVerticalAngle = 50f;
            camera.MaxVerticalAngle = 50f;
        }
    }
}
