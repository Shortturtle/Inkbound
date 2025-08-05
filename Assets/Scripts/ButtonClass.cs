using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClass : MonoBehaviour
{
    public bool isButtonPressed;
    public UnityEvent buttonPressed;
    public UnityEvent buttonReleased;
    public AK.Wwise.Event buttonPress;
    public AK.Wwise.Event buttonRelease;
    private float prevState = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonCheck()
    {

        if (isButtonPressed)
        {
            buttonPressed.Invoke();

            if (prevState == 0) 
            { 
                prevState = 1;
                buttonPress.Post(gameObject);
            }
        }

        else if (!isButtonPressed)
        {
            buttonReleased.Invoke();

            if (prevState == 1)
            {
                prevState = 0;
                buttonRelease.Post(gameObject);
            }
        }
    }
}
