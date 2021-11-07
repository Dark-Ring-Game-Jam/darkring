using System;
using System.Linq;
using Spine.Unity;

namespace Components
{
	public class BossAnimationComponent : AnimationComponent
	{
		private const string IdleAnimationName = "inle";
		private const string AttackAnimationName = "attac";
		private const string DieAnimationName = "die";
		private const string AttackMirrorAnimationName = "attac mirror";
		private const string RunAnimationName = "go";

		private SkeletonAnimation _skeletonAnimation;
		private float _normalizedDurationToDelayAttack;
		private float _normalizedDurationToDelayAttackMirror;

		public void InitBossAnimation(float delayToAttack, float delayToMirrorAttack, SkeletonAnimation skeletonAnimation)
		{
			_skeletonAnimation = skeletonAnimation;
			Init(RunAnimationName, skeletonAnimation);

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

			currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(AttackMirrorAnimationName));

			if (currentAnimation != null)
			{
				var duration = _animationsWithDuration[currentAnimation];
				_normalizedDurationToDelayAttackMirror = 1 - duration / delayToMirrorAttack;
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

		public void Run()
		{
			SetAnimationState(RunAnimationName);
		}

		public void Attack()
		{
			var timeScale = _skeletonAnimation.timeScale - _normalizedDurationToDelayAttack;
			SetAnimationState(RunAnimationName);
			SetAnimationState(AttackAnimationName, false, timeScale);
		}

		public void AttackMirror()
		{
			var timeScale = _skeletonAnimation.timeScale - _normalizedDurationToDelayAttackMirror;
			SetAnimationState(RunAnimationName);
			SetAnimationState(AttackMirrorAnimationName, false, timeScale);
		}
	}
}