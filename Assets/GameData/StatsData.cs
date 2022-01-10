using System;
using ValueObjects;

namespace GameData
{
    [Serializable]
    public class StatsData
    {
        public float life = 3;
        public float velocity = 30;
        public float damage = 1;
        public FireRate fireRate = new FireRate(2);
        public float bulletSpeed = 1;
    }
}