using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private float volume = 0.2f;

    private AudioSource playerAudioSource;
    public GameObject player;
    public List<AudioClip> footsteps;
    public AudioClip fallImpact;
    public AudioClip deathSound;

    //class cannot be instantiated because we only want ONE manager
    //if you need the audiomanager, use AudioManager.GetInstance()
    private AudioManager()
    {

    }

    void Start()
    {
        instance = this;
        playerAudioSource = player.GetComponent<AudioSource>();
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlayFootstep()
    {
        //set random footstep
        int i = Random.Range(0, 4);
        playerAudioSource.PlayOneShot(footsteps[i], volume);
    }

    public void PlayImpact()
    {
        playerAudioSource.PlayOneShot(fallImpact, volume);
    }

    public void PlayDeath()
    {
        playerAudioSource.PlayOneShot(deathSound, volume);
    }
}
