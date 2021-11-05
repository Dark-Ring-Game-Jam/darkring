using Components;
using UnityEngine;

namespace _Scripts
{
	public class Enemy : MonoBehaviour, ICanBeAttacked
	{
		[Header("Components")]
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private EnemyAnimationComponent _animationComponent;
		[SerializeField] private AttackComponent _attackComponent;
		[SerializeField] private AiComponent _aiComponent;

		private ICanBeAttacked _targetCanBeAttacked;
		private Player _target;
		private Transform _targetTransform;

		private void Start()
		{
			_target = GameObject.FindGameObjectWithTag("hero").GetComponent<Player>();
			_targetCanBeAttacked = _target.GetComponent<ICanBeAttacked>();
			_targetTransform = _target.transform;

			_animationComponent.InitEnemyAnimation(_attackComponent.DelayToAttack);
			_aiComponent.Init(_target);

			_healthComponent.OnDeath += Die;
			_attackComponent.OnAttack += _animationComponent.Attack;
		}

		private void Update()
		{
			if (_attackComponent.CanAttack(_targetTransform.position) && _attackComponent.IsAttacking == false)
			{
				_attackComponent.Attack(_targetCanBeAttacked, _targetTransform.position);
			}
			else if (_attackComponent.IsAttacking == false)
			{
				_animationComponent.Idle();
			}
		}

		public void TakeDamage(int damage)
		{
			_healthComponent.TakeDamage(damage);
		}

		private void Die()
		{
			_animationComponent.Die();
			_healthComponent.OnDeath -= Die;
			_attackComponent.OnAttack -= _animationComponent.Attack;
			Destroy(this);
		}

	}
}