using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArtistPickUpHandler : PickUpHandlerClass
{
    private RealDoorHandler realDoorHandler;
    [SerializeField] private AK.Wwise.Event PickUp;
    [SerializeField] private AK.Wwise.Event PutDown;
    [SerializeField] private AK.Wwise.Event NoPickUp;
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
            if(heldItem == null)
            {
                PickUpItem();
            }

            else if (heldItem != null)
            {
                if (heldItem.GetComponent<KeyClass>() && doorNearby == true)
                {
                    realDoorHandler.KeyInserted();
                    Destroy(heldItem);
                    heldItem = null;
                }
                else
                {
                    PutDownItem();
                }
            }
        }

        FailSafe();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>() && !collision.gameObject.GetComponent<PickUpClass>().held && (!collision.gameObject.CompareTag("Drawn") || collision.gameObject.CompareTag("Both")))
        {
            pickUpsInRadius.Add(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<RealDoorHandler>())
        {
            doorNearby = true;
            realDoorHandler = collision.GetComponent<RealDoorHandler>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>()  && (!collision.gameObject.CompareTag("Drawn") || collision.gameObject.CompareTag("Both")))
        {
            pickUpsInRadius.Remove(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<RealDoorHandler>())
        {
            doorNearby = false;
            realDoorHandler = null;
        }
    }

    public void InteractButtonMobile()
    {
        if (heldItem == null)
        {
            if (pickUpsInRadius.Count > 0)
            {
                PickUp.Post(gameObject);
                PickUpItem();
            }

            else
            {
                NoPickUp.Post(gameObject);
            }
        }

        else if (heldItem != null)
        {
            if (heldItem.GetComponent<KeyClass>() && doorNearby == true)
            {
                PutDown.Post(gameObject);
                realDoorHandler.KeyInserted();
                Destroy(heldItem);
                heldItem = null;
            }
            else
            {
                PutDown.Post(gameObject);
                PutDownItem();
            }
        }
    }
}
