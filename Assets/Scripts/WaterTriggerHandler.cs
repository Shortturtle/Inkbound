using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask waterMask;
    [SerializeField] private GameObject splashParticles;

    private EdgeCollider2D edgeColl;

    private InteractiveWater water;

    private void Awake()
    {
        edgeColl = GetComponent<EdgeCollider2D>();

        water = GetComponent<InteractiveWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((waterMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();

            if (rb.gameObject.CompareTag("Drawing"))
            {
                rb.gameObject.GetComponent<DrawingDeath>().drawingDead = true;
            }

            if(rb != null)
            {
                Vector2 objectHitPos = collision.transform.position;
                //Instantiate(splashParticles,objectHitPos, Quaternion.identity);
            }
        }
    }
}
