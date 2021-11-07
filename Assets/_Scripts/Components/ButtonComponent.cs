using UnityEngine;

namespace _Scripts
{
	public class ButtonComponent : MonoBehaviour
	{
		[SerializeField] private Button _button;

		private Vector3 _offset = new Vector3(0f, 3f);

		private void Update()
		{
			_button.transform.position = transform.position + _offset;
		}

		public void SetButtonActive(bool active)
		{
			_button.gameObject.SetActive(active);
		}
	}
}