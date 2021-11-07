using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	public class FinalSlide : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private float _step;

		private float _visibility;

		private void Update()
		{
			var imageColor = _image.color;
			_visibility = Mathf.Lerp(_visibility, 1, _step);
			imageColor.a = _visibility;
			_image.color = imageColor;
		}
	}
}