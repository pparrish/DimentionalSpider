using System.Collections.Generic;

namespace AI
{
    public class AttackPatternsSequence : IAttackPattern
    {
        private  readonly List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
        private readonly int _loops;
        private int _actualLoop;
        private int _activePattern;

        public AttackPatternsSequence()
        {
            _loops = -1;
        }
        
        public AttackPatternsSequence(int loops = 1)
        {
            _loops = loops <= 0? -1 : loops;
        }

        public AttackPatternsSequence AddPattern(IAttackPattern pattern)
        {
            _attackPatterns.Add(pattern);
            return this;
        }
        
        public bool End()
        {
            if (_loops == -1)
            {
                return false;
            }

            return _actualLoop >= _loops;
        }

        public void Reset()
        {
            _actualLoop = 0;
            _activePattern = 0;
            foreach (var attackPattern in _attackPatterns)
            {
                attackPattern.Reset();
            }
        }

        public void Execute()
        {
            if (_attackPatterns[_activePattern].End())
            {
                _attackPatterns[_activePattern++].Reset();
                if (_activePattern == _attackPatterns.Count)
                {
                    _activePattern = 0;
                    if (_loops != -1)
                    {
                        _actualLoop += 1;
                    }
                }
                _activePattern = _activePattern == _attackPatterns.Count
                    ? 0
                    : _activePattern;
            }

            if (_actualLoop < _loops || _loops == -1)
            {
                _attackPatterns[_activePattern].Execute();
            }
        }
    }
}