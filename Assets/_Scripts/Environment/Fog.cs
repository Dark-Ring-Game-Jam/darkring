using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(FogAnimationComponent))]
public class Fog : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    
    private FogAnimationComponent _animationComponent;
    
    private void Awake()
    {
        _animationComponent = GetComponent<FogAnimationComponent>();
        _animationComponent.InitFogAnimation(_skeletonAnimation);
    }

    public void Idle()
    {
        _animationComponent.Idle();
    }

    public void On()
    {
        _animationComponent.On();
    }

    public void Off()
    {
        _animationComponent.Off();
    }
    
    public void Destroy()
    {
        DestroySelf();
    }
    
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}