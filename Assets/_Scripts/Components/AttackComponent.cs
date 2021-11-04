using System;
using System.Collections;
using UnityEngine;

namespace Components
{
	public class AttackComponent : MonoBehaviour
	{
		[SerializeField] private float _distanceToAttack;
		[SerializeField] private float _delayToAttack;
		[SerializeField] private int _damage;

		public float DistanceToAttack => _distanceToAttack;
		public float DelayToAttack => _delayToAttack;
		public event Action OnAttack;
		public bool IsAttacking {get; private set;}

		private Coroutine _currentCoroutine;

		public bool CanAttack(Vector2 targetPosition)
		{
			return Vector2.Distance(transform.position, targetPosition) <= _distanceToAttack;
		}

		public void Attack(HealthComponent healthComponent)
		{
			if (_currentCoroutine == null)
			{
				IsAttacking = true;
				_currentCoroutine = StartCoroutine(AttackCoroutine(healthComponent));
				OnAttack?.Invoke();
			}
		}

		private IEnumerator AttackCoroutine(HealthComponent healthComponent)
		{
			yield return new WaitForSeconds(_delayToAttack);

			if (CanAttack(healthComponent.transform.position))
			{
				healthComponent.TakeDamage(_damage);
			}

			IsAttacking = false;
			_currentCoroutine = null;
		}
	}
}