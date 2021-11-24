using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class WalkingState : IState
    {
        private static readonly int isWalkingAnimator = Animator.StringToHash("isWalking");
        private float walkingSpeed = 10f;

        public void EnterState(PlayerMovement player)
        {
            player.animator.SetBool(isWalkingAnimator, true);
        }

        public void UpdateState(PlayerMovement player)
        {
            // If idle
            if (player.movementInput.magnitude <= player.deadzone)
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.SwitchState(player.idleState);
                return;
            }

            // If running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.SwitchState(player.runningState);
                return;
            }

            // If jumping
            if (Input.GetKeyDown(KeyCode.Space) && player.controller.isGrounded)
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.SwitchState(player.jumpingState);
                return;
            }

            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * walkingSpeed);
            player.speed.x = movement.x;
            player.speed.z = movement.z;
        }
    }
}