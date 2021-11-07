using UnityEngine;

namespace _Scripts.Events
{
	public class OpenFinalDoor : MonoBehaviour
	{
		[SerializeField] private FinalDoor _finalDoor;
		[SerializeField] private Table[] _tables;
		[SerializeField] private Boss _boss;

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

			if (_tapeCount == 5 && GameManager.Instance.Player.Inventory.ItemCount(Item.ItemType.Key) == 3)
			{
				foreach (var table in _tables)
				{
					table.OnUse -= AddTape;
				}

				_boss.gameObject.SetActive(true);
				GameManager.Instance.Player.PlayDoorsSound();
				Destroy(_finalDoor.gameObject);
				Destroy(gameObject);
			}
		}

	}
}