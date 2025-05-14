using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Logic : MonoBehaviour
{
    public string targetKeyTag;
    public bool isUnlocked = false;
    public UnityEngine.Events.UnityEvent onTargetEnter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) // For 2D triggers
    {
        
        if (!isUnlocked && other.CompareTag(targetKeyTag))
        {
            isUnlocked = true;
            other.gameObject.SetActive(false);
        }
    }
}
