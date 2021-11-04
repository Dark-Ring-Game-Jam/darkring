using Components;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AnimationComponent))]
public class Player : MonoBehaviour
{
    private const string IdleAnimationName = "idle";
    
    #region Fields
    
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;

    private MovementComponent _movementComponent;
    private AnimationComponent _animationComponent;
    
    #endregion Fields

    #region Default

    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _animationComponent = GetComponent<AnimationComponent>();
        
        _animationComponent.Init(IdleAnimationName);
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

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    private void ProcessAnimation()
    {
        if (_movementDirection == Vector2.zero)
        {
            _animationComponent.SetAnimationState(IdleAnimationName);
            return;
        }

        // TODO - добавить выбор анимации в зависимости от направления движения (продумать как)
        if (_movementDirection.x >= 0 && _movementDirection.y > 0)
        {
            _animationComponent.SetAnimationState("back_walk");
        }
        else if (_movementDirection.x >= 0 && _movementDirection.y < 0)
        {
            _animationComponent.SetAnimationState("front_walk");
        }
        else if (_movementDirection.y == 0)
        {
            _animationComponent.SetAnimationState("walking");
        }
    }
}
