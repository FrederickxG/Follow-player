using UnityEngine;
using System.Collections;

public class Enemyfollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform component
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public float detectionRange = 10f; // Range at which the enemy detects the player
    public LayerMask obstacleLayer; // Layer mask to detect obstacles (e.g., other enemies)
    public float spreadRadius = 2f; // Adjust the spread of enemies

    private float timer;
    private bool canAttack;



    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Check for obstacles before moving towards the player
            if (!Physics.Raycast(transform.position, player.position - transform.position, detectionRange, obstacleLayer))
            {
                // Add some randomness to the movement direction
                Vector3 randomDirection = Random.insideUnitSphere * spreadRadius;
                Vector3 targetPosition = player.position + randomDirection;

                // Move the enemy towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Rotate the enemy to face the player (optional)
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            }
        }
    }

}