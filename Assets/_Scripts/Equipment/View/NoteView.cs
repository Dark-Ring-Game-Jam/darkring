using TMPro;
using UnityEngine;

namespace _Scripts
{
	public class NoteView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		public void Init(string text)
		{
			_text.text = text;
		}
	}
}