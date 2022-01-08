using Enemies;
using UnityEngine;

namespace AI.Movement
{
    public class DistanceToPlayer : TimeOutPattern
    {
        protected readonly float AcceptableDistance;
        
        public DistanceToPlayer(SpiderControlEventBus controller, SpiderVisionEventBus vision,
            float acceptableDistance = 10f, float duration = float.PositiveInfinity) : base(controller, vision, duration)
        {
            AcceptableDistance = acceptableDistance;
        }
        
        public override void Execute()
        {
            base.Execute();

            var distanceToPlayer = Vision.DistanceToPlayer.y;
            var absoluteDistanceToPlayer = Mathf.Abs(distanceToPlayer);

            var playerIsDown = distanceToPlayer < 0;
            var playerIsUp = distanceToPlayer > 0;
            var isFar = absoluteDistanceToPlayer > AcceptableDistance;

            if (playerIsDown && isFar)
            {
                Controller.Move(Vector2.down);
                return;
            }
            
            if (playerIsDown || playerIsUp)
            {
                Controller.Move(Vector2.up);
            }
        }
    }
}