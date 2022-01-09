using Common;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class SpiderAnimationHandler : MonoBehaviour
    {
        
        [SerializeField] private SpiderVisionEventBus spiderVisionEventBus;
        [SerializeField] private WeaponEventBus mainWeaponEventBus;
        [SerializeField] private WeaponEventBus supportWeaponEventBus;
        [SerializeField] private WeaponEventBus powerWeaponEventBus;
        
        private Animator _animator;
        private static readonly int MainWeapon = Animator.StringToHash("MainWeapon");
        private static readonly int MainWeaponShotDuration = Animator.StringToHash("MainWeaponShotDuration");
        private static readonly int SupportWeapon = Animator.StringToHash("SupportWeapon");
        private static readonly int SupportWeaponShotDuration = Animator.StringToHash("SupportWeaponShotDuration");
        private static readonly int PowerWeapon = Animator.StringToHash("PowerWeapon");
        private static readonly int PowerWeaponShotDuration = Animator.StringToHash("PowerWeaponShotDuration");
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int LeftLimit = Animator.StringToHash("LeftLimit");
        private static readonly int RightLimit = Animator.StringToHash("RightLimit");
        private static readonly int DirectionX = Animator.StringToHash("DirectionX");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            mainWeaponEventBus.ONShotStart.AddListener(MainWeaponAttack);
            supportWeaponEventBus.ONShotStart.AddListener(SupportWeaponAttack);
            powerWeaponEventBus.ONShotStart.AddListener(PowerWeaponAttack);
        }

        private void LateUpdate()
        {
            DistanceToLimits();
        }

        public void MainWeaponAttack(float shotDuration)
        {
            _animator.SetTrigger(MainWeapon);
            _animator.SetFloat(MainWeaponShotDuration, shotDuration);
        }
        
        public void SupportWeaponAttack(float shotDuration)
        {
            _animator.SetTrigger(SupportWeapon);
            _animator.SetFloat(SupportWeaponShotDuration, shotDuration);
        }
        
        public void PowerWeaponAttack(float shotDuration)
        {
            _animator.SetTrigger(PowerWeapon);
            _animator.SetFloat(PowerWeaponShotDuration, shotDuration);
        }

        public void OnMove(MovementDto movementDto)
        {
            Move(movementDto.Velocity, movementDto.Direction);
        }

        public void OnStop(MovementDto movementDto)
        {
            Move(Vector2.zero, Vector2.zero);
        }

        private void Move(Vector2 velocity, Vector2 direction)
        {
            _animator.SetFloat(DirectionX, direction.x);
            _animator.SetFloat(VelocityX, 1 + Mathf.Abs(velocity.x));
        }

        [SerializeField]
        private float acceptableDistanceToAnimateLimits;
        
        public void DistanceToLimits()
        {
            if (spiderVisionEventBus.DistanceToLeftLimit < acceptableDistanceToAnimateLimits &&  spiderVisionEventBus.Velocity.x < 0f  )
            {
                _animator.SetFloat(LeftLimit, 1 + (1/ spiderVisionEventBus.DistanceToLeftLimit)  );
            }
            if (spiderVisionEventBus.DistanceToLeftLimit >= acceptableDistanceToAnimateLimits && spiderVisionEventBus.Velocity.x > 0f)
            {
                _animator.SetFloat(LeftLimit, -1);
            }
            
            if (spiderVisionEventBus.DistanceToRightLimit < acceptableDistanceToAnimateLimits &&  spiderVisionEventBus.Velocity.x > 0f  )
            {
                _animator.SetFloat(RightLimit, 1 + (1/ spiderVisionEventBus.DistanceToRightLimit) );
            }
            
            if (spiderVisionEventBus.DistanceToRightLimit >= acceptableDistanceToAnimateLimits && spiderVisionEventBus.Velocity.x < 0f)
            {
                _animator.SetFloat(RightLimit, -1);
            }
        }
    }
}