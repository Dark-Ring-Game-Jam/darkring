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

		private bool _isDead = false;		
		
		private void Start()
		{
			Health = _initHealth;
		}

		public void TakeDamage(int amount)
		{
			Health = Mathf.Clamp(Health - amount, 0, _initHealth);

			if (Health == 0 && _isDead == false)
			{
				_isDead = true;
				OnDeath?.Invoke();
			}
		}

		public void SetHealth(int health)
		{
			Health = health;
			_isDead = health == 0;
		}
	}
}
