using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPush : MonoBehaviour
{
    [SerializeField] private Vector2 force = new Vector2(100f, 0f);
    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && ((layerMask & (1 << collision.gameObject.layer)) != 0))
        {
            if (collision.isTrigger == false)
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    collision.attachedRigidbody.AddForce(-force, ForceMode2D.Impulse);
                }

                else
                {
                    collision.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }
}
