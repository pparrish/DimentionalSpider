using ValueObjects;

namespace Bullets
{
    public interface ICanTakeStatusEffects
    {
        void TakeStatusEffect(string attribute, float change,  float duration);
        string TakeStatusEffect(Multiplier change,  float duration = float.PositiveInfinity);
        void RemoveStatusEffect(string statusEffectID);
    }
}