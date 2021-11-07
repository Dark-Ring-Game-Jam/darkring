using System;
using UnityEngine;

namespace _Scripts
{
	public class MiniBattery : MonoBehaviour, IHasId
	{
		public Guid Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				var battery = new Battery();
				player.Inventory.AddItem(battery);
				Id = battery.Id;
				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
		}
	}
}