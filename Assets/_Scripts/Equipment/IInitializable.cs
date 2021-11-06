namespace _Scripts
{
	public interface IEquipInitializable<in TInitData>
		where TInitData : struct
	{
		IEquipment Init(TInitData initData, Inventory inventory);
	}
}