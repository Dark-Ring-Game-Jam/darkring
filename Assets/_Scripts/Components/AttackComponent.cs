using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

namespace Components
{
	public class AttackComponent : MonoBehaviour
	{
		[SerializeField] private float _distanceToAttack;
		[SerializeField] private float _delayToAttack;
		[SerializeField] private int _damage;

		public float DelayToAttack => _delayToAttack;
		public event Action OnAttack;
		public bool IsAttacking {get; private set;}

		private Coroutine _currentCoroutine;

		public bool CanAttack(Vector2 targetPosition)
		{
			return Vector2.Distance(transform.position, targetPosition) <= _distanceToAttack;
		}

		public void StopAttack()
		{
			StopAllCoroutines();
		}

		public void Attack(Dictionary<ICanBeAttacked, Transform> canBeAttacked)
		{
			if (_currentCoroutine == null)
			{
				IsAttacking = true;
				_currentCoroutine = StartCoroutine(AttackCoroutine(canBeAttacked));
				OnAttack?.Invoke();
			}
		}

		private IEnumerator AttackCoroutine(Dictionary<ICanBeAttacked, Transform> canBeAttacked)
		{
			yield return new WaitForSeconds(_delayToAttack);

			foreach (var pair in canBeAttacked)
			{
				var target = pair.Key;
				var position = pair.Value;

				if (CanAttack(position.position))
				{
					target.TakeDamage(_damage);
				}
			}

			IsAttacking = false;
			_currentCoroutine = null;
		}
	}
}