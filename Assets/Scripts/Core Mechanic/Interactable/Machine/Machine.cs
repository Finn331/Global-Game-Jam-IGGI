using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    [Header("Countdown Timer")]
    public float countdownTimer = 5f; // Waktu hitung mundur dalam detik
    private float currentTimer;
    private bool isTimerRunning = false;
    private KidnapSystemV2 kidnapSystem;

    [Header("Battery Status")]
    public Slider batterySlider;
    public int batteryMax = 100;
    private int batteryCurrent;

    void Start()
    {
        batteryCurrent = batteryMax;
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
        StartCoroutine(ReduceHealthOverTime());
    }

    private IEnumerator ReduceHealthOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 10 seconds

            // Reduce health by 1 every minute
            BatteryDrain(1);

        }
    }

    IEnumerator CountdownCoroutine()
    {
        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTimer--;

            StopCoroutine(ReduceHealthOverTime());
            BatteryStatus();
            BatteryCharge(1);
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

    void BatteryStatus()
    {
        // Hitung persentase baterai dan set nilai slider
        int batteryPercentage = (int)batteryCurrent / batteryMax;
        batterySlider.value = batteryPercentage;
    }

    void BatteryDrain(int drainAmount)
    {
        // Kurangi baterai sebesar 1 setiap detik
        batteryCurrent -= drainAmount;
        batteryCurrent = Mathf.Clamp(batteryCurrent, 0, batteryMax);
        BatteryStatus();

        //if (currentBattery <= 0)
        //{
        //    Die();
        //}
    }

    void BatteryCharge(int chargeAmount)
    {
        // Tambah baterai sebesar 1 setiap detik
        batteryCurrent += chargeAmount;
        batteryCurrent = Mathf.Clamp(batteryCurrent, 1, batteryMax);
        BatteryStatus();
    }
}
