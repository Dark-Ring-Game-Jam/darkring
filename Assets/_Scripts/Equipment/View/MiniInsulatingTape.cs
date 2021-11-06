using UnityEngine;

namespace _Scripts
{
	public class MiniInsulatingTape : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var insulatingTape = new InsulatingTape();
				player.Inventory.AddItem(insulatingTape);
				Destroy(gameObject);
			}
		}
	}
}