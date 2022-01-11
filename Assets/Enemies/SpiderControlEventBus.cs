using AI;
using AI.Boost;
using Common;
using UnityEngine;
using ValueObjects;
using Weapons;

namespace Enemies
{
    [CreateAssetMenu(fileName = "PlayerControl", menuName = "EventBusses/SpiderControl")]
    public class SpiderControlEventBus : ControlEventBus
    {
        //Weapon
        [SerializeField] private WeaponEventBus supportWeaponEventBus;
        protected bool SupportShootActive;
        
        [SerializeField] private WeaponEventBus powerWeaponEventBus;
        protected bool PowerShootActive;
        
        //vision
        [SerializeField] private SpiderVisionEventBus SpiderVisionEventBus;

        // AI
        private readonly AttackPatternsSequence _attackPatterns = new AttackPatternsSequence();

        private SpiderVision vision;
        private Statistics statistics;
        
        public void Setup(SpiderVision aVision, Statistics aStatistics)
        {
            LoopEvent += SendMovementCommand;
            vision = aVision;
            statistics = aStatistics;
            
            var fireBoost = new Boost( aStatistics, new Multiplier(2f));
            var fireBoostRemover = fireBoost.GetStatusEffectRemoverPattern();
            
            var patterns = new AttackPatterns() {Controller = this, Vision = SpiderVisionEventBus};
            _attackPatterns
                .AddPattern(patterns.FollowAndAttack(0, 5f))
                .AddPattern(patterns.FollowAndAttack(1, 5f))
                .AddPattern(fireBoost)
                .AddPattern(patterns.SideToSide())
                .AddPattern(fireBoostRemover)
                .AddPattern(patterns.TrapAndAttack(5f, 2));

            LoopEvent += () =>
            {
                if( vision && statistics && SpiderVisionEventBus) _attackPatterns.Execute();
            };
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            LoopEvent += () =>
            {
                if (SupportShootActive) supportWeaponEventBus.Shoot();
            };
            LoopEvent += () =>
            {
                if (PowerShootActive) powerWeaponEventBus.Shoot();
            };
        }
        

        public void Shoot(int weapon)
        {
            switch (weapon)
            {
                default:
                    weaponEventBus.Shoot();
                    //ShootActive = true;
                    return;
                case 1:
                    supportWeaponEventBus.Shoot();
                    //SupportShootActive = true;
                    return;
                case 2:
                    powerWeaponEventBus.Shoot();
                    //PowerShootActive = true;
                    return;
            }
        }
        
        // CONTROL MOVEMENT
        private bool _directionUpdated; 
        private Vector2 _lastDirectionUpdated = Vector2.zero;
        

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                _lastDirectionUpdated = direction;
                _directionUpdated = true;
                return;
            }
            _lastDirectionUpdated = (_lastDirectionUpdated + direction).normalized;
            _directionUpdated = true;
        }
        
        public void SendMovementCommand()
        {
            if (_directionUpdated)
            {
                OnMove.Invoke(_lastDirectionUpdated);
                _directionUpdated = false;
            }

            if (_directionUpdated || _lastDirectionUpdated == Vector2.zero) return;
            
            _lastDirectionUpdated = Vector2.zero;
            OnMove.Invoke(_lastDirectionUpdated);
        }
        
    }
}