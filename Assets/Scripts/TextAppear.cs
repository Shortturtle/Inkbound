using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class TextAppear : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private LayerMask layerMask;
    private Vector2 boxSize;
    private bool show;
    [SerializeField] private PlayerSwap playerSwap;

    // Start is called before the first frame update
    void Start()
    {
        text = this.transform.GetChild(0).gameObject;
        text.SetActive(false);
        playerSwap = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlayerSwap>();
    }

    // Update is called once per frame
    void Update()
    {
        TextShow();
    }

    private void OnValidate()
    {
        boxSize = GetComponent<BoxCollider2D>().size;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && !collision.isTrigger)
        {
            switch (playerSwap.activePlayer)
            {
                case 1:
                    if (collision.gameObject.CompareTag("Artist"))
                    {
                        show = true;
                        Debug.Log("Artist");
                    }

                    break;

                case 2:
                    if (collision.gameObject.CompareTag("Drawing"))
                    {
                        show = true;
                        Debug.Log("Drawing");
                    }

                    break;
            }
        }

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
