using KinematicCharacterController;
using UnityEngine;

public class DialogBehaviour :  ICharacterBehaviour
{
    public void Set(Transform character)
    {
        character.gameObject.GetComponent<MineCharacterController>().IsDialog = true;
        character.GetComponent<KinematicCharacterMotor>().BaseVelocity = Vector3.zero;
    }
}