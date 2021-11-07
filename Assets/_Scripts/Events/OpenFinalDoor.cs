﻿using UnityEngine;

namespace _Scripts.Events
{
	public class OpenFinalDoor : MonoBehaviour
	{
		[SerializeField] private Doors _doors;
		[SerializeField] private Table[] _tables;

		private int _tapeCount;

		private void Start()
		{
			foreach (var table in _tables)
			{
				table.OnUse += AddTape;
			}
		}

		private void AddTape()
		{
			_tapeCount++;

			if (_tapeCount == 5)
			{
				foreach (var table in _tables)
				{
					table.OnUse -= AddTape;
				}

				Destroy(_doors.gameObject);
				Destroy(gameObject);
			}
		}

	}
}