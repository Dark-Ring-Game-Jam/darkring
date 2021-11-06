using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using Components;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

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
    public bool HasKeroseneLamp => _inventory.ContainItemType(Item.ItemType.KeroseneLamp);
    public IUsable UsableEnvironment {get; set;}
    public bool IsHide {get; private set;}

    private Inventory _inventory;
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;
    private readonly Vector2 _distance = new Vector2(5f, 3f);

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

    [SerializeField] private MeshRenderer _spriteRenderer;

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
        /*ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.Batteriy, Amount = 2 });
        ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.Flashlight, Amount = 1 });
        ItemWorld.SpawnItemWorld(transform.position, new Item { Type = Item.ItemType.KeroseneLamp, Amount = 1 });*/
        //----------------------------------------------
    }

    //TODO - для проверки атаки (потом удалить)
    //----------------------------------------------
    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out ItemWorld item))
        {
            _inventory.AddItem(item.GetItem());
            item.DestroySelf();
        }
    }*/
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

    public void SetHidePlayer(bool active)
    {
        _spriteRenderer.enabled = active;
        IsHide = active;
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
        if (Input.GetKey(KeyCode.E) && _inventory.ContainItemType(Item.ItemType.Batteriy) && _inventory.ContainItemType(Item.ItemType.Flashlight))
        {
            Attack();
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

    private void Attack()
    {
        var results = new Collider2D[5];
        var size = Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + new Vector2(Mathf.Sign(-transform.localScale.x) * 1.5f, 1f), _distance, 0f, results, LayerMask.NameToLayer("Enemy"));
        var enemies = new Dictionary<ICanBeAttacked, Vector2>();

        _inventory.RemoveItem(new Item { Type = Item.ItemType.Batteriy, Amount = 1 });

        if (size > 0)
        {
            foreach (var collider2d in results)
            {
                if (collider2d != null && collider2d.TryGetComponent(out Enemy enemy))
                {
                    if (enemy is BigEnemy bigEnemy)
                    {
                        bigEnemy.ResetSpeed();
                    }
                    else
                    {
                        enemies[enemy] = enemy.transform.position;
                    }
                }
            }
        }

        _attackComponent.Attack(enemies);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(Mathf.Sign(-transform.localScale.x) * 1.5f, 1f), new Vector2(5f, 3f));
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
            _animationComponent.BackWalk(HasKeroseneLamp);
        }
        else if (_movementDirection.x >= 0 && _movementDirection.y < 0)
        {
            _animationComponent.FrontWalk(HasKeroseneLamp);
        }
        else if (_movementDirection.y == 0)
        {
            _animationComponent.SideWalk(HasKeroseneLamp);
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
