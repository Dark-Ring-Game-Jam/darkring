using UnityEngine;

namespace _Scripts
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private PatrolPointsController _patrolPointsController;
		[SerializeField] private NoteView _noteViewPrefab;
		[SerializeField] private Canvas _canvas;

		[Header("Fog")] 
		[SerializeField] private Vector2 _startPosition;
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private Transform _fogAnchor;

		public static GameManager Instance {get; private set;}

		public Player Player => _player;
		public PatrolPointsController PatrolPointsController => _patrolPointsController;
		public NoteView NoteViewPrefab => _noteViewPrefab;
		public Canvas Canvas => _canvas;

		private void Awake()
		{
			Instance = this;

			FogTile.FillTheMap(_startPosition, _width, _height, _fogAnchor);
		}
	}
}