using System;
using System.Linq;
using Spine.Unity;
using UnityEngine;

namespace Components
{
    public class MainCharacterAnimationComponent : AnimationComponent
    {
        private const string IdleAnimationName = "idle";
        private const string AttackAnimationName = "attack";
        private const string DieAnimationName = "die";
        private const string BackWalkAnimationName = "back_walk";
        private const string BackWalkWithKeroseneLampAnimationName = "back_walk_with lamp";
        private const string FrontWalkAnimationName = "front_walk";
        private const string FrontWalkWithKeroseneLampAnimationName = "front_walk_with lamp";
        private const string SideWalkAnimationName = "walking";
        private const string SideWalkWithKeroseneLampAnimationName = "walking_with lamp";
        private const string SmokeAnimationName = "smoke";

        private SkeletonAnimation _skeletonAnimation;
        private float _normalizedDurationToDelayAttack;
        private float _normalizedDurationToDelaySmoke;

        public void InitMainCharacterAnimation(float delayToAttack, float delayToSmoke, SkeletonAnimation skeletonAnimation)
        {
            _skeletonAnimation = skeletonAnimation;
            Init(IdleAnimationName, skeletonAnimation);

            var currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(AttackAnimationName));

            if (currentAnimation != null)
            {
                var duration = _animationsWithDuration[currentAnimation];
                _normalizedDurationToDelayAttack = 1 - duration / delayToAttack;
            }
            else
            {
                throw new NullReferenceException("Has not " + nameof(currentAnimation));
            }
            
            currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(SmokeAnimationName));

            if (currentAnimation != null)
            {
                var duration = _animationsWithDuration[currentAnimation];
                _normalizedDurationToDelaySmoke = 1 - duration / delayToSmoke;
            }
            else
            {
                throw new NullReferenceException("Has not " + nameof(currentAnimation));
            }
        }

        public void Die()
        {
            SetAnimationState(DieAnimationName, false);
        }

        public void Idle()
        {
            SetAnimationState(IdleAnimationName);
        }

        public void Attack()
        {
            var timeScale = _skeletonAnimation.timeScale - _normalizedDurationToDelayAttack;
            SetAnimationState(IdleAnimationName);
            SetAnimationState(AttackAnimationName, false, timeScale);
        }
        
        public void Smoke()
        {
            var timeScale = _skeletonAnimation.timeScale - _normalizedDurationToDelaySmoke;
            SetAnimationState(IdleAnimationName);
            SetAnimationState(SmokeAnimationName, false, timeScale);
        }
        
        public void BackWalk(bool withKeroseneLamp = false)
        {
            if (withKeroseneLamp)
            {
                SetAnimationState(BackWalkWithKeroseneLampAnimationName);
            }
            else
            {
                SetAnimationState(BackWalkAnimationName);
            }
        }
        
        public void FrontWalk(bool withKeroseneLamp = false)
        {if (withKeroseneLamp)
            {
                SetAnimationState(FrontWalkWithKeroseneLampAnimationName);
            }
            else
            {
                SetAnimationState(FrontWalkAnimationName);
            }
        }
        
        public void SideWalk(bool withKeroseneLamp = false)
        {
            if (withKeroseneLamp)
            {
                SetAnimationState(SideWalkWithKeroseneLampAnimationName);
            }
            else
            {
                SetAnimationState(SideWalkAnimationName);
            }
        }
    }
}