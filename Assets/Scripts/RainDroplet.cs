using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDroplet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float time;
    private float timer;
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }

        RaycastHit2D hit1 = Physics2D.Raycast((Vector2)transform.position - new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0f), Vector2.down, 0.5f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast((Vector2)transform.position + new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0f), Vector2.down, 0.5f, layerMask);

        if(hit1 || hit2)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Artist") || collision.gameObject.CompareTag("Drawing"))
        {
            collision.GetComponent<PlayerController>().Slow();
        }
    }
}
