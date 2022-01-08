using System;
using UnityEngine;

namespace ValueObjects
{
    [Serializable]
    public class FireRate
    {
        [SerializeField]
        private float shotsPerSecond;
        
        public FireRate(float shotsPerSecond=1)
        {
            this.shotsPerSecond = shotsPerSecond;
        }
        public float Value => (1f / shotsPerSecond);

        public float ShootsPerSecond => shotsPerSecond;

        public int Milliseconds => (int)(Value * 1000) ;

        public static float FireRateToShootsPerSecond(float fireRate)
        {
            if (fireRate <= 0) return 0;
            return 1 / fireRate; 
        }

        public FireRate GetMultiplier(Multiplier aMultiplier)
        {
            return new FireRate(shotsPerSecond * aMultiplier);
        }
    }
}