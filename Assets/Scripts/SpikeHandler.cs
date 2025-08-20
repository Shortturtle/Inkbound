using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHandler : MonoBehaviour
{
    private Material hueShifter;
    private PlayerSwap playerSwap;
    private int currentPlayer;
    [SerializeField] private float hue1;
    [SerializeField] private float hue2;
    // Start is called before the first frame update
    void Start()
    {
        playerSwap = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlayerSwap>();
        hueShifter = GetComponent<Renderer>().sharedMaterial;
        currentPlayer = playerSwap.activePlayer;
        hueShifter.SetFloat("_Hue", hue1);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != playerSwap.activePlayer)
        {
            if (playerSwap.activePlayer == 1)
            {
                hueShifter.SetFloat("_Hue", hue1);
                currentPlayer = playerSwap.activePlayer;
            }

            else if (playerSwap.activePlayer == 2)
            {
                hueShifter.SetFloat("_Hue", hue2);
                currentPlayer = playerSwap.activePlayer;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Artist"))
        {
            collision.gameObject.GetComponent<ArtistDeath>().artistDead = true;
        }
    }
}
