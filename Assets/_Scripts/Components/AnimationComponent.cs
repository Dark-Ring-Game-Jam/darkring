using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [Serializable] public class AnimationDurationDictionary : SerializableDictionary<AnimationReferenceAsset, float> {}
    
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] AnimationDurationDictionary _animationsWithDuration;

    public IDictionary<AnimationReferenceAsset, float> AnimationsWithDuration => _animationsWithDuration;

    private AnimationReferenceAsset _idleAnimation;
    public AnimationReferenceAsset IdleAnimation => _idleAnimation;

    private AnimationReferenceAsset _currentAnimation;
    
    public void Init(string idleAnimationName)
    {
        _idleAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(idleAnimationName));
        if (_idleAnimation == null)
        {
            throw new NullReferenceException(nameof(_idleAnimation));
        }
    }

    public void ProcessAnimation(Vector2 movementDirection)
    {
        if (movementDirection == Vector2.zero)
        {
            SetAnimationState(_idleAnimation.name);
            return;
        }

        // TODO - добавить выбор анимации в зависимости от направления движения (продумать как)
        if (movementDirection.x >= 0 && movementDirection.y > 0)
        {
            SetAnimationState("back_walk");
        }
        else if (movementDirection.x >= 0 && movementDirection.y < 0)
        {
            SetAnimationState("front_walk");
        }
        else if (movementDirection.y == 0)
        {
            SetAnimationState("walking");
        }
    }
    
    private void SetAnimationState(string state)
    {
        var currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(state));
        if (currentAnimation != null)
        {
            SetAnimation(currentAnimation, true, AnimationsWithDuration[currentAnimation]);
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
        
        _skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        _currentAnimation = animation;
    }
}