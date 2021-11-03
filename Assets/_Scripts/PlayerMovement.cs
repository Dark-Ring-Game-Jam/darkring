using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Main")]
    [SerializeField, Range(0.1f, 10f)] private float movementSpeed = 2.5f;
    private Rigidbody2D rigidBody;
    private Vector2 movementDirection = Vector2.zero;
    private bool faceLeft = true;
 
    [Header("Animations")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private List<AnimationReferenceAsset> animationAssets;
    [SerializeField] private string currentState;
    private AnimationReferenceAsset idle;
    private string currentAnimation;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (animationAssets?.Count > 0)
        {
            idle = animationAssets.Find(x => x.name.Equals("idle"));
            if (idle != null)
            {
                currentState = idle.name;
                SetCharacterState(currentState);
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
            // TODO - добавить выбор анимации в зависимости от направления движения
            
            if (movementDirection.x >= 0 && movementDirection.y > 0)
            {
                SetCharacterState("back_walk");
            }
            else if (movementDirection.x >= 0 && movementDirection.y < 0)
            {
                SetCharacterState("front_walk");
            }
            else if (movementDirection.y == 0)
            {
                SetCharacterState("walking");
            }
        }
        else
        {
            SetCharacterState(idle.name);
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
        if (animationAssets?.Count > 0)
        {
            var currentAnimation = animationAssets.Find(x => x.name.Equals(state));
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
