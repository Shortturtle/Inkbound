using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class ArtistPush : MonoBehaviour
{
    //Player Push varible
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask boxMask;
    private float xInput;
    private GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        BoxCheck();
    }

    private void BoxCheck()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && xInput == (Vector2.right * transform.localScale.x).x)
        {
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;

            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
    }

    private void OnDrawGizmos()
    {
        //raycast boxes
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + offset, (Vector2)transform.position + (Vector2)offset + Vector2.right * transform.localScale.x * distance);
    }

    private void GetInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
    }
}
