namespace Assets.Scripts.Player
{
    public interface IState
    {
        void EnterState(PlayerMovement player);
        void UpdateState(PlayerMovement player);
    }
}