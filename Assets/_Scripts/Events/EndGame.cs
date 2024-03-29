﻿using UnityEngine;

namespace _Scripts
{
	public class EndGame : MonoBehaviour
	{
		[SerializeField] private FinalSlide _finalSlide;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player _))
			{
				GameManager.Instance.Player.SetHealthPoints(0);
				_finalSlide.gameObject.SetActive(true);
			}
		}
	}
}