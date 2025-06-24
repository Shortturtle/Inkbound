using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PopUpSpikes : MonoBehaviour
{
    [SerializeField] private GameObject popOutLocation;
    private BoxCollider2D boxCollider2D;
    private Vector3 popOutPosition;
    private Vector3 popInPosition;

    [SerializeField] private float popOutValue;
    [SerializeField] private float popOutTimer;
    [SerializeField] private float activeValue;
    [SerializeField] private float activeTimer;
    [SerializeField] private bool isActive;

    [SerializeField] private float lerpValue;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        popOutPosition = popOutLocation.transform.position + new Vector3 (0f, boxCollider2D.bounds.size.y/2, 0f);
        popInPosition = transform.position;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            popOutTimer -= Time.deltaTime;
        }

        if (isActive)
        {
            activeTimer -= Time.deltaTime;
        }
        
        if (popOutTimer <= 0 && !isActive)
        {
            PopOut();
        }

        else if (activeTimer <= 0 && isActive)
        {
            PopIn();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Artist"))
        {
            collision.gameObject.GetComponent<ArtistDeath>().artistDead = true;
        }
    }

    private void PopOut()
    {
        transform.position = Vector3.Lerp(transform.position, popOutPosition, lerpValue);
        if ((transform.position == popOutPosition) && !isActive)
        {
            activeTimer = activeValue;
            isActive = true;
        }
    }

    private void PopIn()
    {
        transform.position = Vector3.Lerp(transform.position, popInPosition , lerpValue);
        if ((transform.position == popInPosition) && isActive)
        {
            popOutTimer = popOutValue;
            isActive = false;
        }
    }
}
