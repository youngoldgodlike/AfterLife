using KinematicCharacterController.Examples;
using UnityEngine;

namespace Movement
{
    public class MyPlayer : MonoBehaviour
    {
        public ExampleCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MineCharacterController Character;

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";
        private MainControls _mainControls;
        private Transform _checkPoint;

        public bool IsPressedMoveAxes { get; private set; }

        private void Awake() {
            OrbitCamera.SetFollowTransform(CameraFollowPoint);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform

            // Ignore the character's collider(s) for camera obstruction checks
            OrbitCamera.IgnoredColliders.Clear();
            OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());

            _mainControls = new MainControls();
            _mainControls.Player.Enable();
        }

        private void Update()
        {
            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            Vector2 mouseLook = _mainControls.Player.Look.ReadValue<Vector2>();


            Vector3 lookInputVector = new Vector3(mouseLook.x, mouseLook.y, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
        }

        private void HandleCharacterInput()
        {
            MinePlayerCharacterInputs characterInputs = new MinePlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxes = _mainControls.Player.Move.ReadValue<Vector2>();
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.Running = _mainControls.Player.Sprint.IsPressed();

            IsPressedMoveAxes = characterInputs.MoveAxes != Vector2.zero;

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}