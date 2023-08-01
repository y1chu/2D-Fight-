using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] attackSounds;
    public AudioClip[] defendSounds;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the AudioController.");
        }
    }

    public void PlayAttackSound()
    {
        if (attackSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, attackSounds.Length);
            audioSource.PlayOneShot(attackSounds[randomIndex]);
        }
    }

    public void PlayDefendSound()
    {
        if (defendSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, defendSounds.Length);
            audioSource.PlayOneShot(defendSounds[randomIndex]);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No background music clip provided.");
        }
    }
}