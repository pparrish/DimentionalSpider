namespace AI
{
    public interface IAttackPattern
    {
        bool End();
        void Reset();
        void Execute();
    }
}