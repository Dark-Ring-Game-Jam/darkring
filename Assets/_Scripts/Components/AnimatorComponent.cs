using Spine.Unity;
using UnityEngine;

namespace Components
{
	public class AnimatorComponent : MonoBehaviour
	{
		[SerializeField] private SkeletonAnimation _skeletonAnimation;

		private const string DieAnimation = "die";
		private const string AttackAnimation = "attac";
		private const string RunAnimation = "run";

		private float _normalizedDurationToDelayAttack;

		public void Init(float delayToAttack)
		{
			var duration = _skeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration;
			_normalizedDurationToDelayAttack = duration / delayToAttack;
		}

		public void Die()
		{
			_skeletonAnimation.AnimationState.SetAnimation(0, DieAnimation, false);
		}

		public void Attack()
		{
			_skeletonAnimation.AnimationState.SetAnimation(0, AttackAnimation, false);

			_skeletonAnimation.timeScale -= 1 - _normalizedDurationToDelayAttack;
		}

		public void Run()
		{
			_skeletonAnimation.AnimationState.SetAnimation(0, RunAnimation, true);
		}
	}
}