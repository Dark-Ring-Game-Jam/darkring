using System;
using System.Linq;
using Spine.Unity;
using UnityEngine;

namespace Components
{
	public class EnemyAnimationComponent : AnimationComponent
	{
		private const string IdleAnimationName = "run";
		private const string AttackAnimationName = "attac";
		private const string DieAnimationName = "die";

		private SkeletonAnimation _skeletonAnimation;
		private float _normalizedDurationToDelayAttack;

		public void InitEnemyAnimation(float delayToAttack, SkeletonAnimation skeletonAnimation)
		{
			_skeletonAnimation = skeletonAnimation;
			Init(IdleAnimationName, skeletonAnimation);

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