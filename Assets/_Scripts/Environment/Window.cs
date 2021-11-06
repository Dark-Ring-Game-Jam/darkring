using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace _Scripts.Environment
{
    public class Window : MonoBehaviour
    {
        [field: SerializeField] private bool sideWindow = false;
        [field: SerializeField] private float thunderDelay = 0.5f;
        [field: SerializeField] private float thunderDuration = 2f;
        
        private SkeletonAnimation _skeletonAnimation;
        void Start()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _skeletonAnimation.state.SetAnimation(0, GetAnimationName(), false);
        }

        public void PlayThunderAnimation()
        {
            StartCoroutine(StartThunder());
        }

        private IEnumerator StartThunder()
        {
            yield return new WaitForSeconds(thunderDelay);
            _skeletonAnimation.state.SetAnimation(0, GetAnimationName(), true);
            StartCoroutine(StopThunder());
        }
        
        private IEnumerator StopThunder()
        {
            yield return new WaitForSeconds(thunderDuration);
            _skeletonAnimation.state.SetAnimation(0, GetAnimationName(), false);
        }

        private string GetAnimationName()
        {
            return sideWindow ? "side thunder" : "thunder";
        }
    }
}
