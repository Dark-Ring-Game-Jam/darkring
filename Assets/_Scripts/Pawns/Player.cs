using System.Collections.Generic;
using System;
using System.Collections;
using _Scripts;
using _Scripts.Audio;
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
    public Inventory Inventory => _inventory;
    public bool HasKeroseneLamp => _inventory.ContainItemType(Item.ItemType.KeroseneLamp);
    public IUsable UsableEnvironment {get; set;}
    public bool IsHide {get; private set;}
    public int HealthPoints => _healthComponent.Health;

    private Inventory _inventory;
    private Vector2 _movementDirection = Vector2.zero;
    private bool _faceLeft = true;
    private readonly Vector2 _distance = new Vector2(5f, 5f);
    private bool _isReadNote => UsableEnvironment is NoteView;

    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;
    private MainCharacterAnimationComponent _animationComponent;
    private AttackComponent _attackComponent;
    private SmokeComponent _smokeComponent;
    private CharacterSounds _characterSounds;
    private PhrasesComponent _phrasesComponent;
    private ButtonComponent _buttonComponent;


    [Header("Common")]
    [SerializeField] private float _delayToUse;
    private Coroutine _usingCooldownCoroutine;
    private bool _isUsing;

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
        Init();
    }

    public void Init()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _animationComponent = GetComponent<MainCharacterAnimationComponent>();
        _attackComponent = GetComponent<AttackComponent>();
        _smokeComponent = GetComponent<SmokeComponent>();
        _characterSounds = GetComponent<CharacterSounds>();
        _phrasesComponent = GetComponent<PhrasesComponent>();
        _buttonComponent = GetComponent<ButtonComponent>();

        _buttonComponent.SetButtonActive(false);

        _animationComponent.InitMainCharacterAnimation(2f, 2f, _skeletonAnimation);

        _healthComponent.OnDeath += Die;
        _attackComponent.OnAttack += _animationComponent.Attack;
        _smokeComponent.OnSmoke += _animationComponent.Smoke;

        _inventory = new Inventory();
        _UIInventory.SetPlayer(this);
        _UIInventory.SetInventory(_inventory);

        _UIHealthBar.Idle();
        SetHidePlayer(false);
    }

    private void Update()
    {
        _buttonComponent.SetButtonActive(UsableEnvironment != null);

        if (!_healthComponent.IsDead)
        {
            if (IsBusy() == false)
            {
                ProcessInputs();
                ProcessInteractions();
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsBusy() == false && _isReadNote == false && IsHide == false)
        {
            _movementComponent.Move(_movementDirection);
        }
    }

    #endregion Default

    public void SetHealthPoints(int health)
    {
        _healthComponent.SetHealth(health);
    }

    public void PlayPickUpSound()
    {
        _characterSounds.PlayPickupItemSound();
    }

    public void PlayDoorsSound()
    {
        _characterSounds.PlayOpenDoorSound();
    }

    public void TakeDamage(int damage)
    {
        _healthComponent.TakeDamage(damage);
    }

    public void SetHidePlayer(bool hide)
    {
        _spriteRenderer.enabled = !hide;
        IsHide = hide;
    }

    public void SetText(string text)
    {
        _phrasesComponent.SetText(text);
    }

    private bool IsBusy()
    {
        return
            _attackComponent.IsAttacking ||
            _smokeComponent.IsSmoking ||
            _healthComponent.IsDead;
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
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ShowMenuScreen(true);
        }
        else if (Input.GetKey(KeyCode.E) && _inventory.ContainItemType(Item.ItemType.Batteriy) && _inventory.ContainItemType(Item.ItemType.Flashlight))
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
        else if (Input.GetKey(KeyCode.F) && UsableEnvironment != null && _isUsing == false)
        {
            if (_usingCooldownCoroutine == null)
            {
                _isUsing = true;
                _usingCooldownCoroutine = StartCoroutine(UsingCooldownCoroutine());
                if (UsableEnvironment is Doors || UsableEnvironment is Cupboard)
                {
                    _characterSounds.PlayOpenDoorSound();
                }
                UsableEnvironment.Use(Inventory);
            }
        }

        ProcessAnimation();
    }

    private IEnumerator UsingCooldownCoroutine()
    {
        yield return new WaitForSeconds(_delayToUse);

        _isUsing = false;
        _usingCooldownCoroutine = null;
    }

    private void Attack()
    {
        var results = new Collider2D[5];
        var size = Physics2D.OverlapBoxNonAlloc(transform.position, _distance, 0f, results, LayerMask.NameToLayer("Enemy"));
        var enemies = new Dictionary<ICanBeAttacked, Transform>();

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
                        enemies[enemy] = enemy.transform;
                    }
                }
            }
        }

        _attackComponent.Attack(enemies);
    }

    private void ProcessAnimation()
    {
        if (IsBusy() || _isReadNote)
        {
            return;
        }

        if (_movementDirection == Vector2.zero)
        {
            _animationComponent.Idle();
            return;
        }

        _characterSounds.PlayStepSound();

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

        GameManager.Instance.ShowMenuScreen();
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSpawnPoint spawnPoint) && !spawnPoint.IsUsed)
        {
            GameManager.Instance.Save(spawnPoint.GetPosition());
            spawnPoint.SetUsed();

            // TODO - вывести надпись при сохранении
        }
    }
}
