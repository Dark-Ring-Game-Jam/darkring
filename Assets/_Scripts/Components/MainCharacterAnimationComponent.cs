using System;
using System.Linq;

namespace Components
{
    public class MainCharacterAnimationComponent : AnimationComponent
    {
        private const string IdleAnimationName = "idle";
        private const string AttackAnimationName = "attack";
        private const string DieAnimationName = "die";

        private float _normalizedDurationToDelayAttack;

        public void InitMainCharacterAnimation(float delayToAttack)
        {
            Init(IdleAnimationName);

            var currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(AttackAnimationName));

            if (currentAnimation != null)
            {
                var duration = _animationsWithDuration[currentAnimation];
                _normalizedDurationToDelayAttack = 1 - duration / delayToAttack;
            }
            else
            {
                throw new NullReferenceException("Has not " + nameof(currentAnimation));
            }
        }

        public void Die()
        {
            SetAnimationState(DieAnimationName, false);
        }

        public void Idle()
        {
            SetAnimationState(IdleAnimationName);
        }

        public void Attack()
        {
            var timeScale = _skeletonAnimation.timeScale - _normalizedDurationToDelayAttack;
            SetAnimationState(IdleAnimationName);
            SetAnimationState(AttackAnimationName, false, timeScale);
        }
    }
}