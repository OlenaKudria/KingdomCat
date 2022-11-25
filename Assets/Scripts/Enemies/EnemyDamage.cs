using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    public Animator animator;
    [SerializeField] private AudioClip trapSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("ActivateTrap");
            SoundManager.instance.PlaySound(trapSound);
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
