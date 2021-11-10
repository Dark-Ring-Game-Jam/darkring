using UnityEngine;

namespace _Scripts.Events
{
	public class OpenFinalDoor : MonoBehaviour
	{
		public bool CanOpen;
		
		[SerializeField] private Table[] _tables;
		
		private int _tapeCount;

		private void Start()
		{
			foreach (var table in _tables)
			{
				table.OnUse += AddTape;
			}
		}

		private void AddTape()
		{
			_tapeCount++;

			if (_tapeCount == 5)
			{
				GameManager.Instance.Player.SetText("Похоже, что все изоленты на месте", Color.green);

				foreach (var table in _tables)
				{
					table.OnUse -= AddTape;
				}

				CanOpen = true;
			}
			else
			{
				GameManager.Instance.Player.SetText("Я должен использовать все 6 изолент", Color.yellow);
			}
		}
	}
}