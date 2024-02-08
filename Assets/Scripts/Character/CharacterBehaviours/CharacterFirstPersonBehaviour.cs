using KinematicCharacterController.Examples;
using UnityEngine;

namespace Assets.Scripts.Character.CharacterBehaviours
{
    public class FirstPersonBehaviour :  ICharacterBehaviour
    {
        public void Set(Transform character)
        {
            var camera = character.gameObject.GetComponentInChildren<ExampleCharacterCamera>();
            camera.DefaultDistance = 4f;
            camera.MinDistance = 3f;
            camera.MaxDistance = 4f;
            camera.DefaultVerticalAngle = 20f;
            camera.MinVerticalAngle = 20f;
            camera.MaxVerticalAngle = 20;
            camera.FollowTransform.position += new Vector3(0f, 2f, 0f);
            //Debug.Log("Set new follow point for camera");
            //camera.SetFollowTransform(GameObject.Find("FollowPoint2").transform);
        }
    }
}
