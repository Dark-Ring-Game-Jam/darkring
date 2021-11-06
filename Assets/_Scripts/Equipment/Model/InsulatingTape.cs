using UnityEngine;

namespace _Scripts
{
	public class InsulatingTape : Item, IUsable
	{
		private Player _player;

		public InsulatingTape()
		{
			_player = GameManager.Instance.Player;
			Type = ItemType.InsulatingTape;
			Amount = 1;
		}

		public void Use()
		{
			var results = new Collider2D[2];
			var size = Physics2D.OverlapBoxNonAlloc(_player.transform.position + _player.transform.up, Vector2.one, 0f, results, LayerMask.NameToLayer("Environment"));

			if (size <= 0)
			{
			}

			foreach (var collider2D in results)
			{
				if (collider2D.TryGetComponent(out Table table) && table.IsActive == false)
				{
					table.Active();

					_player.Inventory.RemoveItem(new Item { Type = ItemType.InsulatingTape, Amount = 1 });
				}
			}
		}
		public void Use(Inventory inventory)
		{
			throw new System.NotImplementedException();
		}
	}
}