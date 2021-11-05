using UnityEngine;

namespace _Scripts
{
	public class Flashlight : IEquipment, IUsable, IEquipInitializable<Flashlight.InitData>
	{
		public readonly struct InitData
		{
			public readonly float FlashDistance;

			public InitData(float flashDistance)
			{
				FlashDistance = flashDistance;
			}
		}
		public bool IsCharge {get; set;}

		private float _flashDistance;
		private Transform _playerTransform;


		public IEquipment Init(InitData initData)
		{
			_flashDistance = initData.FlashDistance;
			_playerTransform = GameManager.Instance.Player.transform;

			return this;
		}

		public bool TryUse()
		{
			var results = new RaycastHit2D[1];
			var size = Physics2D.RaycastNonAlloc(_playerTransform.position, _playerTransform.forward, results, _flashDistance, LayerMask.NameToLayer("Enemy"));

			if (size <= 0)
			{
				return false;
			}

			foreach (var raycastHit2D in results)
			{
				if (raycastHit2D.collider.TryGetComponent(out Enemy enemy) && IsCharge)
				{
					if (enemy is BigEnemy bigEnemy)
					{
						bigEnemy.ResetSpeed();
					}
					else
					{
						enemy.TakeDamage(1);
					}

					IsCharge = false;
				}
			}

			return false;
		}
	}
}