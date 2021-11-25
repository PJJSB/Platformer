﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : PlayerStateMachine
    {
        // Other components that are attached to this game object
        public Light headlamp;
        public new Transform camera;
        [System.NonSerialized] public Animator animator;
        [System.NonSerialized] public CharacterController controller;

        // Indicates horizontal movement (WASD for 1/0 or analog stick for values inbetween)
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

        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            state = idleState;
            acceleration.y = -gravityStrength;
        }

        void Update()
        {
            // Get movement input (WASD/arrow keys/analog stick or whatever)
            movementInput = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                z = Input.GetAxisRaw("Vertical")
            };

            // Limit input intensity to 1
            if (movementInput.sqrMagnitude > 1)
            {
                movementInput.Normalize();
            }

            // Rotate player when user provides movement input
            if (movementInput.magnitude > deadzone)
            {
                AlignPlayerRotationWithCamera();
            }

            // If player touches floor, gravity gets reset
            if (controller.isGrounded)
            {
                speed.y = floorGlue;
            }

            // Handle state specific behavior
            state.UpdateState(this);

            ApplyPhysics();

            if (Input.GetKeyDown("l"))
            {
                headlamp.enabled = !headlamp.enabled;
            }
        }

        public void ApplyPhysics()
        {
            // Apply acceleration to speed
            speed += acceleration * Time.deltaTime;

            // Apply speed to movement
            controller.Move(speed * Time.deltaTime);
        }

        public void AlignPlayerRotationWithCamera()
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
    }
}