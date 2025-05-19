using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class DrawnClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawnObjectCollision( object CollisionObject)
    {
        if(CollisionObject != null)
        {
            GameObject artist = GameObject.FindGameObjectWithTag("Artist");

            if (CollisionObject.GetType() == typeof(BoxCollider2D))
            {
                Physics2D.IgnoreCollision(artist.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            }

            else if(CollisionObject.GetType() == typeof(CompositeCollider2D))
            {
                Physics2D.IgnoreCollision(artist.GetComponent<BoxCollider2D>(), GetComponent<CompositeCollider2D>());
            }
        }
    }
}
