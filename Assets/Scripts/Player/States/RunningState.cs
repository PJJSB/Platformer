using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class RunningState : IState
    {
        private static readonly int isRunningAnimator = Animator.StringToHash("isRunning");
        private float runningSpeed = 16f;

        public void EnterState(PlayerMovement player)
        {
            player.animator.SetBool(isRunningAnimator, true);
        }

        public void UpdateState(PlayerMovement player)
        {
            // If player stops moving
            if(player.movementInput.magnitude <= player.deadzone)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.idleState);
                return;
            }

            // If walking
            if (player.playerInput.actions["Sneak"].triggered)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.walkingState);
                return;
            }

            // If jumping
            if (player.playerInput.actions["Jump"].triggered && player.controller.isGrounded)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.jumpingState);
                return;
            }

            if (!player.controller.isGrounded)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.state = player.jumpingState;
                return;
            }

            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * runningSpeed);
            player.speed.x = movement.x;
            player.speed.z = movement.z;
        }
    }
}
