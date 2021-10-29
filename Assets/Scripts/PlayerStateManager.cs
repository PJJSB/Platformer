using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public Vector3 jumpMomentum;
    //Coupling to the player controller
    public CharacterController playerCharacter;
    //Active player state
    private IState _playerState;
    //Allowed and instantiated player states
    public IState groundState = new GroundedState();
    public IState jumpState = new JumpState();

    // Start is called before the first frame update
    void Start()
    {
        //Set the initial state
        SwitchToState(groundState);
    }

    // Update is called once per frame
    void Update()
    {
        //State switching logic
        _playerState.ExitState(this);
        //Main State logic
        _playerState.DoState(this);
    }

    public void SwitchToState(IState newState)
    {
        //Change state to new state
        _playerState = newState;
        //Execute new state entry logic
        _playerState.EnterState(this);
    }
}
