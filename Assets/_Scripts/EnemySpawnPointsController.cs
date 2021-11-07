using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
	public class EnemySpawnPointsController : MonoBehaviour
	{
		[SerializeField] private float _secondsToSpawn;
		[SerializeField] private SpawnPoint[] _littleEnemySpawnPoints;
		[SerializeField] private SpawnPoint[] _bigEnemySpawnPoints;

		private readonly List<Enemy> _enemyList = new List<Enemy>();
		private bool _isCanSpawnBig;

		private void Start()
		{
			StartCoroutine(SpawnEnemy());
		}

		public void StopEnemies() => SetEnemiesActive(true);
		public void RunEnemies() => SetEnemiesActive(false);

		public void StartSpawnBigEnemy()
		{
			_isCanSpawnBig = true;
		}

		private void SetEnemiesActive(bool active)
		{
			foreach (var enemy in _enemyList)
			{
				enemy.SetHidePlayer(active);
			}
		}

		private IEnumerator SpawnEnemy()
		{
			while (true)
			{
				yield return new WaitForSeconds(_secondsToSpawn);

				if (_enemyList.Count > 0)
				{
					continue;
				}

				foreach (var spawnPoint in _littleEnemySpawnPoints)
				{
					SpawnEnemy(spawnPoint);
				}

				if (_isCanSpawnBig)
				{
					foreach (var spawnPoint in _bigEnemySpawnPoints)
					{
						SpawnEnemy(spawnPoint);
					}
				}

			}
		}

		private void SpawnEnemy(SpawnPoint spawnPoint)
		{
			var enemy = spawnPoint.Spawn<Enemy>();

			enemy.OnDie += DeleteEnemy;

			_enemyList.Add(enemy);
		}

		private void DeleteEnemy(Enemy enemy)
		{
			var existEnemy = _enemyList.Find(existEnemy => existEnemy.Equals(enemy));

			_enemyList.Remove(existEnemy);

			existEnemy.OnDie -= DeleteEnemy;
		}
	}
}