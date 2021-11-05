using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
	public class PatrolPointsController : MonoBehaviour
	{
		[SerializeField] private List<PatrolPoint> _patrolPoints;

		public Transform GetRandomPatrolPoint()
		{
			var randomIndex = Random.Range(0, _patrolPoints.Count);

			return _patrolPoints[randomIndex].transform;
		}
	}
}