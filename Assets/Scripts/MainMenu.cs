using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject worldSelect;
    [SerializeField] private GameObject world1LevelSelect;
    [SerializeField] private GameObject world2LevelSelect;
    [SerializeField] private GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        worldSelect.SetActive(false);
        world1LevelSelect.SetActive(false);
        world2LevelSelect.SetActive(false);
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        worldSelect.SetActive(true);
    }

    public void BackToMainMenu()
    {
        worldSelect.SetActive(false);
    }

    public void WorldSelect(int num)
    {
        switch (num)
        {
            case 1:
                world1LevelSelect.SetActive(true);
                break;

            case 2:
                world2LevelSelect.SetActive(true);
                break;
        }
    }

    public void BackToWorldSelect(int num)
    {
        switch (num)
        {
            case 1:
                world1LevelSelect.SetActive(false);
                break;

            case 2:
                world2LevelSelect.SetActive(false);
                break;
        }
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void ExitCredits()
    {
        credits.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
