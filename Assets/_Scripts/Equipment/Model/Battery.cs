using System.Collections.Generic;

namespace _Scripts
{
	public class Battery : IEquipment, IUsable, IEquipInitializable<Battery.InitData>
	{
		public Inventory Inventory { get; private set; }
		
		public readonly struct InitData
		{
			public readonly IReadOnlyDictionary<IEquipment, int> Items;

			public InitData(IReadOnlyDictionary<IEquipment, int> items)
			{
				Items = items;
			}
		}

		private IReadOnlyDictionary<IEquipment, int> _items;

		public IEquipment Init(InitData initData, Inventory inventory)
		{
			_items = initData.Items;
			
			Inventory = inventory;
			inventory.AddItem(new Item { Type = Item.ItemType.Batteriy, Amount = 1 });

			return this;
		}


		public bool TryUse()
		{
			foreach (var equipment in _items.Keys)
			{
				if (equipment is Flashlight { IsCharge: false } flashlight)
				{
					flashlight.IsCharge = true;

					Inventory.RemoveItem(new Item { Type = Item.ItemType.Batteriy, Amount = 1 });
					
					return true;
				}
			}

			return false;
		}
	}
}