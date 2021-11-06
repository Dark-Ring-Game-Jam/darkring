using UnityEngine;

namespace _Scripts
{
	public class Mirror : MonoBehaviour, IUsable
	{
		[SerializeField] private CandleStick _candleStick;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player))
			{
				player.UsableEnvironment = this;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player) && player.UsableEnvironment.Equals(this))
			{
				player.UsableEnvironment = null;
			}
		}

		public void Use(Inventory inventory)
		{
			if ( inventory.ContainItemType(Item.ItemType.KeroseneLamp))
			{
				_candleStick.Active();
			}
		}
	}
}