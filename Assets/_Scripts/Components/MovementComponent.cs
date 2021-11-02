using UnityEngine;

namespace Components
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MovementComponent : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Rigidbody2D _rigidbody2D;

		public void Move(Vector2 normalizeDirection)
		{
			_rigidbody2D.MovePosition(_rigidbody2D.position + normalizeDirection * _speed * Time.deltaTime);
		}
	}
}