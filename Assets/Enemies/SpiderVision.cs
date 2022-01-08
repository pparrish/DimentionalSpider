using Common;
using UnityEngine;
using UnityEngine.Events;


namespace Enemies
{
    public class SpiderVision : MonoBehaviour
    {
        [SerializeField] private SpiderControlEventBus spiderControlEventBus;
        [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
        
        public Transform player;
        

        public void Start()
        {
            spiderVisionEventBus.player = player;
            spiderVisionEventBus.spider = transform;
            
            spiderVisionEventBus.SetRigidbody();
            spiderControlEventBus.Setup(this, GetComponent<Statistics>());
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform == spiderVisionEventBus.leftLimit)
            {
                spiderVisionEventBus.IsTouchingTheLeftLimit = true;
            }
            if (other.transform == spiderVisionEventBus.rightLimit)
            {
                spiderVisionEventBus.IsTouchingTheRightLimit = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform == spiderVisionEventBus.leftLimit)
            {
                spiderVisionEventBus.IsTouchingTheLeftLimit = false;
            }
            if (other.transform == spiderVisionEventBus.rightLimit)
            {
                spiderVisionEventBus.IsTouchingTheRightLimit = false;
            }
        }

        public UnityEvent<SpiderVisionDto> onCheckDistanceToLimits = new UnityEvent<SpiderVisionDto>();
        public void Update()
        {
            
            onCheckDistanceToLimits.Invoke(new SpiderVisionDto()
            {
                DistanceToLeftLimit = spiderVisionEventBus.DistanceToLeftLimit,
                DistanceToRightLimit = spiderVisionEventBus.DistanceToRightLimit,
                Velocity = spiderVisionEventBus.Velocity.x
            });
        }

        public class SpiderVisionDto
        {
            public float DistanceToLeftLimit { get; set; }
            public float Velocity { get; set; }
            public float DistanceToRightLimit { get; set; }
        }
    }
}