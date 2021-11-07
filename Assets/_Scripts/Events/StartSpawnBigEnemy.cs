using UnityEngine;

namespace _Scripts.Events
{
	public class StartSpawnBigEnemy : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player _))
			{
				GameManager.Instance.StartSpawnBigEnemy();
				Destroy(gameObject);
			}
		}
	}
}