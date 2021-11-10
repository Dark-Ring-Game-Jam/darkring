using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Events;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
		[SerializeField] private OpenFinalDoor _openFinalDoor;

		[Header("AI")]
		[SerializeField] private PatrolPointsController _patrolPointsController;
		[SerializeField] private EnemySpawnPointsController _enemySpawnPointsController;

		[Header("UI")]
		[SerializeField] private NoteView _noteViewPrefab;
		[SerializeField] private Canvas _canvas;
		[SerializeField] private GameObject _menuScreen;
		[SerializeField] private GameObject _deathTitle;
		[SerializeField] private GameObject _menuTitle;

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
		public bool MenuIsActive => _menuScreen.gameObject.activeSelf;
		
		public List<Guid> UsedItemIds { get; private set; }

		private void Awake()
		{
			Instance = this;
			UsedItemIds = new List<Guid>();
			_defaultPlayerSpawnPoint = _playerSpawnPoint;

			FogTile.FillTheMap(_startPosition, _width, _height, _fogTile, _fogAnchor);
			HideMenuScreen();

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
			HideMenuScreen();
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
				.Except(UsedItemIds)
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

		public void Exit()
		{
			Application.Quit();
		}

		public void ShowMenuScreen(bool isMenu = false)
		{
			_deathTitle.SetActive(!isMenu);
			_menuTitle.SetActive(isMenu);

			_menuScreen.SetActive(true);
		}
		
		public void HideMenuScreen()
		{
			_menuScreen.SetActive(false);
		}

		private void Initialize(SaveData data, int inventoryNotes)
		{
			_player.SetHealthPoints(data.PlayerHealthPoints);

			_player.Inventory.ItemList.Clear();
			foreach (var savedItem in data.Items)
			{
				if ((savedItem.Type == Item.ItemType.InsulatingTape || savedItem.Type == Item.ItemType.Key) && _openFinalDoor.CanOpen)
				{
					continue;
				}
				
				_player.Inventory.AddItem(savedItem);
			}

			var notesDiff = inventoryNotes - _player.Inventory.ItemCount(Item.ItemType.Note);
			if (notesDiff > 0)
			{
				_player.Inventory.AddItem(new Item { Type = Item.ItemType.Note, Amount = notesDiff });
			}

			_playerSpawnPoint = data.SpawnPoint;
			_player.transform.position = _playerSpawnPoint;
			// TODO - тут не переносится освещение, рассеивающее туман войны вместе с ГГ
		}

		#endregion Save/Load/Reset
	}
}