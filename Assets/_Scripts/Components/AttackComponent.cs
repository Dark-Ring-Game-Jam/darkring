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

		private Coroutine _currentCoroutine;

		public void Attack(HealthComponent healthComponent)
		{
			if (_currentCoroutine == null)
			{
				_currentCoroutine = StartCoroutine(AttackCoroutine(healthComponent));
			}
		}

		private IEnumerator AttackCoroutine(HealthComponent healthComponent)
		{
			yield return new WaitForSeconds(_delayToAttack);

			if (Vector2.Distance(transform.position, healthComponent.transform.position) <= _distanceToAttack)
			{
				healthComponent.TakeDamage(_damage);
			}

			_currentCoroutine = null;
		}
	}
}