using UnityEngine;

public class JumpState : IState
{
    //The speed of movement during jump
    private float _airSpeed = 5.0f;

    //The gravity factor
    private float _gravityStrength = 9.82f;

    //Jumpheight
    private float _jumpHeight = 2.5f;

    //Vertical movement component
    private Vector3 verticalVelocity = new Vector3(0, 0, 0);

    public void EnterState(PlayerStateManager player)
    {
        //Velocityrequired = SQRT(-2 * Height * Gravity) where gravity is negative
        verticalVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravityStrength);
    }

    public void DoState(PlayerStateManager player)
    {
        //TODO: add player.jumpMomentum to calculation so the jump doesnt move half as fast as walking, just the movement during the jump
        //Translate the input into the movement vector
        Vector3 movement = new Vector3(0, 0, 0);
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = movement.normalized;
        movement = Matrix4x4.Rotate(player.camera.rotation) * movement;
        movement.y = 0;

        //Move the object
        player.playerCharacter.Move(movement * _airSpeed * Time.deltaTime);

        //Gravity or something
        player.playerCharacter.Move(verticalVelocity * Time.deltaTime);
        verticalVelocity.y -= _gravityStrength * Time.deltaTime;
    }

    public void ExitState(PlayerStateManager player)
    {
        //Land
        if (player.playerCharacter.isGrounded)
        {
            player.SwitchToState(player.groundState);
            return;
        }
    }
}
