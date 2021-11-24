using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class JumpingState : IState
    {
        private float jumpHeight = 2.5f;
        private float airMovementStrength = 50f;
        private float airSpeedMax = 12f;

        public void EnterState(PlayerMovement player)
        {
            player.speed.y = Mathf.Sqrt(2 * jumpHeight * player.gravityStrength);
        }

        public void UpdateState(PlayerMovement player)
        {
            // Input affects movement in air
            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * airMovementStrength);
            player.acceleration.x = movement.x;
            player.acceleration.z = movement.z;

            // Trim the player's speed to max speed when it exceeds max speed
            Vector2 horizontalSpeed = new Vector2(player.speed.x, player.speed.z);
            if(horizontalSpeed.sqrMagnitude > airSpeedMax * airSpeedMax)
            {
                horizontalSpeed = horizontalSpeed.normalized * airSpeedMax;
                player.speed.x = horizontalSpeed.x;
                player.speed.z = horizontalSpeed.y;
            }

            // If player lands on floor (check is at the end to prevent getting stuck in code between pre-jump states and this state)
            if (player.controller.isGrounded)
            {
                player.acceleration.x = 0;
                player.acceleration.z = 0;
                // If the player was moving while landing
                if (player.movementInput.magnitude > player.deadzone)
                {
                    // This can happen if the player lands while already holding shift
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
                else
                {
                    player.SwitchState(player.idleState);
                    return;
                }
            }
        }
    }
}
