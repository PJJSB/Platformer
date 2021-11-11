using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public Animator animator;
    
    [HideInInspector]
    public Vector3 jumpMomentum;

    // Can later be set to hidden but for now when testing if the reverse section is being started correctly having it on the ui is pretty nice
    public bool isReverseSection; 

    // Coupling to the player controller
    public CharacterController playerCharacter;
    // Active player state
    private IState _playerState;
    // Allowed and instantiated player states
    public IState groundState = new GroundedState();
    public IState jumpState = new JumpState();
    // Camera transform component
    public Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Set the initial state
        SwitchToState(groundState);
        //Hide cursor
        Cursor.visible = false;
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
