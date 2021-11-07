using System.Collections;
using System.Collections.Generic;
using Components;
using Pathfinding;
using Spine.Unity;
using UnityEngine;

namespace _Scripts
{
	public class Boss : MonoBehaviour
	{
		[SerializeField] private float _abilityCooldown;
		[SerializeField] private float _abilityDelay;
		[SerializeField] private float _delayToDie;
		[SerializeField] private Vector2 _abilityDistance;
		[SerializeField] private AIDestinationSetter _destinationSetter;
		[SerializeField] private BossAnimationComponent _bossAnimationComponent;
		[SerializeField] private AttackComponent _attackComponent;
		[SerializeField] private SkeletonAnimation _skeletonAnimation;
		[SerializeField] private List<BossMirror> _bossMirrors;
		[SerializeField] private FinalDoor _finalDoor;

		private readonly Vector2 _leftSize = new Vector2(-1f, 1f);
		private readonly Vector2 _rightSize = new Vector2(1f, 1f);

		private ICanBeAttacked _targetCanBeAttacked;
		private float _currentAbilityCooldown;
		private Player _player;
		private bool _isUseAbility;

		private void Start()
		{
			_player = GameManager.Instance.Player;
			_destinationSetter.target = _player.transform;
			_targetCanBeAttacked = _player.GetComponent<ICanBeAttacked>();

			_currentAbilityCooldown = _abilityCooldown;

			_bossAnimationComponent.InitBossAnimation(_attackComponent.DelayToAttack, _abilityDelay, _skeletonAnimation);
			_attackComponent.OnAttack += _bossAnimationComponent.Attack;
			gameObject.SetActive(false);
		}

		private void Update()
		{
			var position = transform.position;
			var normalized = (position - _player.transform.position).normalized;
			transform.localScale = normalized.x < 0 ? _leftSize : _rightSize;

			if (_currentAbilityCooldown <= 0)
			{
				_currentAbilityCooldown = _abilityCooldown;

				StartCoroutine(UseAbility());
				_isUseAbility = true;
			}
			else
			{
				_currentAbilityCooldown -= Time.deltaTime;
			}

			if (_attackComponent.CanAttack(_player.transform.position) && _attackComponent.IsAttacking == false && _player.IsHide == false)
			{
				if (!_player.GetComponent<HealthComponent>().IsDead)
				{
					var enemy = new Dictionary<ICanBeAttacked, Transform>
					{
						[_targetCanBeAttacked] = _player.transform
					};

					_attackComponent.Attack(enemy);
				}
			}
			else if (_attackComponent.IsAttacking == false && _isUseAbility == false)
			{
				_bossAnimationComponent.Run();
			}
		}

		private IEnumerator UseAbility()
		{
			_bossAnimationComponent.AttackMirror();

			yield return new WaitForSeconds(_abilityDelay);

			var results = new Collider2D[5];
			var size = Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + new Vector2(Mathf.Sign(-transform.localScale.x) * 1.5f, 1f), _abilityDistance, 0f, results, LayerMask.NameToLayer("Enemy"));

			if (size > 0)
			{
				foreach (var collider2d in results)
				{
					if (collider2d != null && collider2d.TryGetComponent(out BossMirror bossMirror))
					{
						Destroy(bossMirror.gameObject);
						_bossMirrors.Remove(bossMirror);

						if (_bossMirrors.Count <= 0)
						{
							StartCoroutine(DeferredDie());
						}
					}
				}
			}

			_isUseAbility = false;
		}

		private IEnumerator DeferredDie()
		{
			_bossAnimationComponent.Die();
			_attackComponent.enabled = false;
			_destinationSetter.enabled = false;

			yield return new WaitForSeconds(_delayToDie);

			Destroy(_finalDoor);

			Destroy(gameObject);
		}
	}
}