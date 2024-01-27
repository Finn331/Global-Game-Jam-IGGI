using System.Collections;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    private int currentWaypointIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to NPCPatrol script on " + gameObject.name);
            enabled = false; // Disable the script if no waypoints are assigned
            return;
        }

        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // Set animation parameter for movement
            animator.SetBool("isWalking", true);

            // Move towards the current waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // Check if the NPC has reached the current waypoint
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
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
