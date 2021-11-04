using _Scripts;
using Components;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(MainCharacterAnimationComponent))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(SmokeComponent))]
public class Player : MonoBehaviour
{
    #region Fields
    
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;

    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;
    private MainCharacterAnimationComponent _animationComponent;
    private AttackComponent _attackComponent;
    private SmokeComponent _smokeComponent;

    // TODO - для теста (потом выбирать динамически ближайшего врага)
    [SerializeField] private Enemy _targetEnemy;
    
    #endregion Fields

    #region Default

    private void Start()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _animationComponent = GetComponent<MainCharacterAnimationComponent>();
        _attackComponent = GetComponent<AttackComponent>();
        _smokeComponent = GetComponent<SmokeComponent>();
        
        _animationComponent.InitMainCharacterAnimation(2f, 2f);
        
        _healthComponent.OnDeath += Die;
        _attackComponent.OnAttack += _animationComponent.Attack;
        _smokeComponent.OnSmoke += _animationComponent.Smoke;
    }
    
    private void Update()
    {
        if (!_healthComponent.IsDead)
        {
            ProcessInputs();
            ProcessInteractions();
        }
        else
        {
            // TODO - закончить игру (через GameComponent?)
        }
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
    
    private void ProcessInteractions()
    {
        if (_attackComponent.CanAttack(_targetEnemy.transform.position) && _attackComponent.IsAttacking == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _attackComponent.Attack(_targetEnemy.HealthComponent);
            }
        }
        else if (_attackComponent.IsAttacking == false)
        {
            if (_smokeComponent.CanSmoke() && _smokeComponent.IsSmoking == false)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    _smokeComponent.Smoke();
                }
                else
                {
                    ProcessAnimation();
                }
            }
            else if (_smokeComponent.IsSmoking == false)
            {
                ProcessAnimation();
            }
        }
    }

    private void ProcessAnimation()
    {
        if (_movementDirection == Vector2.zero)
        {
            _animationComponent.Idle();
            return;
        }

        if (_movementDirection.x >= 0 && _movementDirection.y > 0)
        {
            _animationComponent.BackWalk();
        }
        else if (_movementDirection.x >= 0 && _movementDirection.y < 0)
        {
            _animationComponent.FrontWalk();
        }
        else if (_movementDirection.y == 0)
        {
            _animationComponent.SideWalk();
        }
    }
    
    private void Die()
    {
        _animationComponent.Die();
        _healthComponent.OnDeath -= Die;
        _attackComponent.OnAttack -= _animationComponent.Attack;
        _smokeComponent.OnSmoke -= _animationComponent.Smoke;
    }
}
