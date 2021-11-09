using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
	public class PhrasesComponent : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;
		[SerializeField] private float _cooldown;
		[SerializeField] private float _showTextTime;
		[SerializeField] private List<string> _phrases;

		private float _currentTime;
		private float _currentShowTime;
		private Vector3 _offset = new Vector3(0f, 3f);

		private void Awake()
		{
			_currentTime = _cooldown;
			_currentShowTime = _showTextTime;
			_text.text = GetRandomPhrase();
			_text.enabled = false;
		}

		private void Update()
		{
			if (_currentTime <= 0)
			{
				_text.enabled = true;

				if (_currentShowTime > 0)
				{
					_currentShowTime -= Time.deltaTime;
				}
				else
				{
					_currentTime = _cooldown;
					_currentShowTime = _showTextTime;
					_text.text = GetRandomPhrase();
					_text.enabled = false;
				}
			}
			else
			{
				_currentTime -= Time.deltaTime;
			}

			_text.transform.position = transform.position + _offset;
		}

		public void SetText(string text, Color color = default)
		{
			_text.text = text;
			if (color != default)
			{
				_text.color = color;
			}
			else
			{
				_text.color = new Color(1f, 1f, 1f, 1f);
			}
			
			_currentTime = 0;
		}

		private string GetRandomPhrase()
		{
			var number = Random.Range(0, _phrases.Count);
			_text.color = new Color(1f, 1f, 1f, 1f);
			return _phrases[number];
		}
	}
}