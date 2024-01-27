using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public float countdownTimer = 5f; // Waktu hitung mundur dalam detik
    private float currentTimer;
    private bool isTimerRunning = false;
    private KidnapSystemV2 kidnapSystem;

    void Start()
    {
        currentTimer = countdownTimer;
        kidnapSystem = GameObject.Find("Player").GetComponent<KidnapSystemV2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            // Jika bersentuhan dengan NPC, mulai hitung mundur
            StartCountdown();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            // Jika tidak bersentuhan dengan NPC, hentikan hitung mundur
            StopCountdown();
            currentTimer = countdownTimer;
        }
    }

    void StartCountdown()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    void StopCountdown()
    {
        isTimerRunning = false;
        StopAllCoroutines();
        Debug.Log("Countdown stopped");
    }

    IEnumerator CountdownCoroutine()
    {
        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTimer--;

            // Debug log selama hitung mundur
            Debug.Log("Countdown: " + currentTimer);
        }

        // Waktu habis, hancurkan NPC dan hentikan timer
        DestroyAllChildrenOf(gameObject);
        Debug.Log("NPC Destroyed");
        kidnapSystem.machineOccupied = false;
        kidnapSystem.machine2Occupied = false;
        kidnapSystem.machine3Occupied = false;
        StopCountdown();
    }

    void DestroyAllChildrenOf(GameObject parent)
    {
        // Loop melalui semua anak dan hancurkan masing-masing
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
