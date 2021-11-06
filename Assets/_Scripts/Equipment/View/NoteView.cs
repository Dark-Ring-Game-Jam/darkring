using TMPro;
using UnityEngine;

namespace _Scripts
{
	public class NoteView : MonoBehaviour, IUsable
	{
		[SerializeField] private TMP_Text _text;

		public void Init(string text)
		{
			_text.text = text;
		}

		public void Use(Inventory inventory)
		{
			// TODO - закрыть NoteView
			// Destroy(this); // так не работает
		}
	}
}