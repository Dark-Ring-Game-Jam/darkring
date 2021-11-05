using UnityEngine;

namespace _Scripts
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private PatrolPointsController _patrolPointsController;
		[SerializeField] private NoteView _noteViewPrefab;
		[SerializeField] private Canvas _canvas;

		public static GameManager Instance {get; private set;}

		public Player Player => _player;
		public PatrolPointsController PatrolPointsController => _patrolPointsController;
		public NoteView NoteViewPrefab => _noteViewPrefab;
		public Canvas Canvas => _canvas;

		private void Awake()
		{
			Instance = this;
		}


	}
}