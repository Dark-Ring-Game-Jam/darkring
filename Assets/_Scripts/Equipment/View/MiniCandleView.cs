﻿using System;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
	public class MiniCandleView : MonoBehaviour, IHasId
	{
		public Guid Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				var candle = new Candle();
				player.Inventory.AddItem(candle);
				Id = candle.Ids.First();
				player.PlayPickUpSound();
				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
		}
	}
}