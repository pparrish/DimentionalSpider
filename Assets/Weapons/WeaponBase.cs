using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using ValueObjects;

namespace Weapons
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] private WeaponEventBus weaponEventBus;
        [SerializeField] private WeaponModificator modificator;

        [SerializeField]
        [Range(0, 1)]
        private float shootPerformTime = 0.5f;
        [SerializeField]
        private Transform firePoint;
        [SerializeField]
        private bool shotWithMissingFirePoint;

        [Header("Projectile")]
        [SerializeField]
        private string projectileLayer;
        [SerializeField]
        private GameObject projectile;

        public UnityEvent<float> onShootStart;
        
        private bool _shooting;

        private  void Start()
        {
            weaponEventBus.ONShot.AddListener(ShootAction);
        }

        private bool CanShot => !_shooting &&
                                projectile != null &&
                                !(!shotWithMissingFirePoint && firePoint == null);

        private async void ShootAction(FireRate fireRate, IBulletStatisticsModifier bulletStatisticsModifier)
        {
            if(!CanShot) return;
            _shooting = true;
            
            var finalFireRate = fireRate.GetMultiplier(modificator.fireRate);
            var shotTime  = finalFireRate.Milliseconds;
            var timeToPerformShot = (int) (shotTime * shootPerformTime) ;

            onShootStart?.Invoke(finalFireRate.ShootsPerSecond);
            await Task.Delay(timeToPerformShot);
            
            CreateProjectile(bulletStatisticsModifier);
            
            await Task.Delay(shotTime - timeToPerformShot);
            _shooting = false;
        }
        
        private void CreateProjectile(IBulletStatisticsModifier bulletStatisticsModifier)
        {
            Transform myTransform;
            try
            {
                myTransform = transform;
            }
            catch (Exception)
            {
                return;
            }

            var projectilePosition = firePoint != null ? firePoint.position : myTransform.position;
            var projectileRotation = firePoint != null ? firePoint.rotation : myTransform.rotation;
            var projectileInstance = Instantiate(projectile, projectilePosition, projectileRotation);

            if (projectileLayer != "")
            {
                projectileInstance.layer = LayerMask.NameToLayer(projectileLayer);
            }
            
            //Maybe this must be place in a subclass
            var projectileComponent = projectileInstance.GetComponent<IBullet>();
            projectileComponent?.SetModifiers(
                modificator.speed * bulletStatisticsModifier.Speed,
                modificator.damage * bulletStatisticsModifier.Damage,
                modificator.scale * bulletStatisticsModifier.Size);
        }
    }
}