using System.Collections;
using UnityEngine;

public class PolicePatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;

    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    public Transform player;
    private Animator animator;

    private KidnapSystem kidnapSystem;

    void Start()
    {
        kidnapSystem = FindObjectOfType<KidnapSystem>();
        animator = GetComponent<Animator>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to NPCPatrolChaser script on " + gameObject.name);
            enabled = false; // Disable the script if no waypoints are assigned
            return;
        }

        StartCoroutine(Patrol());
    }

    void Update()
    {
        // Check for the player in detection range using raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, detectionRange);

        if (hit.collider != null && kidnapSystem.fullBag.activeSelf)
        {
            if (hit.collider.CompareTag("Player"))
            {
                player = hit.collider.transform;
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }
        else
        {
            isChasing = false;
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasing)
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
            }
            else
            {
                // Set animation parameter for movement
                animator.SetBool("isWalking", true);

                // Move towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

                yield return null;
            }
            yield return null;
        }
    }

}
