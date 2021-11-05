using System;
using System.Linq;
using Spine.Unity;

namespace Components
{
    public class UIHealthBarAnimationComponent : AnimationComponent
    {
        private SkeletonGraphic _skeletonGraphic;
        
        private const string NormalAnimationName = "circle";
        private const string DeadAnimationName = "dead";
        private const string DeadStaticAnimationName = "dead static";
        
        private float _normalizedDurationToDie;
        
        public void InitUIHealthBarAnimation(SkeletonGraphic skeletonGraphic)
        {
            _skeletonGraphic = skeletonGraphic;
            Init(NormalAnimationName, _skeletonGraphic);
            
            var currentAnimation = AnimationsWithDuration.Keys.FirstOrDefault(x => x.name.Equals(DeadAnimationName));

            if (currentAnimation != null)
            {
                var duration = _animationsWithDuration[currentAnimation];
                _normalizedDurationToDie = 1 - duration;
            }
            else
            {
                throw new NullReferenceException("Has not " + nameof(currentAnimation));
            }
        }

        public void Idle()
        {
            SetAnimationState(NormalAnimationName);
        }
        
        public void Die()
        {
            var timeScale = _skeletonGraphic.timeScale - _normalizedDurationToDie;
            SetAnimationState(DeadAnimationName, false, timeScale);
            SetAnimationState(DeadStaticAnimationName, false);
        }
    }
}