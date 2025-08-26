using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForestBossLogic : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;
    private int i;

    [SerializeField] private BoxCollider2D collision;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float bufferTime;
    [SerializeField] private float stunTime;
    [SerializeField] private float cooldownTime;
    [SerializeField] private BoxCollider2D push;
    [SerializeField] private float shakeAmt;
    [SerializeField] private GameObject spikes;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite damaged;
    [SerializeField] private Sprite stunned;

    private bool triggered;
    private bool attacking;
    private bool stun;
    private bool hurt;
    private bool dead;
    private float buttonPressCooldown;
    private float cooldownTimer;
    private SpriteRenderer sr;
    private Vector2 originalPos;
    private Vector2 dropPosition;
    private Vector2 attackPosition;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        transform.position = points[startingPoint].position;

        push.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        buttonPressCooldown -= Time.deltaTime;

        float shakeVar = 1f;


        if (hurt && !dead)
        {

            Vector2 pos = new Vector2(transform.position.x + (Mathf.Sin(Time.time * shakeAmt) * 0.05f), transform.position.y) ;

            transform.position = pos;
        }

        if (dead)
        {
            Vector2 pos = new Vector2(transform.position.x + (Mathf.Sin(Time.time * shakeAmt * shakeVar) * 0.05f), transform.position.y);
            shakeVar += 0.5f;  

            transform.position = pos;
        }

        PlayerCheck();
    }

    private void FixedUpdate()
    {
       if (!triggered && !attacking && !dead)
            {
                MoveLeftRight();
            }
        
    }

    private void MoveLeftRight()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.down, 99f, layerMask);
        
        if (hit)
        {
            if(cooldownTimer < 0 && !attacking && !dead)
            {
                triggered = true;
                Debug.Log(hit.point);
                attacking = true;
                dropPosition = transform.position;
                attackPosition = hit.point;
                Attack();
            }
        }
    }

    IEnumerator AttackBuffer(Vector2 attackLoc)
    {
        attacking = true;
        sr.sprite = angry;

        yield return new WaitForSeconds(bufferTime);

        while (Vector2.Distance(transform.position, attackLoc) > 0.5f && !stun)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackLoc, speed * 2 * Time.deltaTime);
            yield return null;
        }

        if (stun)
        {
            sr.sprite = stunned;
            yield return new WaitForSeconds(stunTime);
        }

        else
        {
            yield return new WaitForSeconds(0.2f);
        }

            while (Vector2.Distance(transform.position, dropPosition) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, dropPosition, speed * 2 * Time.deltaTime);
                yield return null;
            }

        yield return new WaitForSeconds(bufferTime);

        cooldownTimer = cooldownTime;
        stun = false;
        triggered = false;
        attacking = false;
        sr.sprite = normal;
    }
    private void Attack()
    {
            StartCoroutine(AttackBuffer(attackPosition));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SwordClass>())
        {
            stun = true;
        }


    }

    private void Damage()
    {
        StopAllCoroutines();

        if (Health > 0)
        {
            StartCoroutine(Hurt(dropPosition));
        }

        else if (Health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Hurt(Vector2 dropPosition)
    {

        yield return new WaitForSeconds(0.1f);
        originalPos = transform.position;

        sr.sprite = damaged;
        hurt = true;
        push.enabled = true;

        yield return new WaitForSeconds(1.5f);

        hurt = false;
        push.enabled = false;
        transform.position = originalPos;
        sr.sprite = normal;

        yield return new WaitForSeconds(0.1f);


        while (Vector2.Distance(transform.position, dropPosition) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dropPosition, speed * 2 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(bufferTime);

        cooldownTimer = cooldownTime;
        stun = false;
        triggered = false;
        attacking = false;
    }

    IEnumerator Death()
    {
        dead = true;
        sr.sprite = damaged;
        spikes.SetActive(false);

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
        
    }

    public void OnButtonPress()
    {
        if (buttonPressCooldown < 0 && !dead)
        {
            Health -= 1;
            Damage();
            buttonPressCooldown = 5f;
            
        }
    }
}
