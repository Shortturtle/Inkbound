using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BGMusicHandler : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        soundManager.musicSource.volume = 0.8f;
        soundManager.PlayBackgroundMusic(bgMusic, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
