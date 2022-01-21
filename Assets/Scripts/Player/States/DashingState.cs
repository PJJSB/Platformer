using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class DashingState : IState
    {
        private float dashingSpeed = 30f;
        private float airSpeedMax = 30f;
        private float dashingTime;
        private float dashingTimeMax = 0.2f;
        
        private static readonly int isDashingAnimator = Animator.StringToHash("isDashing");
        private static readonly int isFallingAnimator = Animator.StringToHash("isFalling");

        public void EnterState(PlayerMovement player)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundType.dash);

            player.animator.SetBool(isFallingAnimator, false);
            player.animator.SetBool(isDashingAnimator, true);
            Vector3 movement;

            if (player.movementInput.magnitude > player.deadzone)
            {
                movement = player.AlignMovementWithCamera(player.movementInput.normalized * dashingSpeed);
            }
            else
            {
                movement = Quaternion.Euler(0, player.transform.eulerAngles.y, 0) * new Vector3(0, 0, dashingSpeed);
            }
            
            player.speed = new Vector3(movement.x, 0, movement.z);
            dashingTime = 0f;
            player.acceleration.y = 0;
        }

        public void UpdateState(PlayerMovement player)
        {
            // Input affects movement in air
            Vector3 movement = player.AlignMovementWithCamera(player.movementInput * player.airMovementStrength);
            player.acceleration.x = movement.x;
            player.acceleration.z = movement.z;

            // Trim the player's speed to max speed when it exceeds max speed
            Vector2 horizontalSpeed = new Vector2(player.speed.x, player.speed.z);
            if (horizontalSpeed.sqrMagnitude > airSpeedMax * airSpeedMax)
            {
                horizontalSpeed = horizontalSpeed.normalized * airSpeedMax;
                player.speed.x = horizontalSpeed.x;
                player.speed.z = horizontalSpeed.y;
            }

            // Enable gravity again after the actual "dash" has finished
            if (dashingTime > dashingTimeMax)
            {
                player.acceleration.y = -player.gravityStrength;
            }

            dashingTime += Time.deltaTime;

            // If player lands on floor (check is at the end to prevent getting stuck in code between pre-jump states and this state)
            if (player.controller.isGrounded)
            {
                // If player lands before the dash was finished, enable gravity again
                if (dashingTime < dashingTimeMax)
                {
                    player.acceleration.y = -player.gravityStrength;
                }

                // Play impact sound
                AudioManager.GetInstance().PlaySound(AudioManager.SoundType.impact);

                player.acceleration.x = 0;
                player.acceleration.z = 0;

                // If the player was moving while landing
                if (player.movementInput.magnitude > player.deadzone)
                {
                    // This can happen if the player lands while already holding the sneak button
                    if (player.playerInput.actions["Sneak"].ReadValue<float>() > 0.5f)
                    {
                        player.animator.SetBool(isDashingAnimator, false);
                        player.SwitchState(player.walkingState);
                        return;
                    }
                    else
                    {
                        player.animator.SetBool(isDashingAnimator, false);
                        player.SwitchState(player.runningState);
                        return;
                    }
                }
                else
                {
                    player.animator.SetBool(isDashingAnimator, false);
                    player.SwitchState(player.idleState);
                    return;
                }
            }
        }
    }
}
