using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private GameObject player;
    [SerializeField] private float stoppingDistance;
    
    private Transform target;
    
    private void Start()
    {
        target = player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            // TODO - возвращается куда-то?
        }
    }
}
