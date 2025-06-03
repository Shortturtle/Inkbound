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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(drawingPresent && artistPresent)
        {
            EndLevel();
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
        victoryUI.SetActive(true);
        Time.timeScale = 0.0f;
        colouredUI.enabled = false;
        uncolouredUI.enabled = false;
    }
}
