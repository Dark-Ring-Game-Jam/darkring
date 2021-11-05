using UnityEngine;

namespace _Scripts
{
	public class InsulatingTape : IEquipment, IUsable, IEquipInitializable<InsulatingTape.InitData>
	{
		public readonly struct InitData
		{}

		private Transform _playerTransform;

		public IEquipment Init(InitData initData)
		{
			_playerTransform = GameManager.Instance.transform;

			return this;
		}

		public bool TryUse()
		{
			var results = new Collider2D[2];
			var size = Physics2D.OverlapBoxNonAlloc(_playerTransform.position + _playerTransform.forward, Vector2.one, 0f, results, LayerMask.NameToLayer("Environment"));

			if (size <= 0)
			{
				return false;
			}

			foreach (var collider2D in results)
			{
				if (collider2D.TryGetComponent(out Table table) && table.IsActive == false)
				{
					table.Active();

					return true;
				}
			}

			return false;
		}
	}
}