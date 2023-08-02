using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] attackSounds;
    public AudioClip[] defendSounds;
    public AudioClip backgroundMusic;
    public float soundCooldown = 0.5f; // Cooldown time in seconds

    private AudioSource soundEffectsSource;
    private AudioSource musicSource;
    private float lastSoundTime;

    private void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length < 2)
        {
            Debug.LogError("Two AudioSources components are required on the AudioController for background music and sound effects.");
        }
        else
        {
            soundEffectsSource = audioSources[0];
            musicSource = audioSources[1];
        }
    }

    public void PlayAttackSound()
    {
        if (attackSounds.Length > 0 && Time.time >= lastSoundTime + soundCooldown)
        {
            Debug.Log("Playing attack sound");
            int randomIndex = Random.Range(0, attackSounds.Length);
            soundEffectsSource.PlayOneShot(attackSounds[randomIndex]);
            lastSoundTime = Time.time;
        }
    }

    public void PlayDefendSound()
    {
        Debug.Log("Playing defend sound");
        if (defendSounds.Length > 0 && Time.time >= lastSoundTime + soundCooldown)
        {
            int randomIndex = Random.Range(0, defendSounds.Length);
            soundEffectsSource.PlayOneShot(defendSounds[randomIndex]);
            lastSoundTime = Time.time;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("No background music clip provided.");
        }
    }
}