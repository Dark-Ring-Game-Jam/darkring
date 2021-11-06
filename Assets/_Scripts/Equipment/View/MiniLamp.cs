using UnityEngine;

namespace _Scripts
{
	public class MiniLamp : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var lamp = new Lamp();
				player.Inventory.AddItem(lamp);
				Destroy(gameObject);
			}
		}
	}
}