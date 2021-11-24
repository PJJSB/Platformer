namespace Assets.Scripts.Player
{
    public interface IState
    {
        void EnterState(Player player);
        void UpdateState(Player player);
    }
}