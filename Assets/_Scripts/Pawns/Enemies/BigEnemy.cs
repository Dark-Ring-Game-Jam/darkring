using System.Collections;
using UnityEngine;

namespace _Scripts
{
	public class BigEnemy : Enemy
	{
		[SerializeField] private float _increaseSpeedAmount;

		protected override void HandleInit()
		{
			StartCoroutine(IncreaseSpeed());
		}

		public void ResetSpeed()
		{
			_aiComponent.ResetSpeed();
		}

		private IEnumerator IncreaseSpeed()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);

				_aiComponent.IncreaseSpeed(_increaseSpeedAmount);
			}
		}
	}
}