using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MantaRayPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ArtistPickUpHandler artist;
    [SerializeField] private DrawingPickUpHandler drawing;
    [SerializeField] private GameObject mantaRayBait;
    [SerializeField] private GameObject followPoint;
    [SerializeField] private bool above;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        artist = GameObject.FindGameObjectWithTag("Artist").GetComponent<ArtistPickUpHandler>();
        drawing = GameObject.FindGameObjectWithTag("Drawing").GetComponent<DrawingPickUpHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (artist.heldItem != null)
        {

            if (artist.heldItem == mantaRayBait)
            {
                followPoint = artist.gameObject;
            }

        }

        else if (drawing.heldItem != null)
        {
            if (drawing.heldItem == mantaRayBait)
            {
                followPoint = drawing.gameObject;
            }
        }

        else if (followPoint != null)
        {
            followPoint = null;
        }

        BoxCast();
    }

    private void FixedUpdate()
    {

        PlatformMove();
    }

    private void PlatformMove()
    {
        if (followPoint != null && !followPoint.transform.IsChildOf(transform))
        {
            if (!above)
            {
                transform.position = Vector2.MoveTowards(transform.position, followPoint.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    private void BoxCast()
    {
        Collider2D hit = Physics2D.OverlapBox((Vector2)transform.position + boxOffset, boxSize, 0f, layerMask);


        if (hit != null && hit.gameObject == followPoint)
        {
            above = true;
        }

        else
        {
            above = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + boxOffset, boxSize);
    }
}
