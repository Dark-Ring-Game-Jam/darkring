using UnityEngine;

[RequireComponent(typeof(Transform))]
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private GameObject player;
    [SerializeField] private float stoppingDistance;
    
    private Transform _target;
    
    private void Start()
    {
        _target = player.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        // TODO - учитывать видимость ГГ (гапример, если спрятался в ящик)
        
        if (Vector2.Distance(transform.position, _target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // TODO - возвращается куда-то?
        }
    }
}
