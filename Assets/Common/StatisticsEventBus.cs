using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "Statistics", menuName = "EventBusses/Statistics")]
    public class StatisticsEventBus : ScriptableObject
    {
        public Statistics activeStats;
    }
}