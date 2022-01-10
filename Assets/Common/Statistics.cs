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
        [SerializeField] private float maxLife = 1f;
        [SerializeField] private float life = 1f;
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

        private void SetStats(float newMaxLife,
            float newMaxVelocity,
            float newDamage,
            float newBulletSpeed,
            FireRate newFireRate)
        {
            maxLife = newMaxLife;
            maxVelocity = newMaxVelocity;
            damageMultiplier = newDamage;
            bulletSpeedMultiplier = newBulletSpeed;
            fireRate = newFireRate;
        }

        public void CombineStatistics(Statistics other)
        {
            //For combine a statistic first get the strongestStatistic
            var biggest = other.maxLife > maxLife ? other.maxLife : maxLife;
            //Get the difference between the two statistics
            var difference = Mathf.Abs(other.maxLife - maxLife);
            //know if are a mutation
            var mutation = Random.value > 0.5f;
            if (mutation)
            {
                maxLife = biggest + difference * growFactor;
                return;
            }
            maxLife = biggest;
        }

        public void SetStats(Statistics newStatistics)
        {
            SetStats(
                newStatistics.GetMaxLife(),
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

        public float GetMaxLife()
        {
            return maxLife;
        }

        public float SetLife(float newLife)
        {
            life = newLife;
            return life;
        }

        public float GetLife()
        {
            return life;
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
            _fireRateActiveBoost = new Multiplier(0);
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
