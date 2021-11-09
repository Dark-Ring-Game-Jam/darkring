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
        private bool _startGame;

        protected void Start()
        {
            StartCoroutine(ShowNextSlide(0f));
        }

        protected void Update()
        {
            if (!_startGame && Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.E) ||
                Input.GetKey(KeyCode.Space))
            {
                _startGame = true;
                StopAllCoroutines();
                StartCoroutine(StartGame());
            }
        }

        private IEnumerator ShowNextSlide(float delay)
        {
            if (!_startGame)
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
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(delays[delays.Length - 1]);

            SceneManager.LoadScene("darkring");
        }
    }
}