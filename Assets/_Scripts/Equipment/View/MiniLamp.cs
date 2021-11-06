using UnityEngine;

namespace _Scripts
{
	public class MiniLamp : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				if (!player.Inventory.ContainItemType(Item.ItemType.KeroseneLamp))
				{
					var lamp = new Lamp();
					player.Inventory.AddItem(lamp);
					Destroy(gameObject);
				}
			}
		}
	}
}