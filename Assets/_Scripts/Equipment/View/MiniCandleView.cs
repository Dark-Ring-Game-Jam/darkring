using UnityEngine;

namespace _Scripts
{
	public class MiniCandleView : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var candle = new Candle();
				player.Inventory.AddItem(candle);
				Destroy(gameObject);
			}
		}
	}
}