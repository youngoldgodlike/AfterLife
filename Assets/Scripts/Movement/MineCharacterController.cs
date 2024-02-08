using System;
using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using KinematicCharacterController;
using UnityEngine;
using Utils;

public struct MinePlayerCharacterInputs
{
    public Vector2 MoveAxes;
    public Quaternion CameraRotation;
    public bool Running;
}

public class MineCharacterController : MonoBehaviour,ICharacterController
{
    public KinematicCharacterMotor Motor;
    public GameObject FollowObj;

    [Header("GroundMovement")] 
    [Range(0f, 1f)] public float SpeedSmooth;
    [Min(0f)] public float Drag;
    [Min(0f)] public float speed;
    [Min(0f)] public float speedMultiplier;
    [Range(0f, 1f)] public float rotateSmooth;

    [Header("Misc")]
    public Vector3 Gravity = new Vector3(0f, -20f, 0f);
    public float AirSpeed;
    public Transform Camera;

    [SerializeField] private Vector3 currentPlayerVelocity;
    [SerializeField] private Vector3 targetMovementVelocity = Vector3.zero;
    [SerializeField] private Vector3 _lastDirection;

    private bool _isRunMode, _isMoving, _isCaught;
    private Vector3 _moveInputVector;
    private Vector3 _directionMoveVector;
    private Vector3 _lookInputVector;
    private Vector3 _cameraDirection;
    private Quaternion _cameraRotation, _enemyDirection;
    private float _objSpeed;

    public bool IsDialog { get; set; }
    public bool IsRun => _isRunMode;
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _lastDirection = transform.forward;
    }
    
    private void Start()
    {
        Motor.CharacterController = this;
        GameEvents.Instance.OnReloadLevel += Respawn;
    }

    private void OnDisable() => GameEvents.Instance.OnReloadLevel -= Respawn;

    public void StartDialog() => IsDialog = true;
    
    public void EndDialog() => IsDialog = false;
    
    public void SetInputs(ref MinePlayerCharacterInputs inputs)
    {
        // Clamp input
        Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxes.x, 0f, inputs.MoveAxes.y), 1f);
        _directionMoveVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxes.x, 0f, inputs.MoveAxes.y),1f);

        // Calculate camera direction and rotation on the character plane
        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, Motor.CharacterUp).normalized;
        if (cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, Motor.CharacterUp).normalized;
        }
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, Motor.CharacterUp);

        if (Motor.GroundingStatus.IsStableOnGround) {
            _isRunMode = inputs.Running;
        }

        // Move and look inputs
        _moveInputVector = cameraPlanarRotation * moveInputVector;
        _lookInputVector = cameraPlanarDirection;
        _cameraRotation = cameraPlanarRotation;
        _cameraDirection = cameraPlanarDirection;
    }
    
    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if (_isCaught) {
            currentRotation = Quaternion.Slerp(currentRotation, _enemyDirection, rotateSmooth);
            return;
        }   
        
        Vector3 moveInputDirection = Vector3.zero;
        Vector3 target;

        if (Input.GetKey(KeyCode.A))
            moveInputDirection += -Vector3.ProjectOnPlane(Camera.right, Motor.CharacterUp)*2;
        if(Input.GetKey(KeyCode.D))
            moveInputDirection += Vector3.ProjectOnPlane(Camera.right, Motor.CharacterUp)*2;
        if (Input.GetKey(KeyCode.W))
            moveInputDirection += Vector3.ProjectOnPlane(Camera.forward, Motor.CharacterUp)*2;
        if (Input.GetKey(KeyCode.S))
            moveInputDirection += -Vector3.ProjectOnPlane(Camera.forward, Motor.CharacterUp)*2;

        if (moveInputDirection != Vector3.zero)
        {
            target = moveInputDirection;
            _lastDirection = moveInputDirection;
        }
        else
            target = _lastDirection;

        var position = transform.position;
        
        Debug.DrawRay(position,moveInputDirection,Color.yellow,0.05f);
        Debug.DrawRay(position,target,Color.green,0.05f);

        Quaternion q = Quaternion.LookRotation(target);

        currentRotation = Quaternion.Slerp(currentRotation, q, rotateSmooth);
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
        if (IsDialog || _isCaught) {
            currentVelocity = Vector3.zero;
            return;
        }
        
        if (Motor.GroundingStatus.IsStableOnGround)
        {
            if (_isRunMode)
                _objSpeed = speed * speedMultiplier;
            else
                _objSpeed = speed;
            ///////////////////////////////////////////////////
            if (_moveInputVector.magnitude > 0f)
                targetMovementVelocity = _moveInputVector * _objSpeed;
            else
                targetMovementVelocity = Vector3.zero;
            ///////////////////////////////////////////////////
            if (_isRunMode)
                currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, SpeedSmooth / 2);
            else
                currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, SpeedSmooth);
            ///////////////////////////////////////////////////
        }
        else
        {
            if (_moveInputVector.sqrMagnitude > 0f)
            {
                targetMovementVelocity = _moveInputVector * _objSpeed;

                Vector3 velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - currentVelocity, Gravity);

                currentVelocity += velocityDiff * (AirSpeed * deltaTime);
            }

            currentVelocity += Gravity * deltaTime;

            currentVelocity *= (1f / (1f + (Drag * deltaTime)));
        }
        
        currentPlayerVelocity = currentVelocity;
        
        _isMoving = currentVelocity != Vector3.zero;
    }

    private void Respawn() {
        Motor.SetPosition(CheckPointManager.Instance.CheckPoint.position);
        _isCaught = false;
        Debug.Log("Player RESpawned");
    }

    public void Caught(Vector3 enemyPos) {
        var dir = enemyPos - transform.position;
        dir = Vector3.ProjectOnPlane(dir, Motor.CharacterUp);
        _isCaught = true;
        
        _enemyDirection = Quaternion.LookRotation(dir);
    }

    public void Unleash() => _isCaught = false;
    

    #region Misc

    public void BeforeCharacterUpdate(float deltaTime)
    {

    }

    public void PostGroundingUpdate(float deltaTime)
    {
        
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
        ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition,
        Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
        
    }

    #endregion
}

