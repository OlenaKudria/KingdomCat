using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;
    [SerializeField]private AudioSource arrow3DSource;
    [SerializeField]private AudioClip arrowAttackSound3D;

    private void Awake() 
    {
        arrow3DSource = GetComponent<AudioSource>();
    }

    private void Attack()
    {
        cooldownTimer = 0;
        arrows[FindArrows()].transform.position = arrowPoint.position;
        arrows[FindArrows()].GetComponent<EnemyProjectile>().ActivateProjectile();
        arrow3DSource.PlayOneShot(arrowAttackSound3D);
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= attackCooldown)
            Attack();
    }
}
