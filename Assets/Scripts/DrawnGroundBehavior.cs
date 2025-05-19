using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnGroundBehavior : DrawnClass
{

    // Start is called before the first frame update
    void Start()
    {
        DrawnObjectCollision(this.gameObject.GetComponent<CompositeCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
