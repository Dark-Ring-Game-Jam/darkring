using UnityEngine;

namespace _Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AudioPlayer : MonoBehaviour {
        protected AudioSource _audioSource;

        [field: SerializeField] private float pitchRandomless = .05f;
        private float _basePitch;

        protected virtual void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            _basePitch = _audioSource.pitch;
        }

        protected void PlayClipWithVariablePitch(AudioClip clip) {
            var randomPitch = Random.Range(-pitchRandomless, +pitchRandomless);
            _audioSource.pitch = _basePitch + randomPitch;
            _audioSource.clip = clip;
            PlayClip(clip);
        }

        protected void PlayClip(AudioClip clip) {
            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}