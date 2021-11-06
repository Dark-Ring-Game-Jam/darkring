using UnityEngine;

namespace _Scripts
{
	public class MiniBattery : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var battery = new Battery();
				player.Inventory.AddItem(battery);
				Destroy(gameObject);
			}
		}
	}
}