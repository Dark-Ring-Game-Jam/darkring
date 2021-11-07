﻿using System;
using UnityEngine;

namespace _Scripts
{
	public class MiniKey : MonoBehaviour, IHasId
	{
		public Guid Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				var key = new Key();
				player.Inventory.AddItem(key);
				Id = key.Id;
				player.PlayPickUpSound();
				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
		}
	}
}