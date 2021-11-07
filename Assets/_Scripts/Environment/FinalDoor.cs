using UnityEngine;

namespace _Scripts
{
	public class FinalDoor : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			GameManager.Instance.Player.SetText($"Мне нужно разместить на столах все изоленты и собрать все 3 ключа");
		}
	}
}