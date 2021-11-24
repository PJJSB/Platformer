using Assets.Scripts.Player.States;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public abstract class PlayerStateMachine : MonoBehaviour
    {
        public IState state;

        public IdleState idleState = new IdleState();
        public WalkingState walkingState = new WalkingState();
        public RunningState runningState = new RunningState();
        public JumpingState jumpingState = new JumpingState();
        public DoubleJumpingState doubleJumpingState = new DoubleJumpingState();
        public DashingState dashingState = new DashingState();
        public GrapplingState grapplingState = new GrapplingState();
    }
}
