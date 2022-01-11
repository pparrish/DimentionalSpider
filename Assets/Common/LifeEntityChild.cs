using Bullets;
using UnityEngine;
using UnityEngine.Events;
using ValueObjects;

namespace Common
{
    public class LifeEntityChild : MonoBehaviour, IDamageable
    {
        public UnityEvent<Damage> onDamageTaked;
        
        public void TakeDamage(Damage damage)
        {
            onDamageTaked?.Invoke(damage);
        }
    }
}
