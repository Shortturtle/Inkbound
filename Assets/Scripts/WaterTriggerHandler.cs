using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask waterMask;
    [SerializeField] private GameObject splashParticles;
    private Material hueShifter;
    [SerializeField] private float hue1 = 0.56f;
    [SerializeField] private float hue2 = 1f;
    [SerializeField] private int currentPlayer;
    [SerializeField] private PlayerSwap playerswap;

    private EdgeCollider2D edgeColl;

    private void Update()
    {
        if (currentPlayer != playerswap.activePlayer)
        {
            if (playerswap.activePlayer == 1)
            {
                hueShifter.SetFloat("_Hue", hue1);
                currentPlayer = playerswap.activePlayer;
            }

            else if (playerswap.activePlayer == 2)
            {
                hueShifter.SetFloat("_Hue", hue2);
                currentPlayer = playerswap.activePlayer;
            }
        }
    }
    private void Awake()
    {
        hueShifter = GetComponent<Renderer>().sharedMaterial;

        playerswap = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlayerSwap>();

        edgeColl = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((waterMask.value & (1 << collision.gameObject.layer)) > 0 && !collision.isTrigger)
        {
            Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();

            if (rb.gameObject.CompareTag("Drawing"))
            {
                rb.gameObject.GetComponent<DrawingDeath>().drawingDead = true;
                Debug.Log("Die");
            }

            if(rb != null)
            {
                Vector2 objectHitPos = collision.transform.position;
                //Instantiate(splashParticles,objectHitPos, Quaternion.identity);
            }
        }
    }
}
