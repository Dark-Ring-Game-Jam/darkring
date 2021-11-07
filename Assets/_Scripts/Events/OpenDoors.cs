using UnityEngine;

namespace _Scripts.Events
{
	public class OpenDoors : MonoBehaviour
	{
		[SerializeField] private Doors _doors;
		[SerializeField] private SpawnPoint _spawnPoint;
		[SerializeField] private MiniNoteView _noteView;

		private void Update()
		{
			if (_noteView == null)
			{
				var enemy = _spawnPoint.Spawn<BigEnemy>();
				GameManager.Instance.AddEnemy(enemy);
				Destroy(_doors.gameObject);
				Destroy(gameObject);
			}
		}
	}
}