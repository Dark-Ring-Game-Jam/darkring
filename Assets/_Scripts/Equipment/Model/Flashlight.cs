using UnityEngine;

namespace _Scripts
{
	public class Flashlight : Item, IUsable
	{
		private readonly Player _player;

		public Flashlight()
		{
			_player = GameManager.Instance.Player;
			Type = ItemType.Flashlight;
			Amount = 1;
		}

		public void Use()
		{
			var results = new Collider2D[3];
			var size = Physics2D.OverlapBoxNonAlloc((Vector2)_player.transform.position + _player.NormalizedDirection, new Vector2(1f, 5f), 0f, results, LayerMask.NameToLayer("Enemy"));

			if (size <= 0)
			{
				return;
			}

			foreach (var collider2D in results)
			{
				if (collider2D.TryGetComponent(out Enemy enemy) && _player.Inventory.ContainItemType(ItemType.Batteriy))
				{
					if (enemy is BigEnemy bigEnemy)
					{
						bigEnemy.ResetSpeed();
					}
					else
					{
						enemy.TakeDamage(1);
					}

					_player.Inventory.RemoveItem(new Item { Type = ItemType.Batteriy, Amount = 1 });
				}
			}
		}
		public void Use(Inventory inventory)
		{
			throw new System.NotImplementedException();
		}
	}
}