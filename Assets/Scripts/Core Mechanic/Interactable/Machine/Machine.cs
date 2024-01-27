using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 30f;
    private float currentTime;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = countdownTime;
        //UpdateTimerText();
        //StartCoroutine(StartCountdown());
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            UpdateTimerText();
            StartCoroutine(StartCountdown());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            StopCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateTimerText();
        }

        // Waktu habis, lakukan tindakan yang diinginkan di sini
        Debug.Log("Waktu Habis!");

        // Optional: Anda dapat menambahkan tindakan tambahan saat waktu habis di sini

        // Reset timer untuk penggunaan selanjutnya
        currentTime = countdownTime;
        UpdateTimerText();
        StartCoroutine(StartCountdown());
    }

    void UpdateTimerText()
    {
        // Pastikan timerText tidak null
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        }
    }
}
