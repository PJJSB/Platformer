using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class IdleState : IState
    {
        public void EnterState(Player player)
        {
            player.speed = Vector3.zero;
        }

        public void UpdateState(Player player)
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

            // If player touches floor, gravity gets reset
            if (player.controller.isGrounded)
            {
                player.speed.y = 0;
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
