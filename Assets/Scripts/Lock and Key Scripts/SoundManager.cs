using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource effectsSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Makes sure only one Sound manager that persists through scens
    }


    public void PlaySoundEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    // Function to play background music
    public void PlayBackgroundMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // Function to stop background music
    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }
}




//Notes 2 Danish. This Script can be used by:

/*

public AudioClip jumpSound; 

void OnJumpInput()
{
    // ... jumping code ...
    SoundManager.Instance.PlaySoundEffect(jumpSound);
}



public AudioClip coinCollectSound; 

void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        // ... coin collection logic ...
        SoundManager.Instance.PlaySoundEffect(coinCollectSound);
        Destroy(gameObject);
    }
}

*/
