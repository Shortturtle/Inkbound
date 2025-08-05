using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaterColumn : MonoBehaviour
{
    private Vector2 startPosition1;
    private Vector2 startPosition2;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem waterfallParticles;
    [SerializeField] private ParticleSystem waterfallParticlesInstance;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private float temp;
    private float mult;
    private float newHeight;
    private float newScale;
    private float endposition;
    private float sizeX;

    private Material hueShifter;
    [SerializeField] private float hue1;
    [SerializeField] private float hue2;
    [SerializeField] private int currentPlayer;
    [SerializeField] private PlayerSwap playerswap;
    [SerializeField] private AK.Wwise.Event waterfallAudio;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        sizeX = boxCollider.size.x;

        startPosition1 = new Vector2(transform.position.x - (sizeX / 2), transform.position.y);
        startPosition2 = new Vector2(transform.position.x + (sizeX / 2), transform.position.y);

        hueShifter = GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(startPosition1, Vector2.down, 999f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(startPosition2, Vector2.down, 999f, layerMask);

        Debug.DrawLine(startPosition1, hit1.point, Color.cyan);
        Debug.DrawLine(startPosition2, hit2.point, Color.cyan);

        if (hit1.point.y < hit2.point.y && endposition != hit2.point.y)
        {
            temp = Mathf.Abs(startPosition2.y - hit2.point.y);

            mult = temp / spriteRenderer.size.y;

            newHeight = spriteRenderer.size.y * mult;

            newScale = (newHeight / spriteRenderer.size.y);

            transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);

            endposition = hit2.point.y;

            ParticleSpawn();
        }

        else if(hit2.point.y < hit1.point.y && endposition != hit1.point.y)
        {
            temp = Mathf.Abs(startPosition2.y - hit1.point.y);

            mult = temp / spriteRenderer.size.y;

            newHeight = spriteRenderer.size.y * mult;

            newScale = (newHeight / spriteRenderer.size.y);

            transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);

            endposition = hit1.point.y;

            ParticleSpawn();
        }

        else if(endposition != hit1.point.y && endposition != hit2.point.y)
        {
            temp = Mathf.Abs(startPosition1.y - hit2.point.y);

            mult = temp / spriteRenderer.size.y;

            newHeight = spriteRenderer.size.y * mult;

            newScale = (newHeight / spriteRenderer.size.y);

            transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);

            endposition = hit2.point.y;

            ParticleSpawn();
        }

        if (currentPlayer != playerswap.activePlayer)
        {
            if (playerswap.activePlayer == 1)
            {
                hueShifter.SetFloat("_Hue", hue1);
                currentPlayer = playerswap.activePlayer;
            }

            else if (playerswap.activePlayer == 2)
            {
                hueShifter.SetFloat("_Hue", hue2);
                currentPlayer = playerswap.activePlayer;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.CompareTag("Drawing"))
        {
            collision.gameObject.GetComponent<DrawingDeath>().drawingDead = true;
        }
    }

    private void ParticleSpawn()
    {
        if (waterfallParticlesInstance != null)
        {
            waterfallParticlesInstance.gameObject.transform.position = new Vector2(transform.position.x, endposition);
            waterfallAudio.Post(waterfallParticlesInstance.gameObject);
        }

        else
        {
            waterfallParticlesInstance = Instantiate(waterfallParticles, new Vector2(transform.position.x, endposition), Quaternion.identity);
        }
    }
}
