using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Referensi ke komponen AudioSource
    private AudioSource audioSource;

    // Audio Clip yang akan diputar
    public AudioClip myAudioClip;

    void Start()
    {
        // Mendapatkan komponen AudioSource pada objek ini
        audioSource = GetComponent<AudioSource>();

        // Menetapkan audio clip
        audioSource.clip = myAudioClip;

        // Play audio secara otomatis saat permainan dimulai
        PlayAudio();
    }

    // Fungsi untuk memutar audio
    void PlayAudio()
    {
        // Memastikan audio tidak sedang diputar sebelumnya
        if (!audioSource.isPlaying)
        {
            // Memulai pemutaran audio
            audioSource.Play();
        }
    }
}
