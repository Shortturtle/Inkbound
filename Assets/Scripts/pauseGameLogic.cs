using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGameLogic : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private Canvas colouredUI;
    [SerializeField] private Canvas uncolouredUI;
    [SerializeField] private PlayerSwap playerswap;
    [SerializeField] private SoundManager soundManager;

    private void Awake()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        soundManager.musicSource.pitch = 1f;
        soundManager.musicSource.volume = 0.8f;
        if (playerswap.activePlayer == 1)
        {
            colouredUI.enabled = true;
        }

        else
        {
            uncolouredUI.enabled = true;
        }
    }

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
        colouredUI.enabled = false;
        uncolouredUI.enabled = false;
        soundManager.musicSource.pitch = 0.75f;
        soundManager.musicSource.volume = 0.5f;
    }
    //------------------------------- Resume Function ---------------------------//
    void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        GameIsPaused = false;
        soundManager.musicSource.pitch = 1f;
        soundManager.musicSource.volume = 0.8f;
        if (playerswap.activePlayer == 1)
        {
            colouredUI.enabled = true;
        }

        else if (playerswap.activePlayer == 2)
        {
            uncolouredUI.enabled = true;
        }
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