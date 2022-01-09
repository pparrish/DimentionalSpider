using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    [CreateAssetMenu(fileName = "Movement", menuName = "EventBusses/Movement")]
    public class MovementEventBus : ScriptableObject
    {
        public UnityEvent<MovementDto> onMovementStarted;
        public UnityEvent<MovementDto> onMovement;
        public UnityEvent<MovementDto> onMovementStop;
        public UnityEvent<MovementDto> onTurboActivated;
        public UnityEvent<MovementDto> onTurboCanceled;
    }
}