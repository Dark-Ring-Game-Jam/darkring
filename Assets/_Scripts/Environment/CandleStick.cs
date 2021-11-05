using UnityEngine;

namespace _Scripts
{
	public class CandleStick : MonoBehaviour
	{
		private bool _isActive;

		public void Active()
		{
			_isActive = true;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if ( _isActive && other.TryGetComponent(out Enemy enemy) && enemy is BigEnemy == false)
			{
				enemy.TakeDamage(enemy.Health);
			}
		}
	}
}