using Common;
using UnityEngine;
using UnityEngine.Events;
using ValueObjects;


namespace Weapons
{
    public interface IBulletStatisticsModifier
    {
        public Multiplier Speed { get; }
        public Multiplier Size { get;  }
        public Multiplier Damage { get;  }
    }

    public class BulletStatsModifier: IBulletStatisticsModifier
    {
        public BulletStatsModifier(IShipWeaponStatistic statistic)
        {
            Speed = new Multiplier(statistic.GetWeaponBulletSpeed());
            Size = new Multiplier(statistic.GetWeaponDamage());
            Damage = new Multiplier(statistic.GetWeaponDamage());
        }

        public Multiplier Speed { get; }
        public Multiplier Size { get; }
        public Multiplier Damage { get; }
    }
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
    public class WeaponEventBus : ScriptableObject
    {
        public UnityEvent<FireRate, IBulletStatisticsModifier> ONShot { get; } = new UnityEvent<FireRate, IBulletStatisticsModifier>();
        public UnityEvent<float> ONShotStart { get; } = new UnityEvent<float>();
        
        public StatisticsEventBus statistics;
        
        public void Shoot()
        {
            ONShot?.Invoke(
                    statistics.activeStats.GetFireRate(),
                    new BulletStatsModifier(statistics.activeStats)
            );
        }
        
    }
}