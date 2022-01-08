using Bullets;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class LifeEntityChild : MonoBehaviour, IDamageable
    {
        public UnityEvent<float> onDamageTaked;
        
        public void TakeDamage(float damage)
        {
            onDamageTaked?.Invoke(damage);
        }
    }
}
