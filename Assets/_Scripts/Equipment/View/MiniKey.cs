using UnityEngine;

namespace _Scripts
{
	public class MiniKey : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var key = new Key();
				player.Inventory.AddItem(key);
				Destroy(gameObject);
			}
		}
	}
}