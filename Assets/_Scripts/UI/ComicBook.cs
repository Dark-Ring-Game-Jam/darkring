using System;
using System.Collections;
using _Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ComicBook: AudioPlayer
    {
        [field: SerializeField] private RawImage image;
        [field: SerializeField] private Texture2D[] textures;
        [field: SerializeField] private AudioClip[] sounds;
        [field: SerializeField] private float[] delays;

        private int _currentSlide = 0;

        protected void Start()
        {
            StartCoroutine(ShowNextSlide(0f));
        }

        private IEnumerator ShowNextSlide(float delay)
        {
            yield return new WaitForSeconds(delay);

            image.texture = textures[_currentSlide];
            PlayClip(sounds[_currentSlide]);
            _currentSlide++;

            if (_currentSlide < textures.Length)
            {
                StartCoroutine(ShowNextSlide(this.delays[_currentSlide]));
            }
            else
            {
                StartCoroutine(StartGame());

            }
            
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(delays[delays.Length - 1]);

            SceneManager.LoadScene("darkring");
        }
    }
}