using UnityEngine;

namespace _Scripts
{
	public class EnemySpawnPointsController : MonoBehaviour
	{
		[SerializeField] private SpawnPoint[] _enemySpawnPoints;

		public void SpawnEnemy(int spawnPointIndex)
		{
			_enemySpawnPoints[spawnPointIndex].Spawn<Enemy>();
		}
	}
}