using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingFloor : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject teleportTargetB;


    private bool canTeleport = false;


    // Teleport function
    private void TeleportToB()
    {
        if (teleportTargetB != null)
        {
            // Teleport the player to the position of GameObject B
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = teleportTargetB.transform.position;

                // Optionally, you can perform additional actions after teleportation
                // For example, reset velocity or play a teleportation sound
            }
            else
            {
                Debug.LogWarning("Player not found!");
            }
        }
        else
        {
            Debug.LogWarning("Teleport target B is not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactButton.SetActive(true);
            canTeleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactButton.SetActive(false);
            canTeleport = false;
        }
    }

    private void Update()
    {
        // Check for "E" key press in the Update method
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            TeleportToB();
            Debug.Log("teleport ke " + name);
        }
    }
}
