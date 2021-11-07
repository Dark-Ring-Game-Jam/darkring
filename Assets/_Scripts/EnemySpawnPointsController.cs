using System;
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
		private bool _isCanSpawnBig = false;
		private bool _isStartSpawn;

		private void Start()
		{
			_isStartSpawn = true;
			StartCoroutine(SpawnEnemy());
		}

		public void StopEnemies() => SetEnemiesActive(true);
		public void RunEnemies() => SetEnemiesActive(false);

		public void StartSpawnBigEnemy()
		{
			_isCanSpawnBig = true;
		}

		public void AddEnemy(Enemy enemy)
		{
			enemy.OnDie += DeleteEnemy;
			_enemyList.Add(enemy);
		}

		public void DestroyAllEnemies()
		{
			foreach (var enemy in _enemyList)
			{
				if (enemy != null)
				{
					Destroy(enemy.gameObject);
				}
			}

			_enemyList.Clear();
		}

		private void SetEnemiesActive(bool active)
		{
			foreach (var enemy in _enemyList)
			{
				enemy.SetHidePlayer(active);
			}
		}

		private void Update()
		{
			if (_enemyList.Count <= 0 && _isStartSpawn == false)
			{
				_isStartSpawn = true;
				StartCoroutine(SpawnEnemy());
			}
		}

		private IEnumerator SpawnEnemy()
		{
			yield return new WaitForSeconds(_secondsToSpawn);

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
			_isStartSpawn = false;
		}

		private void SpawnEnemy(SpawnPoint spawnPoint)
		{
			var enemy = spawnPoint.Spawn<Enemy>();

			enemy.SetHidePlayer(GameManager.Instance.Player.IsHide);
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