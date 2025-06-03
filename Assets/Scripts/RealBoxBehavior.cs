using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class RealBoxBehavior : BoxClass
{
    [SerializeField] private PlayerSwap playerSwap;
    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private Vector2 horizontalBoxCastSize;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask boxCollisionLayers;
   

    private Rigidbody2D rb;
    [SerializeField] private PhysicsMaterial2D push;
    [SerializeField] private PhysicsMaterial2D noPush;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BoxCheck();

        VelocityCap(rb);
    }
    
    private void BoxCheck()
    {
        Physics2D.queriesStartInColliders = false;
        Collider2D rightHit = Physics2D.OverlapBox(transform.position + new Vector3(boxCollider.size.x / 2, 0, 0), boxCastSize, 0f, boxCollisionLayers);
        Collider2D leftHit = Physics2D.OverlapBox(transform.position - new Vector3(boxCollider.size.x / 2, 0, 0), boxCastSize, 0f, boxCollisionLayers);
        Collider2D TopHit = Physics2D.OverlapBox(transform.position + new Vector3(0, boxCollider.size.y / 2, 0), horizontalBoxCastSize, 90f, boxCollisionLayers);

        
        if (rightHit != null && rightHit.gameObject.CompareTag("Drawing") && playerSwap.activePlayer == 2)
        {
            rb.sharedMaterial = noPush;
            rb.mass = 100;
        }

        else if (leftHit != null && leftHit.gameObject.CompareTag("Drawing") && playerSwap.activePlayer == 2)
        {
            rb.sharedMaterial = noPush;
            rb.mass = 100;
        }

        else
        {
            rb.sharedMaterial = push;
            rb.mass = 4;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(boxCollider.size.x / 2, 0, 0), boxCastSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position - new Vector3(boxCollider.size.x / 2, 0, 0), boxCastSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, boxCollider.size.y / 2, 0), horizontalBoxCastSize);
    }

}
