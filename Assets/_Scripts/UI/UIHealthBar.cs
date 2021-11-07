using Components;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(UIHealthBarAnimationComponent))]
public class UIHealthBar : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private SkeletonGraphic _skeletonGraphic;
    
    private UIHealthBarAnimationComponent _healthBarAnimationComponent;

    private void Awake()
    {
        _healthBarAnimationComponent = GetComponent<UIHealthBarAnimationComponent>();
        _healthBarAnimationComponent.InitUIHealthBarAnimation(_skeletonGraphic);
    }

    public void Idle()
    {
        _healthBarAnimationComponent?.Idle();
    }
    
    public void Die()
    {
        _healthBarAnimationComponent?.Die();
    }
}