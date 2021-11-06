using UnityEngine;

namespace _Scripts
{
	public class Note : Item
	{
		public NoteView NoteView { get; }
		
		public Note(string text)
		{
			if (GameManager.Instance.Player.UsableEnvironment is NoteView openedNoteView)
			{
				openedNoteView.Use(GameManager.Instance.Player.Inventory);
			}
			
			var noteView = Object.Instantiate(GameManager.Instance.NoteViewPrefab, GameManager.Instance.Canvas.transform).GetComponent<NoteView>();

			noteView.Init(text);
			Type = ItemType.Note;
			Amount = 1;

			NoteView = noteView;
		}
	}
}