using System.Collections.Generic;
using UnityEngine;

// If you need the audiomanager, use AudioManager.GetInstance()
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private float volume = 0.2f;
    private float uiVolume = 0.4f;
    public AudioSource playerAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource uiAudioSource;

    public enum SoundType
    {
        footstep,
        impact,
        death,
        dash,
        doubleJump,
        uiOnClick,
        uiOnSelect,
        uiOnStart
    }

    public enum MusicType
    {
        introMusic,
        exploreMusic,
        reversalMusic
    }

    public List<AudioClip> footsteps;
    public AudioClip fallImpact;
    public AudioClip doubleJump;
    public AudioClip dash;
    public AudioClip death;
    public AudioClip uiOnClick;
    public AudioClip uiOnSelect;
    public AudioClip uiOnStart;

    public AudioClip introMusic;
    public AudioClip exploreMusic;
    public AudioClip reversalMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        musicAudioSource.volume = volume;
        playerAudioSource.volume = volume;
        uiAudioSource.volume = uiVolume;
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.footstep:
                // Play random footstep
                int i = Random.Range(0, footsteps.Count);
                playerAudioSource.PlayOneShot(footsteps[i], volume);
                break;

            case SoundType.impact:
                playerAudioSource.PlayOneShot(fallImpact, volume);
                break;

            case SoundType.death:
                playerAudioSource.PlayOneShot(death, volume);
                break;

            case SoundType.dash:
                playerAudioSource.PlayOneShot(dash, volume);
                break;

            case SoundType.doubleJump:
                playerAudioSource.PlayOneShot(doubleJump, volume);
                break;

            case SoundType.uiOnClick:
                uiAudioSource.PlayOneShot(uiOnClick, volume);
                break;

            case SoundType.uiOnSelect:
                uiAudioSource.PlayOneShot(uiOnSelect, volume);
                break;

            case SoundType.uiOnStart:
                uiAudioSource.PlayOneShot(uiOnStart, volume);
                break;

            default:
                break;
        }
    }

    public void PlayMusic(MusicType musicType)
    {
        switch (musicType)
        {
            case MusicType.introMusic:
                musicAudioSource.clip = introMusic;
                musicAudioSource.volume = volume;
                musicAudioSource.Play();
                break;

            case MusicType.exploreMusic:
                musicAudioSource.clip = exploreMusic;
                //Set appropriate volume for music, this is needed for the other sounds to be heard
                musicAudioSource.volume = volume / 3;
                musicAudioSource.Play();
                break;

            case MusicType.reversalMusic:
                musicAudioSource.clip = reversalMusic;
                //Set appropriate volume for music, this is needed for the other sounds to be heard
                musicAudioSource.volume = volume / 3;
                musicAudioSource.Play();
                break;

            default:
                break;
        }
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
}
