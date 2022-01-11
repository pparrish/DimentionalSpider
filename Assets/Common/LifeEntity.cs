using System;
using Bullets;
using UnityEngine;
using UnityEngine.Events;
using ValueObjects;

namespace Common
{
    [Serializable]
    public class LifeChangeEvent: UnityEvent<ILifeStatistic>
    {
    }

    public class LifeEntity : MonoBehaviour, IDamageable
    {
        private ILifeStatistic _lifeStatistic;
        public LifeChangeEvent lifeChange;
        private bool Death => _lifeStatistic.Life.Actual == 0;

        [SerializeField] private LifeEntityEventBus lifeEntityEventBus;
        
        private void Start()
        {
            _lifeStatistic = GetComponent<ILifeStatistic>();
            //TODO: Reset the life for now, but later this must be removed
            _lifeStatistic.Life = new Life(_lifeStatistic.Life.Total);
        }

        public void TakeDamage(Damage damage)
        {
            _lifeStatistic.Life = _lifeStatistic.Life.Damage(damage);
            lifeChange.Invoke(_lifeStatistic);
            if (!Death) return;
            DestroyMe();
        }

        private void DestroyMe()
        {
            if(lifeEntityEventBus)
                lifeEntityEventBus.onDeath?.Invoke();
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o, 0.05f);
        }
    }
}