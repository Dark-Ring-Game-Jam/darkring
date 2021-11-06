using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using Components;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(MainCharacterAnimationComponent))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(SmokeComponent))]
[RequireComponent(typeof(UIInventory))]
public class Player : MonoBehaviour, ICanBeAttacked
{
    #region Fields

    public float Speed => _movementComponent.Speed;
    public Vector2 NormalizedDirection => _movementDirection.normalized;
    public Inventory Inventory => _inventory;
    public int KeysCount => _inventory.ItemCount(Item.ItemType.Key);
    public IUsable UsableEnvironment {get; set;}

    private Inventory _inventory;
    private bool _hasKeroseneKeroseneLamp => _inventory.ContainItemType(Item.ItemType.KeroseneLamp);
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;

    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;
    private MainCharacterAnimationComponent _animationComponent;
    private AttackComponent _attackComponent;
    private SmokeComponent _smokeComponent;

    // TODO - для теста (потом выбирать динамически ближайшего врага или бить всех по области)
    [Header("For Tests")]
    [SerializeField] private Enemy _targetEnemy;

    [Header("UI")]
    [SerializeField] private UIInventory _UIInventory;
    [SerializeField] private UIHealthBar _UIHealthBar;

    [Header("Animation")]
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    #endregion Fields

    #region Default

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _animationComponent = GetComponent<MainCharacterAnimationComponent>();
        _attackComponent = GetComponent<AttackComponent>();
        _smokeComponent = GetComponent<SmokeComponent>();

        _animationComponent.InitMainCharacterAnimation(2f, 2f, _skeletonAnimation);

        _healthComponent.OnDeath += Die;
        _attackComponent.OnAttack += _animationComponent.Attack;
        _smokeComponent.OnSmoke += _animationComponent.Smoke;

        _inventory = new Inventory();
        _UIInventory.SetPlayer(this);
        _UIInventory.SetInventory(_inventory);
        
        //TODO - для проверки атаки (потом удалить)
        //----------------------------------------------
        ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.Batteriy, Amount = 2 });
        ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.Flashlight, Amount = 1 });
        ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.KeroseneLamp, Amount = 1 });
        //----------------------------------------------
    }

    //TODO - для проверки атаки (потом удалить)
    //----------------------------------------------
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.TryGetComponent(out ItemWorld item))
        {
            _inventory.AddItem(item.GetItem());
            item.DestroySelf();
        }
    }
    //----------------------------------------------
    
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
            // TODO - закончить игру (через GameManager?)
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
            // TODO - использовать фонарик для атаки
        }
        else if (Input.GetKey(KeyCode.X))
        {
            if (_smokeComponent.CanSmoke() && _smokeComponent.IsSmoking == false)
            {
                _smokeComponent.Smoke();
            }
        }
        else if (Input.GetKey(KeyCode.F) && UsableEnvironment != null)
        {
            UsableEnvironment.Use(Inventory);
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
            _animationComponent.BackWalk(_hasKeroseneKeroseneLamp);
        }
        else if (_movementDirection.x >= 0 && _movementDirection.y < 0)
        {
            _animationComponent.FrontWalk(_hasKeroseneKeroseneLamp);
        }
        else if (_movementDirection.y == 0)
        {
            _animationComponent.SideWalk(_hasKeroseneKeroseneLamp);
        }
    }

    private void Die()
    {
        _UIHealthBar.Die();
        _animationComponent.Die();
        _healthComponent.OnDeath -= Die;
        _attackComponent.OnAttack -= _animationComponent.Attack;
        _smokeComponent.OnSmoke -= _animationComponent.Smoke;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
