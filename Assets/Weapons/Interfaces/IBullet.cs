using ValueObjects;

namespace Weapons
{
    public interface IBullet
    {
        void SetModifiers(float speed, Multiplier damage, float size);
    }
}