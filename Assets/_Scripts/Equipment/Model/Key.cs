namespace _Scripts
{
	public class Key : IEquipment, IUsable, IEquipInitializable<Key.InitData>
	{
		public readonly struct InitData
		{}

		public IEquipment Init(InitData initData)
		{
			return this;
		}

		public bool TryUse()
		{
			return false;
		}

	}
}