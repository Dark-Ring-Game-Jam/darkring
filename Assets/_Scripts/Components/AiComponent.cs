using _Scripts;
using _Scripts.Audio;
using Pathfinding;
using UnityEngine;

namespace Components
{
	public class AiComponent : MonoBehaviour
	{
		[SerializeField] private AIDestinationSetter _aiDestinationSetter;
		[SerializeField] private AIPath _aiPath;
		[SerializeField] private Seeker _seeker;
		[SerializeField] private float _distanceToAggro;
		[SerializeField] private float _percentFromTargetSpeed;

		public bool IsPlayerHide {get; set;}

		private readonly Vector2 _leftSize = new Vector2(-1f, 1f);
		private readonly Vector2 _rightSize = new Vector2(1f, 1f);
		private float _targetSpeed;
		private Transform _target;
		private Transform _parentTransform;
		private bool _isReached = true;
		private CharacterSounds _characterSounds;
		private float _maxSpeed;

		public void Init(Player target)
		{
			_parentTransform = transform.parent;
			_target = target.transform;
			_targetSpeed = target.Speed;
			_maxSpeed = _targetSpeed * 1.5f;
			_aiPath.maxSpeed = _targetSpeed * _percentFromTargetSpeed;
			_aiDestinationSetter.target = _target;

			_characterSounds = GetComponent<CharacterSounds>();
		}

		public void SetActiveComponent(bool active)
		{
			_aiPath.enabled = active;
			_aiDestinationSetter.enabled = active;
			_seeker.enabled = active;
		}

		private void Update()
		{
			var position = transform.position;
			var normalized = (position - _aiPath.steeringTarget).normalized;
			var isToFar = Vector2.Distance(_target.position, position) > _distanceToAggro;

			if ((isToFar || IsPlayerHide) && _isReached)
			{
				_aiDestinationSetter.target = GameManager.Instance.PatrolPointsController.GetRandomPatrolPoint();

				_isReached = false;
			}
			else if(isToFar == false && IsPlayerHide == false)
			{
				_aiDestinationSetter.target = _target;
			}

			if ((isToFar || IsPlayerHide) && _aiPath.remainingDistance <= 1f)
			{
				_isReached = true;
			}

			_parentTransform.localScale = normalized.x < 0 ? _leftSize : _rightSize;

			if (_aiPath.velocity != Vector3.zero && _characterSounds != null)
			{
				_characterSounds.PlayStepSound();
			}
		}

		public void IncreaseSpeed(float value)
		{
			if (_aiPath.maxSpeed >= _maxSpeed)
			{
				_aiPath.maxSpeed = _maxSpeed;
			}
			else
			{
				_aiPath.maxSpeed += _targetSpeed * value;
			}

		}

		public void ResetSpeed()
		{
			_aiPath.maxSpeed = _targetSpeed * _percentFromTargetSpeed;
		}
	}
}