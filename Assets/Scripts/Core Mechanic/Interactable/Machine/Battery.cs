using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Slider batterySlider;
    public float maxBattery = 100f;
    private float currentBattery;
    private Coroutine increaseBatteryCoroutine;
    private Coroutine decreaseBatteryCoroutine;

    void Start()
    {
        if (batterySlider != null)
        {
            InitializeBatteryBar();
        }
    }

    public void InitializeBatteryBar()
    {
        currentBattery = 1f;
        UpdateBatteryBar();
    }

    public void Mulai()
    {
        // Hentikan DecreaseBattery jika sedang berjalan
        if (decreaseBatteryCoroutine != null)
        {
            StopCoroutine(decreaseBatteryCoroutine);
            decreaseBatteryCoroutine = null;
        }

        // Mulai IncreaseBattery
        increaseBatteryCoroutine = StartCoroutine(IncreaseBattery());
    }

    public void Kurang()
    {
        // Hentikan IncreaseBattery jika sedang berjalan
        if (increaseBatteryCoroutine != null)
        {
            StopCoroutine(increaseBatteryCoroutine);
            increaseBatteryCoroutine = null;
        }

        // Mulai DecreaseBattery
        decreaseBatteryCoroutine = StartCoroutine(DecreaseBattery());
    }

    public void StopCountdown()
    {
        // Hentikan keduanya jika sedang berjalan
        if (increaseBatteryCoroutine != null)
        {
            StopCoroutine(increaseBatteryCoroutine);
            increaseBatteryCoroutine = null;
        }

        if (decreaseBatteryCoroutine != null)
        {
            StopCoroutine(decreaseBatteryCoroutine);
            decreaseBatteryCoroutine = null;
        }

        Debug.Log("Countdown stopped");
    }

    public IEnumerator IncreaseBattery()
    {
        while (currentBattery < 100f)
        {
            yield return new WaitForSeconds(1f);
            currentBattery++;
            Debug.Log("Tambah");
            UpdateBatteryBar();
        }
    }

    public IEnumerator DecreaseBattery()
    {
        while (currentBattery >= 0f)
        {
            yield return new WaitForSeconds(1f);
            currentBattery--;
            Debug.Log("Kurang");
            UpdateBatteryBar();
        }

        if (currentBattery == 0)
        {
            StopCoroutine(DecreaseBattery());
        }
    }

    void UpdateBatteryBar()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery;
        }
    }
}