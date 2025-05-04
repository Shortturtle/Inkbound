using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBoxBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private PhysicsMaterial2D push;
    [SerializeField] private PhysicsMaterial2D noPush;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Drawing"))
        {
            rb.sharedMaterial = noPush;
        }

        else
        {
            rb.sharedMaterial = push;
        }
    }

}
