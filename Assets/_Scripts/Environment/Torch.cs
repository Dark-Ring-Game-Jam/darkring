using UnityEngine;

namespace _Scripts
{
	public class Torch : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _collider2D;

		private Transform _playerTransform;

		private void Start()
		{
			_playerTransform = GameManager.Instance.Player.transform;
		}

		private void Update()
		{
			_collider2D.enabled = Vector2.Distance(_playerTransform.position, transform.position) <= 3f;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Enemy enemy) && enemy is BigEnemy == false)
			{
				enemy.TakeDamage(enemy.Health);
			}
		}
	}
}
