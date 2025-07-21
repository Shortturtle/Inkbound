using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnButtonHandler : ButtonClass
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonCheck();

        animator.SetBool("isButtonPressed", isButtonPressed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && (collision.gameObject.CompareTag("Drawing") || collision.gameObject.GetComponent<DrawnBoxBehavior>()))
        {
            isButtonPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && (collision.gameObject.CompareTag("Drawing") || collision.gameObject.GetComponent<DrawnBoxBehavior>()))
        {
            isButtonPressed = false;
        }
    }
}
