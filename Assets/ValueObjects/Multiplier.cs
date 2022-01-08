using System;
using UnityEngine;

namespace ValueObjects
{
    [Serializable]
    public class Multiplier
    {
        [SerializeField]
        [Min(0)]
        private float value;
        
        public  Multiplier(float value = 1)
        {
            if (value < 0)
            {
                throw new ArgumentException("Multiplier can't be negative value = " + value );
            }
            this.value = value;
        }

        public static float operator *(Multiplier a, float b) => a.value * b;
        public static float operator *(float b, Multiplier a) => a.value * b;
        public static implicit operator float(Multiplier b) => b.value;
        public static Multiplier operator *(Multiplier b, Multiplier a) => new Multiplier(a.value * b.value);
    }
}