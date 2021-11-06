using UnityEngine;

namespace _Scripts
{
	public class Note : IEquipment
	{
		public Inventory Inventory { get; private set; }
		
		public readonly struct InitData
		{
			public readonly string Text;

			public InitData(string text)
			{
				Text = text;
			}
		}

		public IEquipment Init(InitData initData, Inventory inventory)
		{
			var text = initData.Text;

			var noteView = Object.Instantiate(GameManager.Instance.NoteViewPrefab, GameManager.Instance.Canvas.transform).GetComponent<NoteView>();

			noteView.Init(text);

			Inventory = inventory;
			inventory.AddItem(new Item { Type = Item.ItemType.Note, Amount = 1 });
			
			return this;
		}
	}
}