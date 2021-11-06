using UnityEngine;

namespace _Scripts
{
	public class Candle : IEquipment, IUsable, IEquipInitializable<Candle.InitData>
	{
		public Inventory Inventory { get; private set; }
		
		public readonly struct InitData
		{}

		private Transform _playerTransform;

		public IEquipment Init(InitData initData, Inventory inventory)
		{
			_playerTransform = GameManager.Instance.Player.transform;
			
			Inventory = inventory;
			inventory.AddItem(new Item { Type = Item.ItemType.Candle, Amount = 1 });
			
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
				if (collider2D.TryGetComponent(out CandleStick candleStick))
				{
					candleStick.Active();

					Inventory.RemoveItem(new Item { Type = Item.ItemType.Candle, Amount = 1 });
					
					return true;
				}
			}

			return false;
		}
	}
}