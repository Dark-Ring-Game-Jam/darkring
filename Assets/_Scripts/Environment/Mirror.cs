﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
	public class Mirror : MonoBehaviour, IUsable
	{
		[SerializeField] private CandleStick _candleStick;

		private bool _isActive;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (_isActive == false && other.collider.TryGetComponent(out Player player))
			{
				player.UsableEnvironment = this;
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
			if (inventory.ContainItemType(Item.ItemType.KeroseneLamp)  && _isActive == false)
			{
				_isActive = true;
				_candleStick.Active();
				var usedItemId = inventory.ItemList.First(x => x.Type == Item.ItemType.KeroseneLamp).Ids.First();
				GameManager.Instance.UsedItemIds.Add(usedItemId);
				inventory.RemoveItem(new Item { Type = Item.ItemType.KeroseneLamp, Amount = 1, Ids = new List<Guid> { usedItemId } });
			}
			else if (_isActive == false)
			{
				GameManager.Instance.Player.SetText("Для это мне нужна керосиновая лампа", Color.yellow);
			}
		}
	}
}