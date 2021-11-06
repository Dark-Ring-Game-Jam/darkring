using UnityEngine;

namespace _Scripts
{
	public class Cupboard : MonoBehaviour, IUsable
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player))
			{
				player.UsableEnvironment = this;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.collider.TryGetComponent(out Player player) && player.UsableEnvironment?.Equals(this) == true)
			{
				player.UsableEnvironment = null;
			}
		}

		public void Use(Inventory inventory)
		{
			var player = GameManager.Instance.Player;
			var enemiesController = GameManager.Instance.EnemySpawnPointsController;

			if (player.IsHide)
			{
				enemiesController.StopEnemies();
			}
			else
			{
				enemiesController.RunEnemies();
			}

			player.SetHidePlayer(!player.IsHide);
		}
	}
}