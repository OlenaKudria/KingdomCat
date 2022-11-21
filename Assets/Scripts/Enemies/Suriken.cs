using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suriken : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private bool attacking;

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {

        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit2D.collider == null || attacking) continue;
            attacking = true;
            destination = directions[i];
            checkTimer = 0;
        }
    }

    void CalculateDirections()
    {
        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range; //left
        directions[2] = transform.up * range; //up
        directions[3] = -transform.up * range; //down
    }

    private void Stop()
    {
        destination = new Vector3(123,-7,0);
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
