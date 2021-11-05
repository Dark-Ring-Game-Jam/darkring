using _Scripts;
using Components;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(MainCharacterAnimationComponent))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(SmokeComponent))]
public class Player : MonoBehaviour, ICanBeAttacked
{
    #region Fields

    public float Speed => _movementComponent.Speed;


    private Inventory _inventory;
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

    private void Awake()
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

        _inventory = new Inventory();
    }

    private void Update()
    {
        if (!_healthComponent.IsDead)
        {
            if (IsBusy() == false)
            {
                ProcessInputs();
                ProcessInteractions();
            }
        }
        else
        {
            // TODO - закончить игру (через GameComponent?)
        }
    }

    private void FixedUpdate()
    {
        if (IsBusy() == false)
        {
            _movementComponent.Move(_movementDirection);
        }
    }

    #endregion Default

    public void TakeDamage(int damage)
    {
        _healthComponent.TakeDamage(damage);
    }

    private bool IsBusy()
    {
        return _attackComponent.IsAttacking || _smokeComponent.IsSmoking || _healthComponent.IsDead;
    }

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
        if (Input.GetKey(KeyCode.E))
        {
            if (_targetEnemy != null && _attackComponent.CanAttack(_targetEnemy.transform.position) && _attackComponent.IsAttacking == false)
            {
                _attackComponent.Attack(_targetEnemy.GetComponent<ICanBeAttacked>(), _targetEnemy.transform.position);
            }
        }
        else if (Input.GetKey(KeyCode.X))
        {
            if (_smokeComponent.CanSmoke() && _smokeComponent.IsSmoking == false)
            {
                _smokeComponent.Smoke();
            }
        }

        ProcessAnimation();
    }

    private void ProcessAnimation()
    {
        if (IsBusy())
        {
            return;
        }

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
