using UnityEngine;
using ValueObjects;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponModificator", menuName = "ScriptableObjects/WeaponModificator")]
    public class WeaponModificator : ScriptableObject
    {
        [Header("Weapon")]
        [SerializeField]
        public Multiplier fireRate = new Multiplier();
        [Header("Projectile")]
        [SerializeField]
        public Multiplier damage = new Multiplier();
        [SerializeField]
        public Multiplier scale = new Multiplier();
        [SerializeField]
        public Multiplier speed = new Multiplier();
    }
}