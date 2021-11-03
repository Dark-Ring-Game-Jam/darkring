using System.Collections.Generic;
using Components;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(MovementComponent))]
public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [Header("Main")]
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;
    
    // TODO - вынести в AnimationComponent
    [Header("Animations")]
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private string _currentState = "idle";
    [SerializeField] private List<AnimationReferenceAsset> _animationAssets;
    private AnimationReferenceAsset _idle;
    private string _currentAnimation;

    private MovementComponent _movementComponent;
    
    #endregion Fields

    #region Default

    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        
        if (_animationAssets?.Count > 0)
        {
            _idle = _animationAssets.Find(x => x.name.Equals("idle"));
            if (_idle != null)
            {
                _currentState = _idle.name;
                SetCharacterState(_currentState);
            }
        }
    }
    
    private void Update()
    {
        ProcessInputs();
        ProcessAnimation();
    }

    private void FixedUpdate()
    {
        _movementComponent.Move(_movementDirection);
    }
    
    #endregion Default

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0 && _faceLeft)
        {
            Flip();
            _faceLeft = false;
        }
        else if (moveX < 0 && !_faceLeft)
        {
            Flip();
            _faceLeft = true;
        }

        _movementDirection = new Vector2(moveX, moveY).normalized;
    }

    private void ProcessAnimation()
    {
        if (_movementDirection != Vector2.zero)
        {
            // TODO - добавить выбор анимации в зависимости от направления движения
            
            if (_movementDirection.x >= 0 && _movementDirection.y > 0)
            {
                SetCharacterState("back_walk");
            }
            else if (_movementDirection.x >= 0 && _movementDirection.y < 0)
            {
                SetCharacterState("front_walk");
            }
            else if (_movementDirection.y == 0)
            {
                SetCharacterState("walking");
            }
        }
        else
        {
            SetCharacterState(_idle.name);
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(_currentAnimation))
        {
            return;
        }
        _skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        _currentAnimation = animation.name;
    }

    private void SetCharacterState(string state)
    {
        if (_animationAssets?.Count > 0)
        {
            var currentAnimation = _animationAssets.Find(x => x.name.Equals(state));
            if (currentAnimation != null)
            {
                SetAnimation(currentAnimation, true, currentAnimation.name.Equals("idle") ? 0.7f : 1.5f);
            }
        }
        
        /*if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 0.7f);
        }
        else if (state.Equals("walking"))
        {
            SetAnimation(walking, true, 1.5f);
        }*/
    }
}
