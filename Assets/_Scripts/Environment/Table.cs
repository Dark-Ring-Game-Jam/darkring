using UnityEngine;

namespace _Scripts
{
	public class Table : MonoBehaviour
	{
		public bool IsActive {get; private set;}

		public void Active()
		{
			IsActive = true;
		}
	}
}