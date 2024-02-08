using KinematicCharacterController;
using UnityEngine;

public class TeleportComponent : MonoBehaviour
{
    [SerializeField] private Transform _pointTeleport;
    [SerializeField] private KinematicCharacterMotor _character;

    public void Teleport() => _character.SetPosition(_pointTeleport.position);
    
}
