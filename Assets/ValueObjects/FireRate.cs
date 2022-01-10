using System;
using UnityEngine;

namespace ValueObjects
{
    [Serializable]
    public class FireRate
    {
        [SerializeField]
        private float shotsPerSecond;
        [SerializeField] 
        private Multiplier multiplier = new Multiplier();
        
        public FireRate(float shotsPerSecond=1)
        {
            this.shotsPerSecond = shotsPerSecond;
        }

        private FireRate(Multiplier aMultiplier, float shotsPerSecond = 1) : this(shotsPerSecond)
        {
            multiplier = aMultiplier;
        }
        
        private float MultipliedShotsPerSecond => shotsPerSecond * multiplier;
        
        public float Value => (1f / MultipliedShotsPerSecond);

        public float ShootsPerSecond => MultipliedShotsPerSecond;

        public int Milliseconds => (int)(Value * 1000) ;

        public static float FireRateToShootsPerSecond(float fireRate)
        {
            if (fireRate <= 0) return 0;
            return 1 / fireRate; 
        }

        public FireRate GetMultiplier(Multiplier aMultiplier)
        {
            return new FireRate(aMultiplier + multiplier, shotsPerSecond  );
        }

        public FireRate RemoveMultiplier(Multiplier aMultiplier)
        {
            return new FireRate( aMultiplier - multiplier, shotsPerSecond);
        } 
    }
}