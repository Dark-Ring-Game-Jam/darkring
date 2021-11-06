﻿using UnityEngine;

namespace _Scripts
{
	public class MiniFlashLight : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var flashlight = new Flashlight();
				player.Inventory.AddItem(flashlight);
				Destroy(gameObject);
			}
		}
	}
}