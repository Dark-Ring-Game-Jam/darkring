using TMPro;
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
				player.Inventory.AddItem(new Note(_text.text));
				Destroy(gameObject);
			}
		}
	}
}