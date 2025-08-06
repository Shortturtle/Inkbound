using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BGMusicHandler : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AK.Wwise.Event BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
