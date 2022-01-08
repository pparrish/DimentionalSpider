using AI.Attack;
using AI.Movement;
using Enemies;

namespace AI
{
    public class AttackPatterns
    {
        public SpiderControlEventBus Controller;
        public SpiderVisionEventBus Vision;

        public IAttackPattern FollowAndAttack(int weapon, float duration)
        {
            return new ComposedAttackPattern(Controller, Vision, duration)
                .AddPattern(new AlignWithPlayer(Controller, Vision))
                .AddPattern(new DistanceToPlayer(Controller, Vision))
                .AddPattern(new ConstantAttack(Controller, weapon));
        }
        
        public IAttackPattern SideToSide(int weapon=0)
        {
            return new AttackPatternsSequence(1)
                .AddPattern(
                    new GoToCenter(Controller, Vision)
                )
                .AddPattern(
                    new GoToCorner(Controller, Vision, GoToCorner.Corner.Left)
                )
                .AddPattern(
                    new ComposedAttackPattern(mode: ComposedAttackPattern.Mode.Race)
                        .AddPattern(new GoToCorner(Controller, Vision))
                        .AddPattern(new ConstantAttack(Controller, weapon)
                        )
                );
        }

        public IAttackPattern TrapAndAttack(float duration, int mainWeapon = 0, int secondaryWeapon = 1)
        {
            return new AttackPatternsSequence(1)
                .AddPattern(FollowAndAttack(mainWeapon, duration * 0.3f))
                .AddPattern(FollowAndAttack(secondaryWeapon, duration * 0.2f))
                .AddPattern(FollowAndAttack(mainWeapon, duration * 0.5f));
        }
    }
}