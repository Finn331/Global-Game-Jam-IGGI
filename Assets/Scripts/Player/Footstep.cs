using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioClip walkingSound;
    public AudioClip jumpingSound;
    public Transform footTransform;

    private AudioSource audioSource;

    private Vector3 lastPosition;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform wallCheck;
    public float wallCheckDistance = 0.1f;
    private bool isFacingRight = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Check for player input to determine if walking or jumping
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && IsGrounded())
        {
            PlayFootstepSound(walkingSound);
            if (IsFacingWall())
            {
                audioSource.Stop();
            }
        }
        else if (IsPlayerStationary())
        {
            // Player is stationary, stop the footstep sound
            audioSource.Stop();
        }

        // Check for jumping input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            PlayFootstepSound(jumpingSound);
        }
    }

    private void PlayFootstepSound(AudioClip footstepSound)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepSound;
            audioSource.Play();
        }
    }

    private bool IsPlayerStationary()
    {
        // Check if the player is not moving by comparing current position with the last position
        return transform.position == lastPosition;
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded by casting a ray downwards
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
    }

    private bool IsFacingWall()
    {
        float direction = isFacingRight ? 1f : -1f;
        Vector2 rayOrigin = new Vector2(wallCheck.position.x, wallCheck.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, wallCheckDistance, groundLayer);

        // Return true if there's a wall in front
        return hit.collider != null;
    }
}
