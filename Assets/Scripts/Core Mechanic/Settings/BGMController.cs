using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isMuted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set initial mute state based on player preferences
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        UpdateMuteState();
        audioSource.Play();
    }

    void Update()
    {
        // Check for user input to toggle mute state
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        // Set the mute state of the audio source
        audioSource.mute = isMuted;
        // Save the mute state to player preferences
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
    }
}
