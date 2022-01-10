using ValueObjects;

namespace Weapons
{
    public interface IShipWeaponStatistic
    {
        float GetWeaponBulletSpeed();
        float GetWeaponDamage();
        FireRate GetFireRate();
    }
}