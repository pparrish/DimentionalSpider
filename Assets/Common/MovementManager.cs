using UnityEngine;

namespace Common
{
    public class MovementManager : MovementManagerBase
    {
        [SerializeField]
        private float turboMultiplier;

        [SerializeField]
        private AnimationCurve accelerationCurve;
        
        private float _turbo = 1;
        private IShipMovementStatistics _movementStatistics;
        private Rigidbody2D _rigidbody;

        protected override void Start()
        {
            base.Start();
            _movementStatistics = GetComponent<IShipMovementStatistics>();
            _rigidbody = GetComponent<Rigidbody2D>();
            onMovement.AddListener(MoveAction);
            onMovementStop.AddListener(StopAction);
            onTurboActivated.AddListener((x) => _turbo = turboMultiplier);
            onTurboCanceled.AddListener(x => _turbo = 1);
        }

        private void MoveAction(MovementDto movementDto)
        {
            var newVelocity = _movementStatistics.GetMaxVelocity() * accelerationCurve.Evaluate(Time.time - movementDto.StartTimestamp) * _turbo;
            _rigidbody.velocity = movementDto.Direction * newVelocity;
        }

        private void StopAction(MovementDto movementDto)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
