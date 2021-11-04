using Components;
using UnityEngine;

namespace _Scripts
{
	public class Enemy : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private MovementComponent _movementComponent;
		[SerializeField] private EnemyAnimationComponent _animationComponent;
		[SerializeField] private AttackComponent _attackComponent;
		[SerializeField] private AiComponent _aiComponent;

		public HealthComponent HealthComponent => _healthComponent;
		public MovementComponent MovementComponent => _movementComponent;

		private HealthComponent _heroHealthComponent;

		private void Start()
		{
			_heroHealthComponent = GameObject.FindGameObjectWithTag("hero").GetComponent<HealthComponent>();

			_animationComponent.InitEnemyAnimation(_attackComponent.DelayToAttack);
			_aiComponent.Init(_movementComponent.Speed, _heroHealthComponent.transform);

			_healthComponent.OnDeath += Die;
			_attackComponent.OnAttack += _animationComponent.Attack;
		}

		private void Update()
		{
			if (_attackComponent.CanAttack(_heroHealthComponent.transform.position) && _attackComponent.IsAttacking == false)
			{
				_attackComponent.Attack(_heroHealthComponent);
			}
			else if (_attackComponent.IsAttacking == false)
			{
				_animationComponent.Idle();
			}
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