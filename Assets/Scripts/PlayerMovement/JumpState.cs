using UnityEngine;

public class JumpState : IState
{
    //The speed of movement during jump
    private float _airSpeed = 7f;

    //The gravity factor
    private float _gravityStrength = 40f;

    //Jumpheight
    private float _jumpHeight = 2.5f;

    //Vertical movement component
    private Vector3 verticalVelocity;

    public void EnterState(PlayerStateManager player)
    {
        verticalVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravityStrength);
    }

    public void DoState(PlayerStateManager player)
    {
        var movement = new Vector3
        {
            x = _airSpeed * Input.GetAxisRaw("Horizontal")
        };
        
        movement = Matrix4x4.Rotate(Quaternion.Euler(0, player.mainCamera.eulerAngles.y, 0)) * movement;
        movement.y = 0;
        
        //Translate the input into the jumping vector
        movement += 0.75f * player.jumpMomentum;
        
        //Add vertical velocity to movement vector
        movement += verticalVelocity;


        //Move the object
        player.playerCharacter.Move(movement * Time.deltaTime);

        //Gravity or something
        verticalVelocity.y -= _gravityStrength * Time.deltaTime;
    }

    public void ExitState(PlayerStateManager player)
    {
        //Land
        if (player.playerCharacter.isGrounded)
        {
            player.SwitchToState(player.groundState);
        }
    }
}
