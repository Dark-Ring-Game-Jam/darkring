using System.Collections.Generic;

namespace _Scripts
{
	public class Battery : IEquipment, IUsable, IEquipInitializable<Battery.InitData>
	{
		public readonly struct InitData
		{
			public readonly IReadOnlyDictionary<IEquipment, int> Items;

			public InitData(IReadOnlyDictionary<IEquipment, int> items)
			{
				Items = items;
			}
		}

		private IReadOnlyDictionary<IEquipment, int> _items;

		public IEquipment Init(InitData initData)
		{
			_items = initData.Items;

			return this;
		}


		public bool TryUse()
		{
			foreach (var equipment in _items.Keys)
			{
				if (equipment is Flashlight { IsCharge: false } flashlight)
				{
					flashlight.IsCharge = true;

					return true;
				}
			}

			return false;
		}
	}
}