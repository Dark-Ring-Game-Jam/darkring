using System;
using System.Collections;
using UnityEngine;

namespace Components
{
    public class SmokeComponent : MonoBehaviour
    {
        [SerializeField] private float _delayToSmoke;
        
        public float DelayToSmoke => _delayToSmoke;
        
        public event Action OnSmoke;
        public bool IsSmoking {get; private set;}
        
        private Coroutine _currentCoroutine;
        
        public bool CanSmoke()
        {
            return true;
        }
        
        public void Smoke()
        {
            if (_currentCoroutine == null)
            {
                IsSmoking = true;
                _currentCoroutine = StartCoroutine(SmokeCoroutine());
                OnSmoke?.Invoke();
            }
        }

        private IEnumerator SmokeCoroutine()
        {
            yield return new WaitForSeconds(_delayToSmoke);

            IsSmoking = false;
            _currentCoroutine = null;
        }
    }
}