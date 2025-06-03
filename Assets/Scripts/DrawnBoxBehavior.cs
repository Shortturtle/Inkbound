using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnBoxBehavior : BoxClass
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocityCap(rb);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

}
