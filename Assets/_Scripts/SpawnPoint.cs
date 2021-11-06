using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts
{
	public class SpawnPoint : MonoBehaviour
	{
		[SerializeField] private GameObject _prefab;
		public T Spawn<T>()
		where T : Object
		{
			return Instantiate(_prefab, transform.position, Quaternion.identity).GetComponent<T>();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, transform.localScale);
		}
		
		public Vector3 GetPosition()
		{
			return transform.position;
		}
	}
}