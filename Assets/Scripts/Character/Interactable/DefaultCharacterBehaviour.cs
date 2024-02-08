using UnityEngine;

public class IdleBehaviour : ICharacterBehaviour
{
    public void Set(Transform character)
    {
        character.gameObject.GetComponent<MineCharacterController>().IsDialog = false;
    }
}
