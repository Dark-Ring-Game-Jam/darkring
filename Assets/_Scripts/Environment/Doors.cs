using UnityEngine;

namespace _Scripts
{
	public class Doors : MonoBehaviour, IUsable
	{
		[SerializeField] private int _needKeysAmount;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player))
			{
				player.UsableEnvironment = this;
				player.Inventory.RemoveItem(new Item { Type = Item.ItemType.InsulatingTape, Amount = _needKeysAmount });
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player) && player.UsableEnvironment?.Equals(this) == true)
			{
				player.UsableEnvironment = null;
			}
		}

		public void Use(Inventory inventory)
		{
			if ( inventory.ItemCount(Item.ItemType.Key) >= _needKeysAmount)
			{
				Destroy(gameObject);
			}
		}
	}
}