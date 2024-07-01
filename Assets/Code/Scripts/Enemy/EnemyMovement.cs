using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Reference to Rigidbody2D on Enemy Object; 
    
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex]; // Set the target to the Start Point;
    }

    private void Update()
    {
        // Check if the Enemy is 0.1 units away from target;
        // The code inside {} will be executed if this condition is true!
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // pathIndex = pathIndex + 1;

            // Check if the Enemy has reached the final path point;
            if(pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                // Switch the target to current pathIndex;
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Local Vector2 (can only be used inside FixedUpdate())
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
