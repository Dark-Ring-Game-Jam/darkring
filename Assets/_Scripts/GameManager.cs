using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
	public class GameManager : MonoBehaviour
	{
		private static string _gameIdentifier = "darkring";

		private Vector3 _defaultPlayerSpawnPoint;
		private int _defaultHealthPoints;
		private GameObject[] _levelItems;

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
		public int GlobalIdCounter {get; set;}

		private void Awake()
		{
			Instance = this;
			_defaultPlayerSpawnPoint = _playerSpawnPoint;

			FogTile.FillTheMap(_startPosition, _width, _height, _fogTile, _fogAnchor);
			_deathScreen.SetActive(false);

			if (_levelItems == null)
			{
				_levelItems = GameObject.FindGameObjectsWithTag("equipment");
			}
		}

		private void Start()
		{
			_defaultHealthPoints = _player.HealthPoints;
		}

		public void StartSpawnBigEnemy() => _enemySpawnPointsController.StartSpawnBigEnemy();

		public void AddEnemy(Enemy enemy) => _enemySpawnPointsController.AddEnemy(enemy);

		#region Save/Load/Reset

		public void Save(Vector3 playerSpawnPoint)
		{
			_playerSpawnPoint = playerSpawnPoint;
			SaveGame.Save(_gameIdentifier, new SaveData(playerSpawnPoint, _player.Inventory.ItemList, _player.HealthPoints));
		}

		public void Load()
		{
			_deathScreen.SetActive(false);
			if (_defaultPlayerSpawnPoint == _playerSpawnPoint)
			{
				Reset();
			}
			else
			{
				var data = SaveGame.Load(_gameIdentifier, new SaveData(_playerSpawnPoint, _player.Inventory.ItemList, _player.HealthPoints));
				var inventoryNotes = RestoreItems(data);

				_player.Init();

				Initialize(data, inventoryNotes);

				_enemySpawnPointsController.DestroyAllEnemies();
			}
		}

		private int RestoreItems(SaveData data)
		{
			var itemsToRestore = _player.Inventory.ItemList
				.Where(x => !(x is Note))
				.SelectMany(x => x.Ids).Except(data.Items.SelectMany(y => y.Ids))
				.ToList();
			
			foreach (var itemToRestore in itemsToRestore)
			{
				var item = _levelItems.FirstOrDefault(x =>
					x.gameObject.TryGetComponent<IHasId>(out var itemWithId) && itemWithId.Id.Equals(itemToRestore));
				item?.SetActive(true);
			}

			return _player.Inventory.ItemCount(Item.ItemType.Note);
		}

		public void Reset()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void ShowDeathScreen()
		{
			_deathScreen.SetActive(true);
		}

		private void Initialize(SaveData data, int inventoryNotes)
		{
			var notesDiff = inventoryNotes - _player.Inventory.ItemCount(Item.ItemType.Note);

			_player.SetHealthPoints(data.PlayerHealthPoints);

			_player.Inventory.ItemList.Clear();
			foreach (var savedItem in data.Items)
			{
				_player.Inventory.AddItem(savedItem);
			}

			if (notesDiff > 0)
			{
				_player.Inventory.AddItem(new Item { Type = Item.ItemType.Note, Amount = notesDiff });
			}

			_playerSpawnPoint = data.SpawnPoint;
			_player.transform.position = _playerSpawnPoint;
		}

		#endregion Save/Load/Reset
	}
}