using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int activePlayer = 1;
    [SerializeField] private PlayerController artist;
    [SerializeField] private PlayerController drawing;
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer drawnBackground;

    private void Start()
    {
        virtualCam = FindFirstObjectByType<CinemachineVirtualCamera>();

        virtualCam.Follow = artist.gameObject.transform;
        artist.enabled = true;
        drawing.enabled = false;
        
        
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
                artist.enabled = false;
                drawing.enabled = true;
                virtualCam.Follow = drawing.gameObject.transform;
                
                activePlayer = 2;
                break;

            case 2:
                artist.enabled = true;
                drawing.enabled = false;
                virtualCam.Follow = artist.gameObject.transform;
                activePlayer = 1;
                break;
        }
    }

    private void ChangeBackgroundTransparency()
    {
        Color tmp = background.color;
        tmp.a = 0f;
        background.color = tmp;
    }
}
