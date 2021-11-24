using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class IdleState : IState
    {
        public void EnterState(PlayerMovement player)
        {
            player.speed = Vector3.zero;
        }

        public void UpdateState(PlayerMovement player)
        {
            // If not idle
            if (player.movementInput.magnitude > player.deadzone)
            {
                // This can happen if the player starts walking while already holding shift
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    player.SwitchState(player.runningState);
                    return;
                }
                else
                {
                    player.SwitchState(player.walkingState);
                    return;
                }
            }

            // If jumping
            if (Input.GetKeyDown(KeyCode.Space) && player.controller.isGrounded)
            {
                player.SwitchState(player.jumpingState);
                return;
            }
        }
    }
}
