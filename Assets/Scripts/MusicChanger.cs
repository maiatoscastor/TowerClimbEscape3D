using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip newMusic; // A nova música a tocar
    public AudioSource musicSource; // Referência ao AudioSource principal

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && newMusic != null && musicSource != null)
        {
            musicSource.clip = newMusic;
            musicSource.Play();
        }
    }
}
