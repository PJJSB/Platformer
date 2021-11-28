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
                // This can happen if the player starts walking while already holding the sneak button
                if (player.playerInput.actions["Sneak"].ReadValue<float>() > 0.5f)
                {
                    player.SwitchState(player.walkingState);
                    return;
                }
                else
                {
                    player.SwitchState(player.runningState);
                    return;
                }
            }

            // If jumping
            if (player.playerInput.actions["Jump"].triggered && player.controller.isGrounded)
            {
                player.SwitchState(player.jumpingState);
                return;
            }
        }
    }
}
