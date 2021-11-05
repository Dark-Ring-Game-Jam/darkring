using Spine.Unity;
using UnityEngine;

public class SpineAnimationStateMachineBehaviour : StateMachineBehaviour
{
    [SerializeField] private string _animationName;
    [SerializeField] private float _speed;
    [SerializeField] private bool _loop;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkeletonAnimation animation = animator.GetComponent<SkeletonAnimation>();
        animation.state.SetAnimation(0, _animationName, _loop).TimeScale = _speed;
    }
}