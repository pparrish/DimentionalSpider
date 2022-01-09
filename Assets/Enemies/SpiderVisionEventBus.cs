using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    [CreateAssetMenu(fileName = "SpiderVision", menuName = "EventBusses/SpiderVision")]
    public class SpiderVisionEventBus : ScriptableObject
    {
        public Transform leftLimit;
        public Transform rightLimit;
        public Transform spider;
        
        private Rigidbody2D _rigidbody;

        public float centerRatio;

        public void UpdateDistanceToPlayer(Vector2 distance)
        {
            DistanceToPlayer = distance;
        }

        public void SetRigidbody()
        {
            _rigidbody = spider.GetComponent<Rigidbody2D>();
        }

        public Vector2 DistanceToPlayer
        {
            get;
            private set;
        }

        private float DistanceXToLimit(Vector2 limit)
        {
            return Mathf.Abs(limit.x - Physics2D.ClosestPoint(limit, _rigidbody).x);
        }

        public float DistanceToLeftLimit => DistanceXToLimit(leftLimit.transform.position);
        public float DistanceToRightLimit => DistanceXToLimit(rightLimit.transform.position);

        public Vector2 Velocity => _rigidbody.velocity;
        private float ArenaCenter => (rightLimit.position.x - leftLimit.position.x) * 0.5f;
        private float PositionInArena => spider.position.x - leftLimit.position.x;
        
        public bool IsOnCenter => PositionInArena >= ArenaCenter - centerRatio && PositionInArena <= ArenaCenter + centerRatio;
        public float DistanceToCenter => PositionInArena - ArenaCenter;
        
        public bool IsTouchingTheLeftLimit { get; set; }
        public bool IsTouchingTheRightLimit { get; set; }
    }
}