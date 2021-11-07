﻿using TMPro;
using UnityEngine;

namespace _Scripts
{
	public class MiniNoteView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				var note = new Note(_text.text);
				player.Inventory.AddItem(note);
				GameManager.Instance.Player.UsableEnvironment = note.NoteView;
				Time.timeScale = 0;
				Destroy(gameObject);
			}
		}
	}
}