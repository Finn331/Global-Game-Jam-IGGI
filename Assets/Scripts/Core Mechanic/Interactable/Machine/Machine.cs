using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public float countdownTimer = 5f;
    private float currentTimer;
    private bool isTimerRunning = false;
    private KidnapSystemV2 kidnapSystem;
    public Battery battery;

    void Start()
    {
        currentTimer = countdownTimer;
        kidnapSystem = GameObject.Find("Player").GetComponent<KidnapSystemV2>();
        battery = GameObject.Find("Laughinator Bar").GetComponent<Battery>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            StartCountdown();
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
        }
    }

    void StopCountdown()
    {
        isTimerRunning = false;
        StopAllCoroutines();
        Debug.Log("Countdown stopped");
        battery.StopCountdown();
        battery.Kurang();
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