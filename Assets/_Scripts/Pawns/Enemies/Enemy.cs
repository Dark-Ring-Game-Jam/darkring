using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Spine.Unity;
using UnityEngine;

namespace _Scripts
{
	public class Enemy : MonoBehaviour, ICanBeAttacked
	{
		[Header("Components")]
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private EnemyAnimationComponent _animationComponent;
		[SerializeField] private AttackComponent _attackComponent;
		[SerializeField] protected AiComponent _aiComponent;

		[Header("Animation")]
		[SerializeField] private SkeletonAnimation _skeletonAnimation;

		public int Health => _healthComponent.Health;
		public event Action<Enemy> OnDie;

		private ICanBeAttacked _targetCanBeAttacked;
		private Player _target;
		private Transform _targetTransform;

		protected virtual void HandleInit() {}

		protected void Start()
		{
			_target = GameManager.Instance.Player;
			_targetCanBeAttacked = _target.GetComponent<ICanBeAttacked>();
			_targetTransform = _target.transform;

			_animationComponent.InitEnemyAnimation(_attackComponent.DelayToAttack, _skeletonAnimation);
			_aiComponent.Init(_target);

			_healthComponent.OnDeath += Die;
			_attackComponent.OnAttack += _animationComponent.Attack;

			HandleInit();
		}

		private void Update()
		{
			if (_attackComponent.CanAttack(_targetTransform.position) && _attackComponent.IsAttacking == false)
			{
				if (!_target.GetComponent<HealthComponent>().IsDead)
				{
					var enemy = new Dictionary<ICanBeAttacked, Vector2>
					{
						[_targetCanBeAttacked] = _targetTransform.position
					};

					_attackComponent.Attack(enemy);
				}
			}
			else if (_attackComponent.IsAttacking == false && _healthComponent.Health > 0)
			{
				_animationComponent.Idle();
			}
		}

		public void SetHidePlayer(bool active)
		{
			_aiComponent.IsPlayerHide = active;
		}

		public void TakeDamage(int damage)
		{
			_healthComponent.TakeDamage(damage);
		}

		private void Die()
		{
			StartCoroutine(DeferredDie());
		}

		private IEnumerator DeferredDie()
		{
			_attackComponent.enabled = false;
			_aiComponent.enabled = false;
			_animationComponent.Die();

			OnDie?.Invoke(this);

			_healthComponent.OnDeath -= Die;
			_attackComponent.OnAttack -= _animationComponent.Attack;

			yield return new WaitForSeconds(2f);

			Destroy(gameObject);
		}

	}
}