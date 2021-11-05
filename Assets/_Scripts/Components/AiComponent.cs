using _Scripts;
using Pathfinding;
using UnityEngine;

namespace Components
{
	public class AiComponent : MonoBehaviour
	{
		[SerializeField] private AIDestinationSetter _aiDestinationSetter;
		[SerializeField] private AIPath _aiPath;
		[SerializeField] private float _distanceToAggro;
		[SerializeField] private float _percentFromTargetSpeed;

		private readonly Vector2 _leftSize = new Vector2(-1f, 1f);
		private readonly Vector2 _rightSize = new Vector2(1f, 1f);
		private float _targetSpeed;
		private Transform _target;
		private Transform _parentTransform;
		private bool _isReached = true;

		public void Init(Player target)
		{
			_parentTransform = transform.parent;
			_target = target.transform;
			_targetSpeed = target.Speed;
			_aiPath.maxSpeed = _targetSpeed * _percentFromTargetSpeed;
			_aiDestinationSetter.target = _target;
		}

		private void Update()
		{
			var position = transform.position;
			var normalized = (position - _aiPath.steeringTarget).normalized;
			var isToFar = Vector2.Distance(_target.position, position) > _distanceToAggro;

			if (isToFar && _isReached)
			{
				_aiDestinationSetter.target = GameManager.Instance.PatrolPointsController.GetRandomPatrolPoint();

				_isReached = false;
			}
			else if(isToFar == false)
			{
				_aiDestinationSetter.target = _target;
			}

			if (isToFar && _aiPath.remainingDistance <= 1f)
			{
				_isReached = true;
			}

			_parentTransform.localScale = normalized.x < 0 ? _leftSize : _rightSize;

		}

		public void IncreaseSpeed(float value)
		{
			_aiPath.maxSpeed += _targetSpeed * value;
		}
	}
}