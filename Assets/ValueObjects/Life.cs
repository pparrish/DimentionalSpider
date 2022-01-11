using System;
using UnityEngine;

namespace ValueObjects
{
    [Serializable]
    public class Damage
    {
        [SerializeField]
        private float value;
        public float Value => value;
        public Damage(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Damage only allow positive numbers : " + value);
            }
            this.value = value;
        }
        
        public Damage Modificator(Multiplier multiplier)
        {
            return new Damage(value * multiplier);
        }
    }
    
    
    [Serializable]
    public class Life
    {
        public static float MaximumAllowed = 100;
        [SerializeField] private float actual;
        [SerializeField] private float total;
        public float Actual => actual;
        public float Total => total;
        
        public Life(float total)
        {
            if (total < 0)
            {
                throw new ArgumentException("Life amount must be positive : " + total);
            }
            if (total > MaximumAllowed)
            {
                throw new ArgumentException("Life have a maximum of " + MaximumAllowed + ", get " + total);
            }

            this.total = total;
            actual  = total;
        }

        private Life(float total, float amount): this(total)
        {
            actual = amount;
        }

        public Life Damage(Damage toDamage)
        {
            var result = actual - toDamage.Value;
            return new Life(Total , result < 0 ? 0 : result );
        }
        
        public Life Heal(Damage toHeal)
        {
            var result = actual  + toHeal.Value;
            return new Life(total, result >= Total ? total : result);
        }
    }
}