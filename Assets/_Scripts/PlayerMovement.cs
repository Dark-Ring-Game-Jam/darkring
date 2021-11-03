using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float movementSpeed = 5f;
    
    private Rigidbody2D rigidBody;
    private Vector2 movementDirection = Vector2.zero;
    private bool faceLeft = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        ProcessInputs();
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

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
