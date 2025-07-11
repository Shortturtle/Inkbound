using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPlatformHandler : MonoBehaviour
{
    [SerializeField] private float bufferTime;
    [SerializeField] private float breakDelayTime;
    [SerializeField] private float buffer;
    [SerializeField] private float breakDelay;

    private Animator animator;
    private BoxCollider2D boxCollider;

    [SerializeField] private bool open;
    [SerializeField] private bool cracking;

    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 boxCastSize;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            if (cracking)
            {
                Reset();
            }

            else if (!cracking)
            {
                buffer -= Time.deltaTime;
            }

            if (buffer <= 0)
            {
                boxCollider.enabled = true;
                open = false;
                cracking = false;
            }

        }

        if (cracking)
        {
            breakDelay -= Time.deltaTime;

            if (breakDelay <= 0)
            {
                boxCollider.enabled = false;
                open = true;
            }
        }

        animator.SetBool("Open", open);

        BoxCast();
    }

    private void Reset()
    {
        buffer = bufferTime;
        cracking = false;
    }


    private void BoxCast()
    {
        Collider2D hit = Physics2D.OverlapBox((Vector2)transform.position + offset, boxCastSize, 0);

        if (hit.gameObject.tag == "Artist")
        {
            if (!cracking && !open)
            {
                breakDelay = breakDelayTime;
                cracking = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + (Vector3)offset, boxCastSize);
    }
}
