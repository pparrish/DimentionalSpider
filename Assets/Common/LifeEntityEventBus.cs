using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    [CreateAssetMenu(fileName = "Life", menuName = "EventBusses/LifeEntity")]
    public class LifeEntityEventBus : ScriptableObject
    { 
        public UnityEvent onDeath = new UnityEvent();
    }
}