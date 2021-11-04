using Pathfinding;
using UnityEngine;

namespace Components
{
	public class AiComponent : MonoBehaviour
	{
		[SerializeField] private AIDestinationSetter _aiDestinationSetter;
		[SerializeField] private AIPath _aiPath;

		public void Init(float maxSpeed, Transform targetTransform)
		{
			_aiPath.maxSpeed = maxSpeed;
			_aiDestinationSetter.target = targetTransform;
		}
	}
}