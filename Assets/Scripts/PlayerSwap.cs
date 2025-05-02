using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int activePlayer = 1;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    private void Start()
    {
        player1.enabled = true;
        player2.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapPlayer();
        }
    }

    private void SwapPlayer()
    {
        switch (activePlayer)
        {
            case 1:
                player1.enabled = false;
                player2.enabled = true;
                activePlayer = 2;
                break;

            case 2:
                player1.enabled = true;
                player2.enabled = false;
                activePlayer = 1;
                break;
        }
    }
}
