using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : PlayerStateMachine
    {
        // Other components that are attached to this game object
        public Light headlamp;
        public new Transform camera;
        [System.NonSerialized] public Animator animator;
        [System.NonSerialized] public PlayerInput playerInput;
        [System.NonSerialized] public CharacterController controller;

        // Indicates horizontal movement
        public Vector3 movementInput;
        public Vector3 speed;
        public Vector3 acceleration;

        // Can later be set to hidden but for now when testing if the reverse section is being started correctly having it on the ui is pretty nice
        public bool isReversing;

        // Deadzone for controllers to avoid analog drift. Everything equal to or below deadzone gets ignored.
        [System.NonSerialized] public float deadzone = 0.1f;

        // How many unity units the player should move downward to stay attached on the floor during floor movement
        [System.NonSerialized] public float floorGlue = -5f;
        [System.NonSerialized] public float gravityStrength = 40f;
        [System.NonSerialized] public float jumpHeight = 2.5f;
        [System.NonSerialized] public float airMovementStrength = 50f;
        [System.NonSerialized] public float airSpeedMax = 16f;
        
        private static readonly int isFallingAnimator = Animator.StringToHash("isFalling");
        private static readonly int isJumpingAnimator = Animator.StringToHash("isJumping");
        private static readonly int isDoubleJumpingAnimator = Animator.StringToHash("isDoubleJumping");
        private static readonly int isDashingAnimator = Animator.StringToHash("isDashing");

        void Start()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            controller = GetComponent<CharacterController>();
            state = idleState;
            acceleration.y = -gravityStrength;
        }

        void Update()
        {
            // Get movement input for walking around
            Vector2 horizontalMovement = playerInput.actions["Move"].ReadValue<Vector2>();
            movementInput = new Vector3(horizontalMovement.x, 0, horizontalMovement.y);

            // Rotate player when user provides movement input
            if (movementInput.magnitude > deadzone)
            {
                AlignPlayerRotationWithCamera();
            }

            // If player touches floor, gravity gets reset
            if (controller.isGrounded)
            {
                animator.SetBool(isFallingAnimator, false);
                speed.y = floorGlue;
            }
            else
            {
                if (speed.y < floorGlue)
                {
                    animator.SetBool(isJumpingAnimator, false);
                    animator.SetBool(isDoubleJumpingAnimator, false);
                    animator.SetBool(isDashingAnimator, false);
                    animator.SetBool(isFallingAnimator, true);
                }
            }

            // Handle state specific behavior
            state.UpdateState(this);

            ApplyPhysics();
        }

        void ApplyPhysics()
        {
            // Apply acceleration to speed
            speed += acceleration * Time.deltaTime;

            // Apply speed to movement
            controller.Move(speed * Time.deltaTime);
        }

        void AlignPlayerRotationWithCamera()
        {
            float smoothSpeed = 20f;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(speed.x, 0, speed.z));
            transform.rotation = Quaternion.Lerp
            (
                transform.rotation,
                targetRotation,
                smoothSpeed * Time.deltaTime
            );
        }

        public Vector3 AlignMovementWithCamera(Vector3 movement)
        {
            // Rotate movement to same direction as camera (vector's forward would then align with camera's forward)
            return Quaternion.Euler(0, camera.eulerAngles.y, 0) * movement;
        }

        public void SwitchState(IState state)
        {
            base.state = state;
            base.state.EnterState(this);
        }

        void OnHeadlamp(InputValue value)
        {
            headlamp.enabled = !headlamp.enabled;
        }
    }
}