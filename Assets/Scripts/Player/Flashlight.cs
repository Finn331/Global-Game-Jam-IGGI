using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public AudioClip flashlightSound;

    public AudioSource audioSource;

    void Start()
    {
        // Create an AudioSource component and attach it to the GameObject
        audioSource.clip = flashlightSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(!flashlight.activeSelf);

            // Check if the audio source is not null to avoid potential issues
            if (audioSource != null)
            {
                // Adjust the volume here (increase or decrease based on your needs)
                audioSource.volume = Mathf.Clamp(audioSource.volume + 0.1f, 0f, 1f);

                // Play the audio clip
                audioSource.Play();
            }
        }
    }
}
