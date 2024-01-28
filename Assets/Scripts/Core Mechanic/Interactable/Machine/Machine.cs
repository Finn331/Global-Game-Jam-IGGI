using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public float countdownTimer = 5f;
    private float currentTimer;
    private bool isTimerRunning = false;
    private KidnapSystemV2 kidnapSystem;
    public Battery battery;

    public AudioClip npcSound; // Add the AudioClip for the countdown sound
    private AudioSource audioSource;
    private NPCPatrol npcPatrol;

    void Start()
    {
        currentTimer = countdownTimer;
        kidnapSystem = GameObject.Find("Player").GetComponent<KidnapSystemV2>();
        battery = GameObject.Find("Laughinator Bar").GetComponent<Battery>();

        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Set the loop property to true for looping sound
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            StartCountdown();
            npcPatrol = other.GetComponent<NPCPatrol>();
            npcPatrol.StopAllCoroutines();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            StopCountdown();
            currentTimer = countdownTimer;
        }
    }

    void StartCountdown()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StopAllCoroutines();
            StartCoroutine(CountdownCoroutine());
            battery.Mulai();

            // Play the countdown sound when the countdown starts
            if (npcSound != null)
            {
                audioSource.clip = npcSound;
                audioSource.Play();
            }
        }
    }

    void StopCountdown()
    {
        isTimerRunning = false;
        StopAllCoroutines();
        Debug.Log("Countdown stopped");
        battery.StopCountdown();
        battery.Kurang();

        // Stop the countdown sound when the countdown stops
        audioSource.Stop();
    }

    IEnumerator CountdownCoroutine()
    {
        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f;
            Debug.Log("Countdown: " + currentTimer);
        }

        DestroyAllChildrenOf(gameObject);
        Debug.Log("NPC Destroyed");
        kidnapSystem.machineOccupied = false;
        kidnapSystem.machine2Occupied = false;
        kidnapSystem.machine3Occupied = false;
        StopCountdown();
    }

    void DestroyAllChildrenOf(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
