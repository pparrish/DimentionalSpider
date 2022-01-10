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
            if(_addStatusEffect == null) return _statusEffectID != "";
            return _statusEffectID == "";
        }

        public void Reset()
        {}

        public void Execute()
        {
            if (_statusEffectID == "" && _addStatusEffect == null)
            {
                _statusEffectID = _fireRate.TakeStatusEffect(_change);
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