using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class TextAppear : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private LayerMask layerMask;
    private Vector2 boxSize;
    [SerializeField] private bool BoxSizeUpdate = true;
    private bool show;

    // Start is called before the first frame update
    void Start()
    {
        text = this.transform.GetChild(0).gameObject;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        TextShow();
    }

    private void OnValidate()
    {
        boxSize = GetComponent<BoxCollider2D>().size;
        BoxSizeUpdate = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && !collision.isTrigger)
        {
            show = true;
        }

        else
        {
            show = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }

    private void TextShow()
    {
        text.SetActive(show);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
