using Bullets;
using ValueObjects;

namespace AI.Boost
{
    public class Boost : IAttackPattern
    {
        private ICanTakeStatusEffects _fireRate;
        private Multiplier _change;
        private string _statusEffectID = "";
        private readonly Boost _addStatusEffect;
        private readonly Boost _removeStatusEffect;
        private bool _listenerAdded;
        private bool _statusApplied;

        public Boost(ICanTakeStatusEffects statusTaker, Multiplier change)
        {
            _removeStatusEffect = new Boost(this);
            _change = change;
            _removeStatusEffect._change = change;
            _fireRate = statusTaker;
            _removeStatusEffect._fireRate = statusTaker;
        }

        private Boost(Boost addBoost)
        {
            _addStatusEffect = addBoost;
        }

        public bool End()
        {
            if(_addStatusEffect == null) return _statusEffectID != "" && _statusApplied;
            return _statusEffectID == "" ;
        }

        public void Reset()
        {
            _statusApplied = false;
            _statusEffectID = "";
        }

        public void Execute()
        {
            if (_statusEffectID == "" && _addStatusEffect == null)
            {
                _statusEffectID = _fireRate.TakeStatusEffect(_change);
                if (!_listenerAdded)
                {
                    _fireRate.OnBoostAnimationFinish.AddListener(() => _statusApplied = true );
                    _listenerAdded = true;
                }

                _removeStatusEffect._statusEffectID = _statusEffectID;
            }
            if (_statusEffectID != "" && _addStatusEffect != null)
            {
                _fireRate.RemoveStatusEffect(_statusEffectID);
                _statusEffectID = "";
                _addStatusEffect._statusEffectID = "";
            }
        }
        
        public Boost GetStatusEffectRemoverPattern()
        {
            return _removeStatusEffect;
        }
    }
}