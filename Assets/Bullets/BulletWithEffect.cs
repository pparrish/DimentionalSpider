using UnityEngine;

namespace Bullets
{
    //TODO This class can be standalone from bullet class
    public class BulletWithEffect : Bullet
    {
        [Header("StatusEffect")]
        [SerializeField] private float duration;
        [SerializeField] private float effectModifier = 1;
        private Vector2 _previousVelocity = Vector2.negativeInfinity;

        private new void Start()
        {
            base.Start();
            onHit.AddListener((bulletDto => ChangeDirectionToFollowObjective(bulletDto.Hit.transform)));
            onKeepInContact.AddListener( (bulletDto) => ApplyStatusEffects(bulletDto.Hit.gameObject) );
            onExitContact.AddListener((bulletDto => RestoreOriginalDirection()));
        }
        
        private void RestoreOriginalDirection()
        {
            if(_previousVelocity == Vector2.negativeInfinity) return;
            var rb = GetComponent<Rigidbody2D>();
            rb.velocity = _previousVelocity;
            _previousVelocity = Vector2.negativeInfinity;
        }

        private void ChangeDirectionToFollowObjective(Transform objective)
        {
            if (objective.transform.parent != null && objective.transform.parent.name == "Limits") return;
            var rb = GetComponent<Rigidbody2D>();
            var velocity = rb.velocity;
            _previousVelocity = velocity;
            velocity = ( objective.position - transform.position ).normalized * velocity.magnitude *0.5f  ;
            rb.velocity = velocity;
        }

        private void ApplyStatusEffects(GameObject toApply)
        {
            var canTakeStatusEffects = toApply.GetComponent<ICanTakeStatusEffects>();
            canTakeStatusEffects?.TakeStatusEffect(
                "movement",
                effectModifier,
                duration
            );
        }
    }
}
