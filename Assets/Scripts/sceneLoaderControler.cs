using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class sceneLoaderControler : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private UnityEngine.UI.Image paperMask;
    [SerializeField] private float transitionTime;
    [SerializeField] private AK.Wwise.Event audioStop;
    private GameObject bgMusicHandler;
    public void LoadLevel(string sceneName)
    {
       StartCoroutine(LoadLevelCoroutine(sceneName));
    }

    IEnumerator LoadLevelCoroutine(string sceneName)
    {
        Time.timeScale = 1.0f;
        transition.SetTrigger("Start");
        Debug.Log("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        audioStop.Post(bgMusicHandler);
        Debug.Log("Loading level: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        bgMusicHandler = GameObject.FindGameObjectWithTag("AudioManager");
    }
}


/*

Notes for Danish:

Hey this script is pretty simple, it just uses unity UI-button presets' onclick funiton to bring u to the scene with the name given in the inspector




- Amos

*/