using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class RunningState : IState
    {
        private static readonly int isRunningAnimator = Animator.StringToHash("isRunning");
        private float runningSpeed = 16f;

        public void EnterState(Player player)
        {
            player.animator.SetBool(isRunningAnimator, true);
        }

        public void UpdateState(Player player)
        {
            // If player stops using WASD/analog stick
            if(player.movementInput.magnitude <= player.deadzone)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.idleState);
                return;
            }

            // If player releases shift
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.walkingState);
                return;
            }

            // If jumping
            if (Input.GetKeyDown(KeyCode.Space) && player.controller.isGrounded)
            {
                player.animator.SetBool(isRunningAnimator, false);
                player.SwitchState(player.jumpingState);
                return;
            }

            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * runningSpeed);
            player.speed.x = movement.x;
            player.speed.z = movement.z;

            // If player touches floor, gravity gets reset
            if (player.controller.isGrounded)
            {
                player.speed.y = 0;
            }
        }
    }
}
