using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
	public class MiniNoteView : MonoBehaviour, IHasId
	{
		[SerializeField] private TMP_Text _text;
		
		public Guid Id { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gameObject.activeSelf && other.TryGetComponent(out Player player))
			{
				var note = new Note(_text.text);
				player.Inventory.AddItem(note);
				GameManager.Instance.Player.UsableEnvironment = note.NoteView;
				Time.timeScale = 0;
				Id = note.Id;
				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
		}
	}
}