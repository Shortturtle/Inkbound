using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.gameObject.CompareTag("Artist"))
            {
                collision.gameObject.GetComponent<ArtistDeath>().artistDead = true;
            }

            if (collision.gameObject.CompareTag("Drawing"))
            {
                collision.gameObject.GetComponent<DrawingDeath>().drawingDead = true;
            }
        }
    }
}
