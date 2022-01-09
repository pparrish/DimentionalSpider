using Common;
using UnityEngine;


namespace Enemies
{
    public class SpiderVision : MonoBehaviour
    {
        [SerializeField] private SpiderControlEventBus spiderControlEventBus;
        [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
        
        public Transform player;
        
        private void LateUpdate()
        {
            Vector2 playerPosition = spiderVisionEventBus.Player.transform.position;
            spiderVisionEventBus.UpdateDistanceToPlayer(playerPosition - Physics2D.ClosestPoint(playerPosition, _rigidbody));
        }

        private Rigidbody2D _rigidbody;
        public void Start()
        {
            spiderVisionEventBus.spider = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
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
    }
}