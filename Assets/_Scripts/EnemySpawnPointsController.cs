using UnityEngine;

namespace _Scripts
{
	public class EnemySpawnPointsController : MonoBehaviour
	{
		[SerializeField] private SpawnPoint[] _enemySpawnPoints;
		[SerializeField] private GameObject _enemyPrefab;

		public void Spawn(int spawnPointIndex)
		{
			_enemySpawnPoints[spawnPointIndex].Spawn(_enemyPrefab);
		}
	}
}