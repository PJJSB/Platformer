using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class JumpingState : IState
    {
        public void EnterState(PlayerMovement player)
        {
            player.speed.y = Mathf.Sqrt(2 * player.jumpHeight * player.gravityStrength);
        }

        public void UpdateState(PlayerMovement player)
        {
            // Input affects movement in air
            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * player.airMovementStrength);
            player.acceleration.x = movement.x;
            player.acceleration.z = movement.z;

            // Trim the player's speed to max speed when it exceeds max speed
            Vector2 horizontalSpeed = new Vector2(player.speed.x, player.speed.z);
            if(horizontalSpeed.sqrMagnitude > player.airSpeedMax * player.airSpeedMax)
            {
                horizontalSpeed = horizontalSpeed.normalized * player.airSpeedMax;
                player.speed.x = horizontalSpeed.x;
                player.speed.z = horizontalSpeed.y;
            }

            // If player lands on floor (check is at the end to prevent getting stuck in code between pre-jump states and this state)
            if (player.controller.isGrounded)
            {
                // Play impact sound
                AudioManager.GetInstance().Play(AudioManager.SoundType.impact);

                player.acceleration.x = 0;
                player.acceleration.z = 0;

                // If the player was moving while landing
                if (player.movementInput.magnitude > player.deadzone)
                {
                    // This can happen if the player lands while already holding shift
                    if (Input.GetKey(KeyCode.LeftControl))
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
                else
                {
                    player.SwitchState(player.idleState);
                    return;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space)) 
            {
                player.SwitchState(player.doubleJumpingState);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player.SwitchState(player.dashingState);
                return;
            }
        }
    }
}
