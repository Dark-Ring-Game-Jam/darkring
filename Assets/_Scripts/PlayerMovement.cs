using Components;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AnimationComponent))]
public class PlayerMovement : MonoBehaviour
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
        _animationComponent.ProcessAnimation(_movementDirection);
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
}
