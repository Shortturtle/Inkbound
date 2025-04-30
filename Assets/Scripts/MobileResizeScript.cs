using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileResizeScript : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Vector2 targetResolution; //the resolution the game is designed for
    [SerializeField] private int ppu;

    void Awake()
    {

        cam = GetComponent<Camera>();

        float targetAspect = targetResolution.x / targetResolution.y;
        float currentAspect = Screen.width / (float)Screen.height;

        if (currentAspect < targetAspect)
        {

            float scalingWidth = Screen.width / targetResolution.x;

            float camSize = ((Screen.height / 2) / scalingWidth) / ppu;
            cam.orthographicSize = camSize;
        }
    }

    bool HasNotch()
    {

        float notchHeight = Screen.height - Screen.safeArea.height - Screen.safeArea.y;

        if (notchHeight > 0f)
            return true;

        return false;
    }

}
