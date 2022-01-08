using Enemies;
using UnityEngine;

namespace AI.Movement
{
    public class AlignWithPlayer : DistanceToPlayer
    {
        public AlignWithPlayer(SpiderControlEventBus controller, SpiderVision vision, float duration = float.PositiveInfinity, float acceptableDistance = 0) : base(controller, vision, acceptableDistance, duration)
        {
        }

        public override void Execute()
        {
            if (StartTimestamp == 0) StartTimestamp = Time.time;
            
            var distanceToPlayer = Vision.DistanceToPlayer.x;

            if(Mathf.Abs(distanceToPlayer) <= AcceptableDistance) return;
            
            
            if (distanceToPlayer > 0)
            {
                Controller.Move(Vector2.right);
                return;
            }
            
            Controller.Move(Vector2.left);
        }
    }
}