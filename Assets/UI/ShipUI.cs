using Common;
using UnityEngine;
using UnityEngine.UI;
using Weapons;


namespace UI
{
    public class ShipUI : MonoBehaviour
    {
        [SerializeField] private Image lifeFill;
        [SerializeField] private Image velocityFill;
        [SerializeField] private Image damageFill;
        [SerializeField] private Image fireRateFill;
        [SerializeField] private Image bulletSpeedFill;
        [SerializeField] private float maxLife = 1;
        [SerializeField] private float maxVelocity = 1;
        [SerializeField] private float maxDamage = 1;
        [SerializeField] private float maxFireRate = 5;
        [SerializeField] private float maxBulletSpeed = 1;
        private ILifeStatistic _life;
        private IShipWeaponStatistic _weapon;
        private IShipMovementStatistics _movementStatistics;
        // Start is called before the first frame update
        void Start()
        {
            SetStatsFills();
        }

        public void SetStatsFills()
        {
            _life = GetComponent<ILifeStatistic>();
            lifeFill.fillAmount = _life.GetMaxLife()/maxLife;
            _weapon = GetComponent<IShipWeaponStatistic>();
            damageFill.fillAmount = _weapon.GetWeaponDamage() / maxDamage;
            fireRateFill.fillAmount =  _weapon.GetFireRate().ShootsPerSecond / maxFireRate;
            bulletSpeedFill.fillAmount = _weapon.GetWeaponBulletSpeed() / maxBulletSpeed;
            _movementStatistics = GetComponent<IShipMovementStatistics>();
            velocityFill.fillAmount = _movementStatistics.GetMaxVelocity() / maxVelocity;
        }
    }
}
