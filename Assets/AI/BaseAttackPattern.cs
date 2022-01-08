using Enemies;

namespace AI
{
    public abstract class BaseAttackPattern : IAttackPattern
    {
        protected readonly SpiderControlEventBus Controller;
        protected readonly SpiderVision Vision;

        protected BaseAttackPattern(SpiderControlEventBus controller)
        {
            Controller = controller;
        }
        protected BaseAttackPattern(SpiderControlEventBus controller, SpiderVision vision)
        {
            Controller = controller;
            Vision = vision;
        }
        public abstract bool End();

        public abstract void Reset();

        public abstract void Execute(); 
    }
}