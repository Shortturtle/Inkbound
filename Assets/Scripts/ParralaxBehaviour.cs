using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBehaviour : MonoBehaviour
{
    private Vector2 length, startPos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parralaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = (cam.transform.position * (1 - parralaxEffect));
        Vector2 dist = (cam.transform.position * parralaxEffect);

        transform.position = new Vector3(startPos.x + dist.x,startPos.y + dist.y, transform.position.z);

        if(temp.x > startPos.x + length.x)
        {
            startPos.x += length.x;
        }

        else if(temp.x < startPos.x - length.x)
        {
            startPos.x -= length.x;
        }

        if (temp.y > startPos.y + length.y)
        {
            startPos.y += length.y;
        }

        else if (temp.y < startPos.y - length.y)
        {
            startPos.y -= length.y;
        }
    }
}
