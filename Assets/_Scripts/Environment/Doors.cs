using UnityEngine;

namespace _Scripts
{
	public class Doors : MonoBehaviour
	{
		[SerializeField] private int _needKeysAmount;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player) && player.KeysCount >= _needKeysAmount)
			{
				Destroy(gameObject);
			}
		}
	}
}