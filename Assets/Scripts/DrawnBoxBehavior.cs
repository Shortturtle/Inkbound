using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnBoxBehavior : DrawnClass
{

    // Start is called before the first frame update
    void Start()
    {
        DrawnObjectCollision(this.gameObject.GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

}
