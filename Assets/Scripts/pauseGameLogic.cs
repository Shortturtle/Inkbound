using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGameLogic : MonoBehaviour
{
    public static bool GameIsPaused = false; 
    public GameObject pauseMenuUI; 

    // ------------------------------ Pause Resume Toggle ------------------------//    
    public void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    //-------------------------------- Pause Function ----------------------------//
    void Pause()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        GameIsPaused = true;
    }
    //------------------------------- Resume Function ---------------------------//
    void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        GameIsPaused = false;
    }
    //------------------------------ Change Scene Funciton (not done)---------------------//




    /* Notes to Danish
    
    - Drag and drop this script in to an empty space in the inspector 
    - Drag your "PauseMenu" GameObject (the container for your pause menu UI) from the Hierarchy window into this "Pause Menu UI" slot in the Inspector

    - "On Click ()" list add a new event.
    - Drag the "PauseButton" GameObject into it. 
    - In the function dropdown select "PauseGame" -> "TogglePause".
    
    
    - Amos
    
    
    */
}