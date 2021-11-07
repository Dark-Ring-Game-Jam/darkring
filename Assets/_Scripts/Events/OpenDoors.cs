using UnityEngine;

namespace _Scripts.Events
{
	public class OpenDoors : MonoBehaviour
	{
		[SerializeField] private Doors _doors;
		[SerializeField] private SpawnPoint _spawnPoint;
		[SerializeField] private MiniNoteView _noteView;
		[SerializeField] private Smoke _smoke;

		private void Update()
		{
			if (_noteView.gameObject.activeSelf == false)
			{
				var enemy = _spawnPoint.Spawn<BigEnemy>();
				GameManager.Instance.AddEnemy(enemy);
				GameManager.Instance.Player.PlayDoorsSound();
				Destroy(_doors.gameObject);
				Destroy(_smoke.gameObject);
				Destroy(gameObject);
			}
		}
	}
}