using UnityEngine;

public class GroundedState : IState
{
    private Vector3 _movement;

    private float _walkSpeed = 10f;
    private float _runSpeed = 16f;
    private static readonly int IsWalkingAnimator = Animator.StringToHash("isWalking");
    private static readonly int IsRunningAnimator = Animator.StringToHash("isRunning");

    public void EnterState(PlayerStateManager player)
    {
        AudioManager.GetInstance().Play(AudioManager.SoundType.impact);
    }

    public void DoState(PlayerStateManager player)
    {
        //Movement vector
        _movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            z = Input.GetAxisRaw("Vertical")
        };
        //Make speed of character constant in every direction
        if (_movement.magnitude != 1)
        {
            _movement /= Mathf.Sqrt(2);
        }
        _movement *= _walkSpeed;
            
        if (_movement.magnitude >= 0.1f)
        {
            //Run faster if shift is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.animator.SetBool(IsRunningAnimator, true);
                _movement *= _runSpeed / _walkSpeed;
            }
            else
            {
                player.animator.SetBool(IsRunningAnimator, false);
                player.animator.SetBool(IsWalkingAnimator, true);
            }
        }
        else
        {
            player.animator.SetBool(IsWalkingAnimator, false);
            player.animator.SetBool(IsRunningAnimator, false);
        }
        
        //Rotate player when moving forward
        if (Input.GetAxis("Vertical") != 0)
        {
            player.transform.eulerAngles = new Vector3(0, player.mainCamera.eulerAngles.y, 0);
        }
        
        // Rotate movement to same direction as camera
        _movement = Matrix4x4.Rotate(Quaternion.Euler(0, player.mainCamera.eulerAngles.y, 0)) * _movement;
        _movement.y = 0;
        
        //Move character
        player.playerCharacter.Move(_movement * Time.deltaTime);
        
        //Gravity or something for being grounded
        if (!player.playerCharacter.isGrounded)
        {
            player.playerCharacter.Move(new Vector3(0,-20f,0) * Time.deltaTime);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && player.playerCharacter.isGrounded)
        {
            player.jumpMomentum = _movement;
            player.SwitchToState(player.jumpState);
        }
    }
}
