using AI;
using UnityEngine;

namespace Enemies
{
    public class GoToCorner : IAttackPattern
    {
        private bool _limitTouched;
        private readonly SpiderControlEventBus _controller;
        private readonly SpiderVision _vision;
        private readonly Corner _corner;

        public enum Corner
        {
            Right,
            Left
        }
        
        public GoToCorner(SpiderControlEventBus controller, SpiderVision vision, Corner corner = Corner.Right )
        {
            _controller = controller;
            _vision = vision;
            _corner = corner;
        }
        
        public bool End()
        {
            return _limitTouched;
        }

        public void Reset()
        {
            _limitTouched = false;
        }

        public void Execute()
        {
            if(_limitTouched) return;
            if (_corner == Corner.Right)
            {
                if (_vision.isTouchingTheRightLimit)
                {
                    _limitTouched = true;
                }

                _controller.Move(Vector2.right);
            }

            if (_corner == Corner.Left)
            {
                if (_vision.isTouchingTheLeftLimit)
                {
                    _limitTouched = true;
                }
                _controller.Move(Vector2.left);
            }
        }
    }
}