using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoaderControler : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        Debug.Log("Loading level: " + sceneName); 
        SceneManager.LoadScene(sceneName);
    }
}


/*

Notes for Danish:

Hey this script is pretty simple, it just uses unity UI-button presets' onclick funiton to bring u to the scene with the name given in the inspector




- Amos

*/