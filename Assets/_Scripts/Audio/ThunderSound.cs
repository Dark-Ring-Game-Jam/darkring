using System.Collections;
using _Scripts.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Audio
{
    public class ThunderSound : AudioPlayer
    {
        [field: SerializeField] private AudioClip[] thunderAudioClips;
        [field: SerializeField] private float maxTime = 30f;
        [field: SerializeField] private float minTime = 10f;

        [field: SerializeField] private Window[] windows;

        private float _nextThunderTime = 0f;

        private void OnEnable()
        {
            if (_nextThunderTime == 0f)
            {
                _nextThunderTime = GetNextThunderTime();
            }
            
            StartCoroutine(ThunderCoroutine());
        }

        IEnumerator ThunderCoroutine () {
            while (true)
            {
                yield return new WaitForSeconds(GetNextThunderTime());
                PlayClip(thunderAudioClips[Random.Range(0, thunderAudioClips.Length)]);
                foreach (var window in windows)
                {
                   window.PlayThunderAnimation();
                }
            }
        }
        
        private float GetNextThunderTime()
        {
            return Random.Range(minTime, maxTime);
        }
    }
}