using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCollisionHandler : MonoBehaviour
{
    private List<Collider2D> drawnColliders;
    // Start is called before the first frame update
    void Start()
    {
        drawnColliders = new List<Collider2D>();

        BoxCollider2D artist = GameObject.FindGameObjectWithTag("Artist").GetComponent<BoxCollider2D>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach(GameObject obj in allObjects)
        {
            if (obj.CompareTag("Drawn"))
            {
                drawnColliders.Add(obj.GetComponent<Collider2D>());
            }
        }

        foreach (Collider2D i in drawnColliders)
        {
            if (i.GetType() == typeof(TilemapCollider2D))
            {
                Physics2D.IgnoreCollision(i.gameObject.GetComponent<CompositeCollider2D>(), artist);
                
            }
            Physics2D.IgnoreCollision(i, artist);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
