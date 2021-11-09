using UnityEngine;

namespace _Scripts
{
	public class FinalDoor : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			GameManager.Instance.Player.SetText("Сначала надо разместить на столах все изоленты и собрать все 3 ключа", Color.yellow);
		}
	}
}