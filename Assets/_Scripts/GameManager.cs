using System;
using System.Runtime.CompilerServices;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace _Scripts
{
	public class GameManager : MonoBehaviour
	{
		private static string _gameIdentifier = "darkring";
		
		private Vector3 _defaultPlayerSpawnPoint;
		private int _defaultHealthPoints;

		[Header("Main")]
		[SerializeField] private Player _player;
		[SerializeField] private Vector3 _playerSpawnPoint;

		[Header("AI")]
		[SerializeField] private PatrolPointsController _patrolPointsController;
		[SerializeField] private EnemySpawnPointsController _enemySpawnPointsController;

		[Header("UI")]
		[SerializeField] private NoteView _noteViewPrefab;
		[SerializeField] private Canvas _canvas;
		[SerializeField] private GameObject _deathScreen;

		[Header("Fog")]
		[SerializeField] private Vector2 _startPosition;
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private Transform _fogAnchor;
		[SerializeField] private FogTile _fogTile;

		public static GameManager Instance {get; private set;}

		public Player Player => _player;
		public PatrolPointsController PatrolPointsController => _patrolPointsController;
		public EnemySpawnPointsController EnemySpawnPointsController => _enemySpawnPointsController;
		public NoteView NoteViewPrefab => _noteViewPrefab;
		public Canvas Canvas => _canvas;

		private void Awake()
		{
			Instance = this;
			_defaultPlayerSpawnPoint = _playerSpawnPoint;

			FogTile.FillTheMap(_startPosition, _width, _height, _fogTile, _fogAnchor);
			_deathScreen.SetActive(false);
		}
		
		private void Start()
		{
			_defaultHealthPoints = _player.HealthPoints;
		}

		public void StartSpawnBigEnemy() => _enemySpawnPointsController.StartSpawnBigEnemy();

		#region Save/Load/Reset

		public void Save(Vector3 playerSpawnPoint)
		{
			_playerSpawnPoint = playerSpawnPoint;
			SaveGame.Save(_gameIdentifier, new SaveData(playerSpawnPoint, _player.Inventory.ItemList, _player.HealthPoints));
		}

		public void Load()
		{
			_deathScreen.SetActive(false);

			var data = SaveGame.Load(_gameIdentifier, new SaveData(_playerSpawnPoint, _player.Inventory.ItemList, _player.HealthPoints));
			Initialize(data);
		}

		public void Reset()
		{
			_deathScreen.SetActive(false);
			_player.Inventory.ItemList.Clear();
			_player.SetHealthPoints(_defaultHealthPoints);
			
			// TODO - перезагрузить туман войны

			Save(_defaultPlayerSpawnPoint);
			Load();
		}

		public void ShowDeathScreen()
		{
			_deathScreen.SetActive(true);
		}

		private void Initialize(SaveData data)
		{
			_player.SetHealthPoints(data.PlayerHealthPoints);
			_player.Inventory.ItemList.Clear();
			foreach (var item in data.Items)
			{
				_player.Inventory.AddItem(item);
			}

			_playerSpawnPoint = data.SpawnPoint;
			_player.transform.position = _playerSpawnPoint;
		}

		#endregion Save/Load/Reset
	}
}