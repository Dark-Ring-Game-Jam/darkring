using System;
using UnityEngine;

namespace _Scripts.Audio
{
    public class Music: AudioPlayer
    {
        [field: SerializeField] private AudioClip mainClip, bossClip;

        private bool _bossMusicStarted = false;

        private void OnEnable()
        {
            PlayClip(mainClip);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_bossMusicStarted) return;
            
            if (col.TryGetComponent(out Player player))
            {
                _bossMusicStarted = true;
                PlayClip(bossClip);
            }
        }
    }
}