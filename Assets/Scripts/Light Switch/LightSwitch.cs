using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<GameObject> lights; // List of lights controlled by this switch
    public float batteryRequirement = 1f; // Minimum battery level required to turn on lights
    public AudioSource audioSource; // Audio source to play when the switch is toggled
    public GameObject interactButton; // The button to press to interact with the switch

    private bool isPlayerInRange = false;
    private Animator anim;

    void Start()
    {
        // Assign the Animator component of the game object
        anim = GetComponent<Animator>();

        if (lights.Count == 0)
        {
            Debug.LogError("No lights assigned to LightSwitch script on " + gameObject.name);
            enabled = false; // Disable the script if no lights are assigned
            return;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Check battery level before toggling lights
            if (CheckBatteryLevel())
            {
                ToggleAllLights();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactButton.SetActive(false);
        }
    }

    bool CheckBatteryLevel()
    {
        Battery battery = FindObjectOfType<Battery>(); // Assuming there's only one Battery script in the scene

        if (battery != null)
        {
            if (battery.GetCurrentBattery() >= batteryRequirement)
            {
                return true;
            }
        }

        return false;
    }

    // Turn off all lights controlled by this switch
    public void TurnOffAllLights()
    {
        foreach (GameObject light in lights)
        {
            if (light != null)
            {
                light.SetActive(false);
                audioSource.Play();
                anim.SetBool("isClicked", false);
            }
        }
    }

    // Turn on all lights controlled by this switch
    public void TurnOnAllLights()
    {
        foreach (GameObject light in lights)
        {
            if (light != null)
            {
                light.SetActive(true);
                audioSource.Play();
            }
        }
    }

    // Toggle the state of all lights controlled by this switch
    public void ToggleAllLights()
    {
        anim.SetBool("isClicked", true);
        foreach (GameObject light in lights)
        {
            if (light != null)
            {
                light.SetActive(!light.activeSelf);
                audioSource.Play();
                
            }
        }
    }
}
