using System;
using UnityEngine;
using UnityEngine.Events;
using ValueObjects;
using Weapons;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IBullet
    {
        // TODO maybe change to invariable
        public class BulletDto
        {
            public Damage Damage;
            public Collider2D Hit;

            public BulletDto SetHit(Collider2D newHit)
            {
                Hit = newHit;
                return this;
            }
        }

        [Serializable]
        public class BulletHitEvent : UnityEvent<BulletDto> {}
        
        private BulletDto ToDto => new BulletDto() { Damage = Damage,  };

        public BulletHitEvent onHit;
        public BulletHitEvent onKeepInContact;
        public BulletHitEvent onExitContact;

        [Header("Statistics")]
        [SerializeField] private float speedBase = 1;
        [SerializeField] private float speedModification = 1;
        private float Speed => speedBase * speedModification;
        
        [SerializeField] private Damage damageBase = new Damage(1);
        [SerializeField] private Multiplier damageModification = new Multiplier(1f);
        private Damage Damage => damageBase.Modificator(damageModification) ;
        
        [Header("Options")]
        [SerializeField]
        private float timeToAutoDestroy = 1f;

        [SerializeField] private bool destroyOnContact = true;
        
        private Rigidbody2D _rigidbody;
        
        protected void Start()
        {
            onHit.AddListener(MakeDamage);
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.up * Speed;
            Destroy(gameObject, timeToAutoDestroy);
        }

        protected void OnTriggerEnter2D(Collider2D hitInfo)
        {
            onHit?.Invoke(ToDto.SetHit(hitInfo));
            
            if (!destroyOnContact) return;
            Destroy(gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            onKeepInContact?.Invoke(ToDto.SetHit(other));
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            onExitContact?.Invoke(ToDto.SetHit(other));
        }

        private static void MakeDamage(BulletDto bulletDto)
        {
            if(bulletDto.Hit == null) return;
            if (!bulletDto.Hit.TryGetComponent(out IDamageable damageable)) return;
            damageable.TakeDamage(bulletDto.Damage);
        }

        public void SetModifiers(float speed, Multiplier damage, float size)
        {
            speedModification = speed;
            damageModification = damage;
            transform.localScale *= size;
        }
    }
}
