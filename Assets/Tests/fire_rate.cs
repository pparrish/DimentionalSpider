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

    public class MultiplierTests
    {
        [Test]
        public void add_a_multiplier_of_0p5_to_a_multiplier_of_1_is_0p5()
        {
            var a = new Multiplier();
            var b = new Multiplier(0.5f);
            var c = a + b;
            
            Assert.AreEqual(c.Value,new Multiplier(0.5f).Value);
        }
    }
}
