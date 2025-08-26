using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBossDoor : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    private float speed = 5f;
    [SerializeField] private GameObject platformExtendEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.activeSelf == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, platformExtendEnd.transform.position, speed * Time.deltaTime);
        }
    }
}
