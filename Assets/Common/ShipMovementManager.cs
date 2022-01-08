using System;
using UnityEngine;

namespace Common
{
    public class ShipMovementManager : MovementManagerBase
    {
        [SerializeField] [Range(0, 1)] private float trajectoryCorrectionLimit = 0.1f;
        [SerializeField] [Range(0, 1)] private float changeDirectionDetectionLimit = 0.5f;
        private IShipMovementStatistics _movementStatistics;
        private Rigidbody2D _body;
        private Vector2 _controlDirection = Vector2.zero;
        private Vector2 _correctedDirection = Vector2.zero;
        private float _turbo;

        protected override void Start()
        {
            base.Start();
            _body = GetComponent<Rigidbody2D>();
            _movementStatistics = GetComponent<IShipMovementStatistics>();
            onMovement.AddListener(x => _controlDirection = x.Direction);
            onMovementStop.AddListener(x => _controlDirection = Vector2.zero);
            onTurboActivated.AddListener((x) => _turbo = 1f);
            onTurboCanceled.AddListener((x) => _turbo = 0f);
        }

        private new void FixedUpdate()
        {
            base.FixedUpdate();
            if (_controlDirection == Vector2.zero)
            {
                ReduceVelocity();
                return;
            }
            IncreaseVelocity();
        }

        private void IncreaseVelocity()
        {
            var acceleration = _movementStatistics.GetAcceleration();
            var actualVelocity = _body.velocity;
            
            var trajectory = _controlDirection * actualVelocity.magnitude;
            _correctedDirection = trajectory;
            var trajectoryDifference = trajectory - actualVelocity;
            
            if (Math.Abs(trajectoryDifference.x) > trajectory.magnitude * trajectoryCorrectionLimit)
            {
                _correctedDirection.x += trajectoryDifference.x;
            }
            if (Math.Abs(trajectoryDifference.y) > trajectory.magnitude * trajectoryCorrectionLimit)
            {
                _correctedDirection.y += trajectoryDifference.y;
            }
            
            if (Math.Abs(trajectoryDifference.magnitude) > trajectory.magnitude * changeDirectionDetectionLimit && _turbo == 0)
            {
                acceleration += _movementStatistics.GetStopAcceleration();
            }

            if (_turbo > 0) acceleration += _movementStatistics.GetStopAcceleration();

            if (trajectory == Vector2.zero)
            {
                _correctedDirection = _controlDirection;
            }

            _correctedDirection = _correctedDirection.normalized;
            
            _body.velocity = Vector2.ClampMagnitude(actualVelocity
                                                        + (_correctedDirection
                                                           * (acceleration *
                                                              Time.fixedDeltaTime)),
                    _movementStatistics.GetMaxVelocity()
                );
        }
        
        private void ReduceVelocity()
        {
            if(_body == null) return;
            var actualVelocity = _body.velocity;
            
            if (actualVelocity.magnitude <= _movementStatistics.GetAcceleration() * Time.fixedDeltaTime)
            {
                _body.velocity = Vector2.zero;
                return;
            }
            
            var velocityToReduce = actualVelocity.normalized * ((_movementStatistics.GetAcceleration() 
                                                                 + _movementStatistics.GetStopAcceleration()) * Time.fixedDeltaTime);
            _body.velocity = actualVelocity   -  velocityToReduce;
        }

        protected override MovementDto ToDto()
        {
            var dto = base.ToDto();
            dto.Velocity = _body.velocity;
            return dto;
        }
    }
    
}