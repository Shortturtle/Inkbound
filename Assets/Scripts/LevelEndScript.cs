using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    private bool artistPresent;
    private bool drawingPresent;
    private bool levelBeat;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private Canvas colouredUI;
    [SerializeField] private Canvas uncolouredUI;
    [SerializeField] AK.Wwise.Event audioStop;
    [SerializeField] AK.Wwise.Event winSFX;
    private GameObject bgMusicHandler;
    // Start is called before the first frame update
    void Start()
    {
        bgMusicHandler = GameObject.FindGameObjectWithTag("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(drawingPresent && artistPresent)
        {
            if (!levelBeat)
            {
                EndLevel();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.CompareTag("Artist"))
        {
            artistPresent = true;
        }

        if (!collision.isTrigger && collision.gameObject.CompareTag("Drawing"))
        {
            drawingPresent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.CompareTag("Artist"))
        {
            artistPresent = false;
        }

        if (!collision.isTrigger && collision.gameObject.CompareTag("Drawing"))
        {
            drawingPresent = false;
        }
    }

    private void EndLevel()
    {
        audioStop.Post(gameObject);
        audioStop.Post(bgMusicHandler);
        winSFX.Post(gameObject);
        levelBeat = true;
        victoryUI.SetActive(true);
        Time.timeScale = 0.0f;
        colouredUI.enabled = false;
        uncolouredUI.enabled = false;
    }
}
