using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool drawingThere;
    private bool artistThere;
    private bool swordThere;
    private bool allIn;
    [SerializeField] private GameObject platformExtendEnd;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drawingThere && artistThere && swordThere)
        {
            allIn = true;
        }

        if (allIn)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, platformExtendEnd.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!allIn)
        {
            if (collision != null && ((layerMask & (1 << collision.gameObject.layer)) != 0))
            {
                if (collision.isTrigger == false)
                {
                    if (collision.gameObject.CompareTag("Artist"))
                    {
                        artistThere = true;
                    }

                    if (collision.gameObject.CompareTag("Drawing"))
                    {
                        drawingThere = true;
                    }
                }
            }

            if (collision != null && collision.gameObject.GetComponent<SwordClass>() != null)
            {
                swordThere = true;
            }
        }
    }
}
