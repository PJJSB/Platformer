
public interface IState
{
    //State Entry Logic
    void EnterState(PlayerStateManager player);

    //State Logic
    void DoState(PlayerStateManager player);

    //State Switching Logic
    void ExitState(PlayerStateManager player);
}
