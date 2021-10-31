using UnityEngine;

public class GroundedState : IState
{
    Vector3 movement;
    
    public float _walkSpeed = 10f; 
    public float _runSpeed = 16f;
    public bool isRunning = false;

    public void EnterState(PlayerStateManager player)
    {
        player.jumpMomentum = new Vector3(0, 0, 0);
        return;
    }

    public void DoState(PlayerStateManager player)
    {
        //Translate the input into the movement vector
        movement = new Vector3(0, 0, 0);
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = movement.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        //Move the object
        if (isRunning)
        {
            player.playerCharacter.Move(movement * _runSpeed * Time.deltaTime);
        } else
        {
            player.playerCharacter.Move(movement * _walkSpeed * Time.deltaTime);
        }
        
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
            isRunning = false;
            player.jumpMomentum = movement;
            player.SwitchToState(player.jumpState);
            return;
        }
    }
}
