using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnGroundBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject artist = GameObject.FindGameObjectWithTag("Artist");
        Physics2D.IgnoreCollision(artist.GetComponent<BoxCollider2D>(), GetComponent<CompositeCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
