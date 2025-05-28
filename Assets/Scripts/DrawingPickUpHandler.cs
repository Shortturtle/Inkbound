using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPickUpHandler : PickUpHandlerClass
{
    // Start is called before the first frame update
    void Start()
    {
        pickUpsInRadius = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                PickUpItem();
            }

            else if (heldItem != null)
            {
                PutDownItem();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>() && collision.gameObject.CompareTag("Drawn"))
        {
            pickUpsInRadius.Add(collision.gameObject);
            Debug.Log(pickUpsInRadius);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>() && collision.gameObject.CompareTag("Drawn"))
        {
            pickUpsInRadius.Remove(collision.gameObject);
        }
    }
}
