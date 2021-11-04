using Components;
using UnityEngine;

namespace _Scripts
{
	[RequireComponent(typeof(HealthComponent), typeof(MovementComponent))]
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private MovementComponent _movementComponent;
		[SerializeField] private float _distaneToAttack;
		[SerializeField] private int _damage;

		public HealthComponent HealthComponent => _healthComponent;
		public MovementComponent MovementComponent => _movementComponent;

		private HealthComponent _heroHealthComponent;

		private void Awake()
		{
			_heroHealthComponent = GameObject.FindGameObjectWithTag("hero").GetComponent<HealthComponent>();
		}

		private void Update()
		{
			if (Vector2.Distance(transform.position, _heroHealthComponent.transform.position) <= _distaneToAttack)
			{
				_healthComponent.TakeDamage(_damage);
			}
		}

	}
}