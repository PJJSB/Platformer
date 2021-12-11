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

            // If player releases sneak button
            if (player.playerInput.actions["Sneak"].ReadValue<float>() < 0.5f)
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.SwitchState(player.runningState);
                return;
            }

            // If jumping
            if (player.playerInput.actions["Jump"].triggered && player.controller.isGrounded)
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.SwitchState(player.jumpingState);
                return;
            }
            
            if (!player.controller.isGrounded)
            {
                player.animator.SetBool(isWalkingAnimator, false);
                player.state = player.jumpingState;
                return;
            }

            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * walkingSpeed);
            player.speed.x = movement.x;
            player.speed.z = movement.z;
        }
    }
}