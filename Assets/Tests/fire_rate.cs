using NUnit.Framework;
using ValueObjects;

namespace Tests
{
    public class FireRateTest
    {
        [Test]
        public void getting_point_five_fire_rate_transform_to_two_shots_per_second()
        {
            const float expectedShotsPerSecond = 2f;
            
            const float plainFireRate = 0.5f;
            var resultShotsPerSecond = FireRate.FireRateToShootsPerSecond(plainFireRate);

            Assert.AreEqual(resultShotsPerSecond, expectedShotsPerSecond);
        }

        [Test]
        public void getting_five_fire_rate_transform_to_point_two_shots_per_second()
        {
            const float expectedShotsPerSecond = .2f;
            const float plainFireRate = 5f;
            var resultShotsPerSecond = FireRate.FireRateToShootsPerSecond(plainFireRate);

            Assert.AreEqual(resultShotsPerSecond, expectedShotsPerSecond);
        }
    }
}
