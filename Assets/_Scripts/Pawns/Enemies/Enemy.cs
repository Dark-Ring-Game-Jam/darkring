using Components;
using UnityEngine;

namespace _Scripts
{
	[RequireComponent(typeof(HealthComponent), typeof(MovementComponent))]
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private MovementComponent _movementComponent;

		public HealthComponent HealthComponent => _healthComponent;
		public MovementComponent MovementComponent => _movementComponent;
	}
}