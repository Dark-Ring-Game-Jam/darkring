using System.Linq;
using _Scripts.Events;
using UnityEngine;

namespace _Scripts
{
	public class FinalDoor : MonoBehaviour
	{
		[SerializeField] private OpenFinalDoor _openFinalDoor;
		[SerializeField] private Boss _boss;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (GameManager.Instance.Player.Inventory.ItemCount(Item.ItemType.Key) == 3 && _openFinalDoor.CanOpen)
			{
				var keys = GameManager.Instance.Player.Inventory.ItemList.FindAll(x => x.Type == Item.ItemType.Key);
				GameManager.Instance.UsedItemIds.AddRange(keys.SelectMany(x => x.Ids).Distinct().ToList());
				GameManager.Instance.Player.Inventory.RemoveItem(new Item { Type = Item.ItemType.Key, Amount = 3, Ids = keys.SelectMany(x => x.Ids).Distinct().ToList() });

				_boss.gameObject.SetActive(true);
				GameManager.Instance.Player.PlayDoorsSound();
				Destroy(_openFinalDoor.gameObject);
				Destroy(gameObject);
			}
			else
			{
				GameManager.Instance.Player.SetText("Сначала надо разместить на столах все изоленты и собрать все 3 ключа", Color.yellow);
			}
		}
	}
}