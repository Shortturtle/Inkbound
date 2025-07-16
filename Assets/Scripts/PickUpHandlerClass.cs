using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickUpHandlerClass : MonoBehaviour
{
    public GameObject heldItem;
    [HideInInspector] public List<GameObject> pickUpsInRadius;
    public GameObject pickUpLocation;
    public bool doorNearby;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabItem(GameObject item)
    {
        heldItem = item;
        item.transform.SetParent(pickUpLocation.transform);
        item.transform.position = new Vector3(pickUpLocation.transform.position.x, pickUpLocation.transform.position.y + (item.GetComponent<Collider2D>().bounds.size.y / 2), pickUpLocation.transform.position.z);
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void DropItem(GameObject item)
    {
        item.transform.SetParent(null);
        item.transform.position = new Vector3(pickUpLocation.transform.position.x + (item.GetComponent<Collider2D>().bounds.size.x / 2), pickUpLocation.transform.position.y, pickUpLocation.transform.position.z);
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        heldItem = null;
    }

    public void PickUpItem()
    {
        if (pickUpsInRadius.Count > 0)
        {
            if (pickUpsInRadius.Count == 1)
            {
                GrabItem(pickUpsInRadius[0]);
            }

            else
            {
                float leastDistance = Vector2.Distance(pickUpsInRadius[0].transform.position, this.gameObject.transform.position);
                float tempDistance;
                int temp = 0;

                for (int i = 0; i < pickUpsInRadius.Count; i++)
                {
                    tempDistance = Vector2.Distance(pickUpsInRadius[i].transform.position, this.gameObject.transform.position);

                    if (tempDistance < leastDistance)
                    {
                        temp = i;
                        leastDistance = tempDistance;
                    }
                }

                GrabItem(pickUpsInRadius[temp]);
            }
        }
    }

    public void PutDownItem()
    {
        if (heldItem != null)
        {
            DropItem(heldItem);
        }
    }   
    
    public void FailSafe()
    {
        if (heldItem != null && heldItem.transform.parent == null)
        {
            heldItem.transform.SetParent(pickUpLocation.transform);
            heldItem.transform.position = new Vector3(pickUpLocation.transform.position.x, pickUpLocation.transform.position.y + (heldItem.GetComponent<Collider2D>().bounds.size.y / 2), pickUpLocation.transform.position.z);
        }
    }
}
