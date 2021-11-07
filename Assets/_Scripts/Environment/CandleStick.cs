using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace _Scripts
{
	public class CandleStick : MonoBehaviour, IUsable
	{
		[SerializeField] private AnimationComponent _animationComponent;
		[SerializeField] private SkeletonAnimation _skeletonAnimation;

		private const string IdleAnimation = "off";
		private const string ActiveIdleAnimation = "on";
		private const string TurnOnAnimation = "turn on";
		private const string TurnOffAnimation = "shutdown";
		private bool _isActive;

		private void Start()
		{
			_animationComponent.Init(IdleAnimation, _skeletonAnimation);
		}

		public void Use(Inventory inventory)
		{
			if (inventory.ContainItemType(Item.ItemType.Candle) && _isActive == false)
			{
				Active();
				inventory.RemoveItem(new Item { Type = Item.ItemType.Candle, Amount = 1 });
			}
			else if (_isActive == false)
			{
				GameManager.Instance.Player.SetText("Мне нужна свечка");
			}
		}

		public void Active()
		{
			StartCoroutine(ActiveCandleStick());
		}

		private IEnumerator ActiveCandleStick()
		{
			_animationComponent.SetAnimationState(TurnOnAnimation, false);

			yield return new WaitForSeconds(.33f);

			_animationComponent.SetAnimationState(ActiveIdleAnimation);
			_isActive = true;
			gameObject.SetActive(false);
			gameObject.SetActive(true);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if ( _isActive && other.TryGetComponent(out Enemy enemy))
			{
				enemy.TakeDamage(enemy.Health);
			}

			if ( _isActive && other.TryGetComponent(out Smoke smoke))
			{
				smoke.Destroy();
			}
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (_isActive == false && other.collider.TryGetComponent(out Player player))
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
	}
}