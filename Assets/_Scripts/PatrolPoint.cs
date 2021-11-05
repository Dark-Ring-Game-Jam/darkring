using UnityEngine;

namespace _Scripts
{
	public class PatrolPoint : MonoBehaviour
	{
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position, Vector3.one);
		}
	}
}