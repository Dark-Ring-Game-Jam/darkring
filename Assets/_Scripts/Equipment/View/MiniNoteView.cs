using TMPro;
using UnityEngine;

namespace _Scripts
{
	public class MiniNoteView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		public string Text => _text.text;
	}
}