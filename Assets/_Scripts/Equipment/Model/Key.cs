namespace _Scripts
{
	public class Key :Item
	{
		public Key()
		{
			GameManager.Instance.Player.Inventory.AddItem(new Item { Type = ItemType.Key, Amount = 1 });
		}
	}
}