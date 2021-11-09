using System;
using System.Collections.Generic;
using System.Linq;
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
				var usedItemId = inventory.ItemList.First(x => x.Type == Item.ItemType.InsulatingTape).Ids.First();
				GameManager.Instance.UsedItemIds.Add(usedItemId);
				inventory.RemoveItem(new Item { Type = Item.ItemType.InsulatingTape, Amount = 1, Ids = new List<Guid> { usedItemId } });
				OnUse?.Invoke();
			}
			else if (_isActive == false)
			{
				GameManager.Instance.Player.SetText("По-моему, здесь нужна изолента", Color.yellow);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player) && _isActive == false)
			{
				player.UsableEnvironment = this;
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