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
        }

        else if (!isButtonPressed)
        {
            buttonReleased.Invoke();
        }
    }
}
