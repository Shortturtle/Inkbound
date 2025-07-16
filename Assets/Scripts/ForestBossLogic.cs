using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class ForestBossLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;
    private int i;

    [SerializeField] private BoxCollider2D collision;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float bufferTime;
    [SerializeField] private float stunTime;
    [SerializeField] private float cooldownTime;

    private bool triggered;
    private bool attacking;
    private float cooldownTimer;
    private Vector2 dropPosition;
    private Vector2 attackPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        PlayerCheck();
    }

    private void FixedUpdate()
    {
        if (!triggered && !attacking)
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
            if(cooldownTimer < 0 && !attacking)
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

        yield return new WaitForSeconds(bufferTime);

        while (Vector2.Distance(transform.position, attackLoc) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackLoc, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(stunTime);

        while (Vector2.Distance(transform.position, dropPosition) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dropPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(bufferTime);

        cooldownTimer = cooldownTime;
        triggered = false;
        attacking = false;
    }

    IEnumerator AttackDown(Vector2 attackLoc)
    {

        transform.position = Vector2.Lerp(transform.position, attackLoc, speed * Time.deltaTime);

        yield return new WaitForSeconds(stunTime);

        StartCoroutine(AttackUp());
    }

    IEnumerator AttackUp()
    {
        transform.position = Vector2.Lerp(transform.position, dropPosition, speed * Time.deltaTime);

        yield return new WaitForSeconds(bufferTime);

        cooldownTimer = cooldownTime;
        triggered = false;
        attacking = false;
    }

    private void Attack()
    {
            StartCoroutine(AttackBuffer(attackPosition));
    }


}
