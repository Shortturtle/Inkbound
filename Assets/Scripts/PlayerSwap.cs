
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
    [SerializeField] private GameObject artistArrow;
    [SerializeField] private PlayerController drawing;
    [SerializeField] private GameObject drawingArrow;
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private SpriteRenderer artistSpriteRenderer;
    [SerializeField] private SpriteRenderer drawingSpriteRenderer;
    [SerializeField] private GameObject colouredBG;
    private List<SpriteRenderer> background;
    [SerializeField] private GameObject uncolouredBG;
    private List<SpriteRenderer> drawnBackground;
    [SerializeField] private Canvas colouredUI;
    [SerializeField] private Canvas uncolouredUI;
    private Array drawnObjects;

    [SerializeField] [Range(0f, 1f)] public float fadedAlpha; // The transparency level when faded
    private bool isFaded = true;
    [SerializeField] private float fadeDuration;

    private void Start()
    {
        BGSort();

        virtualCam = FindFirstObjectByType<CinemachineVirtualCamera>();
        artistSpriteRenderer = artist.gameObject.GetComponent<SpriteRenderer>();
        drawingSpriteRenderer = drawing.gameObject.GetComponent<SpriteRenderer>();

        virtualCam.Follow = artist.gameObject.transform;
        artist.enabled = true;
        artistArrow.SetActive(true);
        drawing.enabled = false;
        drawingArrow.SetActive(false);
        colouredUI.enabled = true;
        uncolouredUI.enabled = false;
        ToggleOpacity(isFaded);
        PlayerOpacityStart();
        SetAlphaList(background, 1f);
        SetAlphaList(drawnBackground, 0f);

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
                artistArrow.SetActive(false);
                drawing.enabled = true;
                drawingArrow.SetActive(true);
                virtualCam.Follow = drawing.gameObject.transform;
                ChangePlayerOpacity();
                colouredUI.enabled = false;
                uncolouredUI.enabled = true;
                activePlayer = 2;
                break;

            case 2:
                artist.enabled = true;
                artistArrow.SetActive(true);
                drawing.enabled = false;
                drawingArrow.SetActive(false);
                virtualCam.Follow = artist.gameObject.transform;
                ChangePlayerOpacity();
                colouredUI.enabled = true;
                uncolouredUI.enabled = false;
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
        GameObject tempItem = null;

        switch (activePlayer)
        {
            case 1:
                SetAlpha(artistSpriteRenderer, fadedAlpha);
                artistSpriteRenderer.sortingOrder = -1;
                tempItem = artist.GetComponent<ArtistPickUpHandler>().heldItem;
                if(tempItem != null)
                {
                    SetAlpha(tempItem.GetComponent<SpriteRenderer>(), fadedAlpha);
                }

                SetAlpha(drawingSpriteRenderer, 1f);
                drawingSpriteRenderer.sortingOrder = 0;

                break;
                

            case 2:
                SetAlpha(artistSpriteRenderer, 1f);
                artistSpriteRenderer.sortingOrder = 0;
                tempItem = artist.GetComponent<ArtistPickUpHandler>().heldItem;
                if (tempItem != null)
                {
                    SetAlpha(tempItem.GetComponent<SpriteRenderer>(), 1f);
                }

                SetAlpha(drawingSpriteRenderer, fadedAlpha);
                drawingSpriteRenderer.sortingOrder = -1;

                break;
        }
    }

    private void SetBackgroundOpacity()
    {
        switch (activePlayer)
        {
            case 1:
                SetAlphaList(background, 1f);
                SetAlphaList(drawnBackground, 0f);

                break;

            case 2:
                SetAlphaList(background, 0f);
                SetAlphaList(drawnBackground, 1f);

                break;
        }
    }

    private void PlayerOpacityStart()
    {
        Physics2D.IgnoreCollision(artist.GetComponent<BoxCollider2D>(), drawing.GetComponent<BoxCollider2D>());

        SetAlpha(artistSpriteRenderer, 1f);
        artistSpriteRenderer.sortingOrder = 0;

        SetAlpha(drawingSpriteRenderer, fadedAlpha);
        drawingSpriteRenderer.sortingOrder = -1;
    }

    void SetAlpha(SpriteRenderer sr, float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    void SetAlphaList(List<SpriteRenderer> srl, float alpha)
    {
        foreach(SpriteRenderer sr in srl)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    private void BGSort()
    {
        background = new List<SpriteRenderer>();
        drawnBackground = new List<SpriteRenderer>();

        for (int i = 0; i < colouredBG.transform.childCount; i++)
        {
            GameObject tempGO = colouredBG.transform.GetChild(i).gameObject;
            SpriteRenderer tempSR = tempGO.GetComponent<SpriteRenderer>();
            background.Add(tempSR);

            for (int j = 0; j < tempGO.transform.childCount; j++)
            {
                tempSR = tempGO.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>();
                background.Add(tempSR);
                Debug.Log("added");
            }
                Debug.Log("added");
        }

        for (int i = 0; i < uncolouredBG.transform.childCount; i++)
        {
            GameObject tempGO = uncolouredBG.transform.GetChild(i).gameObject;
            SpriteRenderer tempSR = tempGO.GetComponent<SpriteRenderer>();
            drawnBackground.Add(tempSR);

            for (int j = 0; j < tempGO.transform.childCount; j++)
            {
                tempSR = tempGO.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>();
                drawnBackground.Add(tempSR);
                Debug.Log("added");
            }
            Debug.Log("added");
        }
    }

    public void SwapCharacterMobile()
    {
        SwapPlayer();
        isFaded = !isFaded;
        ToggleOpacity(isFaded);
        SetBackgroundOpacity();
    }
}
