using UnityEngine;

namespace Components
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MovementComponent : MonoBehaviour
	{
		[SerializeField, Range(0.1f, 10f)] private float _speed = 2.5f;
		[SerializeField] private Rigidbody2D _rigidbody2D;

		public void Move(Vector2 normalizeDirection)
		{
			_rigidbody2D.MovePosition(_rigidbody2D.position + normalizeDirection * _speed * Time.fixedDeltaTime);
		}
	}
}