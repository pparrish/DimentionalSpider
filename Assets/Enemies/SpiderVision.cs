using Common;
using UnityEngine;
using UnityEngine.Events;


namespace Enemies
{
    public class SpiderVision : MonoBehaviour
    {
        [SerializeField] private SpiderControlEventBus spiderControlEventBus;

        public Transform leftLimit;
        public Transform rightLimit;
        public Transform player;
        
        public bool isTouchingTheRightLimit;
        public bool isTouchingTheLeftLimit;
        public float centerRatio;

        private Rigidbody2D _rigidbody;
        
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            spiderControlEventBus.Setup(this, GetComponent<Statistics>());
        }

        private Vector2 DistanceToPoint(Vector2 point)
        {
            var closestPoint = Physics2D.ClosestPoint(point, _rigidbody);
            return point - closestPoint;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform == leftLimit)
            {
                isTouchingTheLeftLimit = true;
            }
            if (other.transform == rightLimit)
            {
                isTouchingTheRightLimit = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform == leftLimit)
            {
                isTouchingTheLeftLimit = false;
            }
            if (other.transform == rightLimit)
            {
                isTouchingTheRightLimit = false;
            }
        }

        public UnityEvent<SpiderVisionDto> onCheckDistanceToLimits = new UnityEvent<SpiderVisionDto>();
        public void Update()
        {
            
            onCheckDistanceToLimits.Invoke(new SpiderVisionDto()
            {
                DistanceToLeftLimit = Mathf.Abs(DistanceToPoint(leftLimit.transform.position).x),
                DistanceToRightLimit = Mathf.Abs(DistanceToPoint(rightLimit.transform.position).x),
                Velocity = _rigidbody.velocity.x
            });
        }

        public class SpiderVisionDto
        {
            public float DistanceToLeftLimit { get; set; }
            public float Velocity { get; set; }
            public float DistanceToRightLimit { get; set; }
        }
        
        public Vector2 DistanceToPlayer => DistanceToPoint(player.position);
        private float ArenaCenter => (rightLimit.position.x - leftLimit.position.x) * 0.5f;
        private float PositionInArena => transform.position.x - leftLimit.position.x;
        public bool IsOnCenter => PositionInArena >= ArenaCenter - centerRatio && PositionInArena <= ArenaCenter + centerRatio;
        public float DistanceToCenter => PositionInArena - ArenaCenter;
    }
}