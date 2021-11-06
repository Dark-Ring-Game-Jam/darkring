using _Scripts;
using UnityEngine;

public class Torch : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Enemy enemy) && enemy is BigEnemy == false)
		{
			enemy.TakeDamage(enemy.Health);
		}
	}
}
