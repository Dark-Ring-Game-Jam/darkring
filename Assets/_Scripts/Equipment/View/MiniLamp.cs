using System;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
	public class MiniLamp : MonoBehaviour, IHasId
	{
		public int Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				if (gameObject.activeSelf && !player.Inventory.ContainItemType(Item.ItemType.KeroseneLamp))
				{
					var lamp = new Lamp();
					player.Inventory.AddItem(lamp);
					Id = lamp.Ids.First();
					player.PlayPickUpSound();
					gameObject.SetActive(false);
					//Destroy(gameObject);
				}
			}
		}
	}
}