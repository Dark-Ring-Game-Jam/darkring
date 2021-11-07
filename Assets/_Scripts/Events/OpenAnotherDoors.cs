using UnityEngine;

namespace _Scripts.Events
{
	public class OpenAnotherDoors : MonoBehaviour
	{
		[SerializeField] private MiniNoteView _noteView;
		[SerializeField] private Doors[] _doors;

		private void Update()
		{
			if (_noteView == null)
			{
				foreach (var door in _doors)
				{
					Destroy(door.gameObject);
				}

				Destroy(gameObject);
			}
		}
	}
}