using UnityEngine;

namespace _Scripts
{
	public class SpawnPoint : MonoBehaviour
	{
		public T Spawn<T>(T pawn)
		where T : Object
		{
			return Instantiate(pawn, transform.position, Quaternion.identity);
		}
	}
}