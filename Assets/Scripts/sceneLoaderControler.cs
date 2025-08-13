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

        Debug.Log("Loading level: " + sceneName);
        audioStop.Post(gameObject);
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
}


/*

Notes for Danish:

Hey this script is pretty simple, it just uses unity UI-button presets' onclick funiton to bring u to the scene with the name given in the inspector




- Amos

*/