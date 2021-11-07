using System;
using UnityEngine;

namespace _Scripts
{
	public class Table : MonoBehaviour, IUsable
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;

		public event Action OnUse;

		private bool _isActive;

		public void Use(Inventory inventory)
		{
			if (_isActive == false && inventory.ContainItemType(Item.ItemType.InsulatingTape))
			{
				_spriteRenderer.enabled = true;
				_isActive = true;
				inventory.RemoveItem(new Item { Type = Item.ItemType.InsulatingTape, Amount = 1 });
				OnUse?.Invoke();
			}
			else
			{
				GameManager.Instance.Player.SetText("Мне нужна изолента");
			}
			else
			{
				GameManager.Instance.Player.SetText("Мне нужна изолента");
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				player.UsableEnvironment = this;
			}

			if ( _isActive && other.TryGetComponent(out Enemy enemy) && enemy is BigEnemy == false)
			{
				enemy.TakeDamage(enemy.Health);
			}

			if ( _isActive && other.TryGetComponent(out Smoke smoke))
			{
				smoke.Destroy();
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player) && player.UsableEnvironment?.Equals(this) == true)
			{
				player.UsableEnvironment = null;
			}
		}
	}
}