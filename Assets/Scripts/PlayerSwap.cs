using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerSwap : MonoBehaviour
{
    public int activePlayer = 1;
    [SerializeField] private PlayerController artist;
    [SerializeField] private PlayerController drawing;
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer drawnBackground;
    private Array drawnObjects;

    [SerializeField] [Range(0f, 1f)] public float fadedAlpha; // The transparency level when faded
    private bool isFaded = true;
    [SerializeField] private float fadeDuration;

    private void Start()
    {
        virtualCam = FindFirstObjectByType<CinemachineVirtualCamera>();

        virtualCam.Follow = artist.gameObject.transform;
        artist.enabled = true;
        drawing.enabled = false;
        ToggleOpacity(isFaded);
        PlayerOpacityStart();
        SetAlpha(background, 1f);
        SetAlpha(drawnBackground, 0f);

    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapPlayer();
            isFaded = !isFaded;
            ToggleOpacity(isFaded);
            SetBackgroundOpacity();
        }
    }

    private void SwapPlayer()
    {
        switch (activePlayer)
        {
            case 1:
                artist.enabled = false;
                drawing.enabled = true;
                virtualCam.Follow = drawing.gameObject.transform;
                ChangePlayerOpacity();
                activePlayer = 2;
                break;

            case 2:
                artist.enabled = true;
                drawing.enabled = false;
                virtualCam.Follow = artist.gameObject.transform;
                ChangePlayerOpacity();
                activePlayer = 1;
                break;
        }
    }

    void ToggleOpacity(bool fade)
    {
        GameObject[] drawnObjects = GameObject.FindGameObjectsWithTag("Drawn");

        foreach (GameObject obj in drawnObjects)
        {
            // SpriteRenderer
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = fade ? fadedAlpha : 1f;
                sr.color = c;
            }

            // Tilemap
            Tilemap tilemap = obj.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Color c = tilemap.color;
                c.a = fade ? fadedAlpha : 1f;
                tilemap.color = c;
            }
        }
    }

    private void ChangePlayerOpacity()
    {
        SpriteRenderer tmp;

        switch (activePlayer)
        {
            case 1:
                tmp = artist.gameObject.GetComponent<SpriteRenderer>();
                SetAlpha(tmp, fadedAlpha);

                tmp = drawing.gameObject.GetComponent<SpriteRenderer>();
                SetAlpha(tmp, 1f);

                break;
                

            case 2:
                tmp = artist.gameObject.GetComponent<SpriteRenderer>();
                SetAlpha(tmp, 1f);

                tmp = drawing.gameObject.GetComponent<SpriteRenderer>();
                SetAlpha(tmp, fadedAlpha);

                break;
        }
    }

    private void SetBackgroundOpacity()
    {
        switch (activePlayer)
        {
            case 1:
                SetAlpha(background, 1f);
                SetAlpha(drawnBackground, 0f);

                break;

            case 2:
                SetAlpha(background, 0f);
                SetAlpha(drawnBackground, 1f);

                break;
        }
    }

    private void PlayerOpacityStart()
    {
        SpriteRenderer tmp;

        tmp = artist.gameObject.GetComponent<SpriteRenderer>();
        SetAlpha(tmp, 1f);

        tmp = drawing.gameObject.GetComponent<SpriteRenderer>();
        SetAlpha(tmp, fadedAlpha);
    }

    void SetAlpha(SpriteRenderer sr, float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

}
