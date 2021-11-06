namespace _Scripts
{
	public class Key : IEquipment, IUsable, IEquipInitializable<Key.InitData>
	{
		public Inventory Inventory { get; private set; }
		
		public readonly struct InitData
		{}

		public IEquipment Init(InitData initData, Inventory inventory)
		{
			Inventory = inventory;
			inventory.AddItem(new Item { Type = Item.ItemType.Key, Amount = 1 });
			
			return this;
		}

		public bool TryUse()
		{
			// TODO - помечтить в ветку, где возвращает true
			//Inventory.RemoveItem(new Item { Type = Item.ItemType.Key, Amount = 1 });
			
			return false;
		}

	}
}