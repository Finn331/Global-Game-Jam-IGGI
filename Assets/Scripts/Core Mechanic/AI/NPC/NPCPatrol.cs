using System.Collections;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    private int currentWaypointIndex = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to NPCPatrol script on " + gameObject.name);
            enabled = false; // Disable the script if no waypoints are assigned
            return;
        }

        // Randomize the order of waypoints
        waypoints = ShuffleArray(waypoints);

        StartCoroutine(Patrol());
    }

    Transform[] ShuffleArray(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // Set animation parameter for movement
            animator.SetBool("isWalking", true);

            // Move towards the current waypoint
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check the direction of movement and flip the sprite accordingly
            if (targetPosition.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            // Check if the NPC has reached the current waypoint
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Stop walking animation
                animator.SetBool("isWalking", false);

                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                // Wait for a short time before moving to the next waypoint
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }
}
