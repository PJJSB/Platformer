using UnityEngine;

public class GroundedState : IState
{
    private Vector3 _movement;

    private float _walkSpeed = 10f;
    private float _runSpeed = 16f;

    public void EnterState(PlayerStateManager player)
    {
        
    }

    public void DoState(PlayerStateManager player)
    {
        //Movement vector
        _movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            z = Input.GetAxisRaw("Vertical")
        };
        _movement *= _walkSpeed;
            
        //Run faster if shift is pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _movement *= _runSpeed / _walkSpeed;
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
            player.playerCharacter.Move(new Vector3(0,-1,0));
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.jumpMomentum = _movement;
            player.SwitchToState(player.jumpState);
        }
    }
}
