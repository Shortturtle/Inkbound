using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClass : MonoBehaviour
{
    public float maxVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VelocityCap(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), rb.velocity.y);
    }
}
