using UnityEngine;

namespace _Scripts.Events
{
	public class StartSpawnBigEnemy : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			GameManager.Instance.StartSpawnBigEnemy();
			Destroy(gameObject);
		}
	}
}