using UnityEngine;
using Spine.Unity;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] private float movementSpeed = 2.5f;
    
    private Rigidbody2D rigidBody;
    private Vector2 movementDirection = Vector2.zero;
    private bool faceLeft = true;

    [SerializeField] private SkeletonAnimation skeletonAnimation;

    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset smoke;
    [SerializeField] private AnimationReferenceAsset startWalking;
    [SerializeField] private AnimationReferenceAsset walking;
    [SerializeField] private AnimationReferenceAsset stopWalking;

    [SerializeField] private string currentState; // TODO - заменить на enum

    private string currentAnimation;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        currentState = "idle";
        SetCharacterState(currentState);
    }
    
    private void Update()
    {
        ProcessInputs();
        ProcessAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0 && faceLeft)
        {
            Flip();
            faceLeft = false;
        }
        else if (moveX < 0 && !faceLeft)
        {
            Flip();
            faceLeft = true;
        }

        movementDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rigidBody.MovePosition(rigidBody.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }

    private void ProcessAnimation()
    {
        if (movementDirection != Vector2.zero)
        {
            SetCharacterState("walking");
        }
        else
        {
            SetCharacterState("idle");
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
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    private void SetCharacterState(string state)
    {
        if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 0.7f);
        }
        else if (state.Equals("walking"))
        {
            SetAnimation(walking, true, 1.5f);
        }
    }
}
