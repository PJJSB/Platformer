using UnityEngine;

public class GroundedState : IState
{
    //The speed of walking
    private float _walkSpeed = 10f; 

    public void EnterState(PlayerStateManager player)
    {
        return;
    }

    public void DoState(PlayerStateManager player)
    {
        //Translate the input into the movement vector
        Vector3 movement = new Vector3(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        //Move the object
        player.playerCharacter.Move(movement * _walkSpeed * Time.deltaTime);
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
            player.SwitchToState(player.jumpState);
            return;
        }
    }
}
