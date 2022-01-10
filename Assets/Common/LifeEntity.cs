using System;
using Bullets;
using UnityEngine;
using UnityEngine.Events;

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
        private bool Death => _lifeStatistic.GetLife() <= 0;

        [SerializeField] private LifeEntityEventBus lifeEntityEventBus;
        
        private void Start()
        {
            _lifeStatistic = GetComponent<ILifeStatistic>();
            _lifeStatistic.SetLife(_lifeStatistic.GetMaxLife());
        }

        public void TakeDamage(float damage)
        {
            _lifeStatistic.SetLife(_lifeStatistic.GetLife() - damage);
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