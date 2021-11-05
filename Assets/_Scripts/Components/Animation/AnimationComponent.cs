using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [Serializable] public class AnimationDurationDictionary : SerializableDictionary<AnimationReferenceAsset, float> {}

    [SerializeField] protected AnimationDurationDictionary _animationsWithDuration;

    public IDictionary<AnimationReferenceAsset, float> AnimationsWithDuration => _animationsWithDuration;
    public AnimationReferenceAsset IdleAnimation => _idleAnimation;

    private IAnimationStateComponent _animationStateComponent;
    private AnimationReferenceAsset _idleAnimation;
    private AnimationReferenceAsset _currentAnimation;

    public void Init(string idleAnimationName, IAnimationStateComponent animationStateComponent)
    {
        _animationStateComponent = animationStateComponent;
        _idleAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(idleAnimationName));
        if (_idleAnimation == null)
        {
            throw new NullReferenceException(nameof(_idleAnimation));
        }
    }

    public void SetAnimationState(string state, bool loop = true, float timeScale = -1)
    {
        var currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(state));
        if (currentAnimation != null)
        {
            if (Mathf.Approximately(timeScale, -1))
            {
                timeScale = AnimationsWithDuration[currentAnimation];
            }

            SetAnimation(currentAnimation, loop, timeScale);
        }
        else
        {
            // если анимации с переданным состоянием нет, то запускаем idle
            SetAnimation(_idleAnimation, true, AnimationsWithDuration[_idleAnimation]);
        }
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.Equals(_currentAnimation))
        {
            return;
        }

        _animationStateComponent.AnimationState.SetAnimation(0, animation, loop).TimeScale = timeScale;
        _currentAnimation = animation;
    }
}