using Enemies;
using UnityEngine;

namespace AI.Movement
{
    public class GoToCenter : IAttackPattern
    {
        private readonly SpiderControlEventBus _controller;
        private readonly SpiderVisionEventBus _vision;
        private bool _isOnCenter;
        
        public GoToCenter(SpiderControlEventBus controller, SpiderVisionEventBus vision )
        {
            _controller = controller;
            _vision = vision;
        }
        
        public void Execute()
        {
            if(_isOnCenter) return;
            
            if (_vision.IsOnCenter)
            {
                _isOnCenter = true;
            }
            
            if (_vision.DistanceToCenter < 0)
            {
                _controller.Move(Vector2.right);
            }

            if (_vision.DistanceToCenter > 0)
            {
                _controller.Move(Vector2.left);
            }
        }

        public bool End()
        {
            return _isOnCenter;
        }

        public void Reset()
        {
            _isOnCenter = false;
        }
    }
}