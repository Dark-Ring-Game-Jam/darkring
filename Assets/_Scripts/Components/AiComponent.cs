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
		private Transform _target;
		private Transform _parentTransform;

		public void Init(Player target)
		{
			_parentTransform = transform.parent;
			_target = target.transform;
			_aiPath.maxSpeed = target.Speed * _percentFromTargetSpeed;
			_aiDestinationSetter.target = _target;
		}

		public void Update()
		{
			var position = transform.position;
			var normalized = (position - _aiPath.steeringTarget).normalized;

			_aiPath.enabled = Vector2.Distance(_target.position, position) <= _distanceToAggro;

			_parentTransform.localScale = normalized.x < 0 ? _leftSize : _rightSize;
		}

	}
}