using System;
using UnityEngine;

namespace _Scripts
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private PatrolPointsController _patrolPointsController;

		public static GameManager Instance {get; private set;}

		private void Awake()
		{
			Instance = this;
		}

		public Player Player => _player;
		public PatrolPointsController PatrolPointsController => _patrolPointsController;
	}
}