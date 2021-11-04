using System;
using UnityEngine;

namespace Components
{
	public class HealthComponent : MonoBehaviour
	{
		[SerializeField] private int _initHealth;

		public int Health {get; private set;}
		public bool IsDead => Health == 0;
		public event Action OnDeath;

		private void Start()
		{
			Health = _initHealth;
		}

		public void TakeDamage(int amount)
		{
			Health = Mathf.Clamp(Health - amount, 0, _initHealth);

			if (Health == 0)
			{
				OnDeath?.Invoke();
			}
		}
	}
}
