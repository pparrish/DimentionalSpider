using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    [CreateAssetMenu(fileName = "Statistics", menuName = "EventBusses/Statistics")]
    public class StatisticsEventBus : ScriptableObject
    {
        public Statistics activeStats;
        public UnityEvent OnBoostAnimationFinish { get; } = new UnityEvent();
        public UnityEvent OnSetBoost { get; } = new UnityEvent();
    }
}