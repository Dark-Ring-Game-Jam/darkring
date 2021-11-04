using Components;
using UnityEngine;

namespace _Scripts
{
	[RequireComponent(typeof(HealthComponent), typeof(MovementComponent))]
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private Collider2D _collider2D;
		[Header("Components")]
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private MovementComponent _movementComponent;
		[SerializeField] private AnimatorComponent _animatorComponent;
		[SerializeField] private AttackComponent _attackComponent;

		public HealthComponent HealthComponent => _healthComponent;
		public MovementComponent MovementComponent => _movementComponent;

		private HealthComponent _heroHealthComponent;

		private void Awake()
		{
			//_heroHealthComponent = GameObject.FindGameObjectWithTag("hero").GetComponent<HealthComponent>();
			_healthComponent.OnDeath += Die;
			_animatorComponent.Init(_attackComponent.DelayToAttack);
		}

		private void Update()
		{
			/*if (Vector2.Distance(transform.position, _heroHealthComponent.transform.position) <= _attackComponent.DistanceToAttack)
			{
				_attackComponent.Attack(_heroHealthComponent);
			}*/
		}

		private void Die()
		{
			_animatorComponent.Die();
			_collider2D.enabled = false;
			_healthComponent.OnDeath -= Die;
		}
	}
}