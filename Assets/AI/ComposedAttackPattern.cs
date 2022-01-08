using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace AI
{
    public class ComposedAttackPattern : TimeOutPattern
    {
        private readonly List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();

        public enum Mode
        {
            Timer,
            Race,
            Complete
        }
        private readonly Mode _mode;

        public ComposedAttackPattern(float duration = float.PositiveInfinity, Mode mode = Mode.Timer) : base(null,  null, duration)
        {
            _mode = mode;
        }
        
        public ComposedAttackPattern(SpiderControlEventBus controller, SpiderVision vision, float duration = float.PositiveInfinity, Mode mode = Mode.Timer) : base(controller, vision, duration)
        {
            _mode = mode;
        }

        public ComposedAttackPattern AddPattern(IAttackPattern attackPattern)
        {
            _attackPatterns.Add(attackPattern);
            return this;
        }

        public override bool End()
        {
            var timerEnd = base.End();
            switch (_mode)
            {
                case Mode.Timer:
                    return timerEnd;
                case Mode.Race:
                {
                    var someComplete = _attackPatterns.Aggregate(timerEnd, (current, attackPattern) => current || attackPattern.End());
                    return someComplete;
                }
                case Mode.Complete:
                {
                    var someComplete = _attackPatterns.Aggregate(timerEnd, (current, attackPattern) => current && attackPattern.End());
                    return someComplete;
                }
                default:
                    return false;
            }
        }

        public override void Execute()
        {
            base.Execute();
            foreach (var attackPattern in _attackPatterns)
            {
                attackPattern.Execute();
            }
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var attackPattern in _attackPatterns)
            {
                attackPattern.Reset();
            }
        }
    }
}