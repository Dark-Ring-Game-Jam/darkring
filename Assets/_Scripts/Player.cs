using Components;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(MainCharacterAnimationComponent))]
[RequireComponent(typeof(AttackComponent))]
public class Player : MonoBehaviour
{
    private const string IdleAnimationName = "idle";
    
    #region Fields
    
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;

    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;
    private MainCharacterAnimationComponent _animationComponent;
    private AttackComponent _attackComponent;
    
    #endregion Fields

    #region Default

    private void Start()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _animationComponent = GetComponent<MainCharacterAnimationComponent>();
        _attackComponent = GetComponent<AttackComponent>();
        
        _animationComponent.InitMainCharacterAnimation(2f);
        
        _healthComponent.OnDeath += Die;
        _attackComponent.OnAttack += _animationComponent.Attack;
    }
    
    private void Update()
    {
        ProcessInputs();
        ProcessOthers();
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
    
    private void ProcessOthers()
    {
        
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
    
    private void Die()
    {
        _animationComponent.Die();
        _healthComponent.OnDeath -= Die;
        _attackComponent.OnAttack -= _animationComponent.Attack;
    }
}
