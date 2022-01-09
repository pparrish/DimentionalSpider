using UnityEngine;

namespace Common
{
    public class MovementManagerBase : MonoBehaviour
    {
        [SerializeField]
        private ControlEventBus controlEventBus;

        [SerializeField] protected MovementEventBus movementEventBus;
        
        [SerializeField] private float timeToStop = 0.1f;

        private float _startedTimestamp;
        private Vector2 _direction;
        private float _stopTimestamp;

        protected virtual void Start()
        {
            controlEventBus.OnMove.AddListener(Move);
            controlEventBus.OnTurbo.AddListener(Turbo);
        }

        public void Move(Vector2 direction)
        {
            if (_startedTimestamp == 0 && direction != Vector2.zero)
            {
                _startedTimestamp = Time.time;
                _stopTimestamp = 0;
                movementEventBus.onMovementStarted?.Invoke(new MovementDto() {Direction = direction});
            }

            if (_startedTimestamp != 0 && direction == Vector2.zero && _stopTimestamp == 0)
            {
                _stopTimestamp = Time.time;
                return;
            }

            if (direction == Vector2.zero) return;
            _direction = direction;
            _stopTimestamp = 0;
        }

        protected void FixedUpdate()
        {
            
            if(_startedTimestamp == 0) return;
            
            if (_stopTimestamp != 0 && Time.time - _stopTimestamp >= timeToStop)
            {
                _startedTimestamp = 0;
                _stopTimestamp = 0;
                movementEventBus.onMovementStop?.Invoke(ToDto());
                return;
            }
            movementEventBus.onMovement?.Invoke(ToDto());
        }

        private void Turbo(bool turboState)
        {
            if (!turboState)
            {
                movementEventBus.onTurboCanceled?.Invoke(ToDto());
                return;
            }
            movementEventBus.onTurboActivated?.Invoke(ToDto());
        }

        protected virtual MovementDto ToDto()
        {
            return new MovementDto()
            {
                Direction = _direction,
                StartTimestamp = _startedTimestamp,
            };
        }
    }
}
