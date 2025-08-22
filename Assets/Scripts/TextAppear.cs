using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TextAppear : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private LayerMask layerMask;
    private Vector2 boxSize;
    [SerializeField] private bool BoxSizeUpdate = true;
    private bool show;
    private List<GameObject> playerList = new List<GameObject>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && ((layerMask & (1 << collision.gameObject.layer)) != 0))
        {
            if (collision.isTrigger == false)
            {
                playerList.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && ((layerMask & (1 << collision.gameObject.layer)) != 0))
        {
            if (collision.isTrigger == false)
            {
                playerList.Remove(collision.gameObject);
            }
        }
    }

    private void TextShow()
    {
        if (playerList.Count <= 0)
        {
            show = false;
        }

        else
        {
            show = true;
        }

        text.SetActive(show);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
