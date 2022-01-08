using Enemies;
using UnityEngine;

namespace AI
{
    public abstract class TimeOutPattern : BaseAttackPattern
    {
        protected float StartTimestamp;
        private readonly float _duration;

        protected TimeOutPattern(SpiderControlEventBus controller, SpiderVision vision, float duration = float.PositiveInfinity) : base(controller, vision)
        {
            _duration = duration;
        }

        public override bool End()
        {
            return Time.time - StartTimestamp >= _duration;
        }

        public override void Reset()
        {
            StartTimestamp = 0;
        }

        public override void Execute()
        {
            if (StartTimestamp == 0) StartTimestamp = Time.time;
        }
    }
}