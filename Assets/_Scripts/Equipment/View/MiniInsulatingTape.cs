using System;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
	public class MiniInsulatingTape : MonoBehaviour, IHasId
	{
		public Guid Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				var insulatingTape = new InsulatingTape();
				player.Inventory.AddItem(insulatingTape);
				Id = insulatingTape.Ids.First();
				player.PlayPickUpSound();
				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
		}
	}
}