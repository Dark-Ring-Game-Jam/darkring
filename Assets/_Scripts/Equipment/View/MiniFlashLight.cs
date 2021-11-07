﻿using System;
using UnityEngine;

namespace _Scripts
{
	public class MiniFlashLight : MonoBehaviour, IHasId
	{
		public int Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				if (!player.Inventory.ContainItemType(Item.ItemType.Flashlight))
				{
					var flashlight = new Flashlight();
					player.Inventory.AddItem(flashlight);
					Id = GameManager.Instance.GlobalIdCounter++;
					player.PlayPickUpSound();
					gameObject.SetActive(false);
					//Destroy(gameObject);
				}
			}
		}
	}
}