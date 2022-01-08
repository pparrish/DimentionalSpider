using Enemies;

namespace AI.Attack
{
    public class ConstantAttack : BaseAttackPattern
    {
        private readonly int _weapon;
        public ConstantAttack(SpiderControlEventBus controller, int weapon = 0) : base(controller)
        {
            _weapon = weapon;
        }

        public override bool End()
        {
            return false;
        }

        public override void Reset()
        {}

        public override void Execute()
        {
            Controller.Shoot(_weapon);
        }
    }
}