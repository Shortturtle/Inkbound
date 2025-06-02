using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendingPlatformBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 platformExtendStart;
    [SerializeField] private GameObject platformExtendEnd;
    // Start is called before the first frame update
    void Start()
    {
        platformExtendStart = gameObject.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, platformExtendEnd.transform.position, speed * Time.deltaTime);
    }

    public void ButtonReleased()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, platformExtendStart, speed * Time.deltaTime);
    }
}
