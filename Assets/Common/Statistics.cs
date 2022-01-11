using System.Collections.Generic;
using System.Linq;
using Bullets;
using GameData;
using UnityEngine;
using ValueObjects;
using Weapons;
using Random = UnityEngine.Random;

namespace Common
{
    public class Statistics : MonoBehaviour, ILifeStatistic, IShipMovementStatistics, IShipWeaponStatistic, ICanTakeStatusEffects
    {
        public Life life = new Life(5);
        public Life Life
        {
            get => life;
            set => life = value;
        }
        
        [SerializeField] private float maxVelocity = 1f;
        [SerializeField] private float stopVelocity = 1f;
        [SerializeField] private float acceleration = 1f;
        [SerializeField] private float damageMultiplier = 1f;
        [SerializeField] private float bulletSpeedMultiplier = 1f;
        [SerializeField] private FireRate fireRate = new FireRate(2);
        [SerializeField] private float growFactor = 1;
        // maybe convert to a statistics event bus
        [SerializeField]
        private StatisticsEventBus statisticsEventBus;

        private void Start()
        {
            if (statisticsEventBus != null)
            {
                statisticsEventBus.activeStats = this;
            }
        }

        private void SetStats(Life newLife,
            float newMaxVelocity,
            float newDamage,
            float newBulletSpeed,
            FireRate newFireRate)
        {
            Life = newLife;
            maxVelocity = newMaxVelocity;
            damageMultiplier = newDamage;
            bulletSpeedMultiplier = newBulletSpeed;
            fireRate = newFireRate;
        }

        public void CombineStatistics(Statistics other)
        {
            //For combine a statistic first get the strongestStatistic
            var biggest = other.Life.Total > Life.Total ? other.Life.Total : Life.Total;
            
            //Get the difference between the two statistics
            var difference = Mathf.Abs(other.Life.Total - Life.Total);
            
            //know if are a mutation
            var mutation = Random.value > 0.5f;
            if (mutation)
            {
                Life = new Life(biggest + difference * growFactor);
                return;
            }
            Life = new Life(biggest);
        }

        public void SetStats(Statistics newStatistics)
        {
            SetStats(
                newStatistics.Life,
                newStatistics.GetMaxVelocity(),
                newStatistics.GetWeaponDamage(),
                newStatistics.GetWeaponBulletSpeed(),
                newStatistics.GetFireRate()
                );
        }

        public void SetStats(StatsData newStats)
        {
            SetStats(
                newStats.life,
                newStats.velocity,
                newStats.damage,
                newStats.bulletSpeed,
                newStats.fireRate
                );
        }

        public float GetMaxVelocity()
        {
            return maxVelocity;
        }

        public float GetStopAcceleration()
        {
            return stopVelocity;
        }

        public float GetAcceleration()
        {
            return acceleration;
        }

        public float GetWeaponDamage()
        {
            return damageMultiplier;
        }

        public float GetWeaponBulletSpeed()
        {
            return bulletSpeedMultiplier;
        }

        public FireRate GetFireRate()
        {
            return fireRate;
        }

        private Multiplier _fireRateActiveBoost;

        private void SetFireRateBoost(Multiplier boost)
        {
            fireRate = fireRate.GetMultiplier(boost);
            _fireRateActiveBoost = boost;
        }

        private void RemoveFireRateBoost()
        {
            fireRate = fireRate.RemoveMultiplier(_fireRateActiveBoost);
            _fireRateActiveBoost = new Multiplier(1);
        }
        
        private class StatusChange
        {
            public float Change;
            public float Duration;
            private float _startTime;
            public bool Finished => Time.time - _startTime > Duration;

            public StatusChange()
            {
                Restart();
            }

            public void Restart()
            {
                _startTime = Time.time;
            }
        }

        private readonly Dictionary<string,StatusChange> _listOfStatusChanges = new Dictionary<string, StatusChange>();
        public void TakeStatusEffect(string attribute, float change, float duration)
        {
            if (_listOfStatusChanges.ContainsKey(attribute))
            {
                _listOfStatusChanges[attribute].Restart();
                return;
            }

            maxVelocity *= change;
            _listOfStatusChanges.Add(
                attribute,
                new StatusChange(){ Change = change, Duration = duration }
                );
            
        }
        
        public string TakeStatusEffect(Multiplier change, float duration)
        {
            var id = Time.time + "" + change+ "" + duration;
            //Add fireRate boost per statusEffect
            SetFireRateBoost(change);
            _listOfStatusChanges.Add(
                id,
                new StatusChange(){ Change = change, Duration = duration }
            );
            return id;
        }

        public void RemoveStatusEffect(string statusEffectID)
        {
            if (!_listOfStatusChanges.ContainsKey(statusEffectID)) return;
            RemoveFireRateBoost();// add fireRate boost per statusEffect
            _listOfStatusChanges.Remove(statusEffectID);
        }

        private void Update()
        {
            var toRemove = new List<string>();
            foreach (var statusChange in _listOfStatusChanges.Where(statusChange => statusChange.Value.Finished))
            {
                maxVelocity /= statusChange.Value.Change;
                toRemove.Add(statusChange.Key);
            }

            foreach (var key in toRemove)
            {
                _listOfStatusChanges.Remove(key);
            }
        }

        
    }
}
