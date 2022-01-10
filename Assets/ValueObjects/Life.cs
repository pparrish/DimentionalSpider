using System;
using UnityEngine;

namespace ValueObjects
{
    [Serializable]
    public class Life
    {
        public static float MaximumAllowed = 1000;
        [SerializeField] private float value;
        public float Value => value;
        
        public Life(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Life amount must be positive : " + amount);
            }

            if (amount > MaximumAllowed)
            {
                throw new ArgumentException("Life have a maximum of " + MaximumAllowed + ", get " + amount);
            }
            value = amount;
        }

        public Life Damage(Life amount)
        {
            var result = Value - amount.Value;
            return new Life(result < 0 ? 0 : result );
        }
        
        public Life Heal(Life amount)
        {
            var result = Value + amount.value;
            return new Life(result > MaximumAllowed ? MaximumAllowed : result);
        }
    }
}