using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   [SerializeField]private float attackCooldown;
   [SerializeField]private float range;
   [SerializeField]private float colliderDistance;
   [SerializeField]private float damage;
   [SerializeField]private BoxCollider2D boxCollider2D;
   [SerializeField]private LayerMask playerLayer;
   private float _cooldownTimer = Mathf.Infinity;
   
   private Animator _animator;
   private Health _playerHealth;
   private EnemyPatrol _enemyPatrol;
   [SerializeField] private AudioClip enemyAttackSound;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _enemyPatrol = GetComponentInParent<EnemyPatrol>();
   }

   private void Update()
   {
      _cooldownTimer += Time.deltaTime;
      if (PlayerInSight())
      {
         if (_cooldownTimer >= attackCooldown)
         {
            _cooldownTimer = 0;
            SoundManager.instance.PlaySound(enemyAttackSound);
            _animator.SetTrigger("Attack");
         }
      }

      if (_enemyPatrol != null)
         _enemyPatrol.enabled =  !PlayerInSight();

   }

   private bool PlayerInSight()
   {
      RaycastHit2D hit2D = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
         new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
         0,Vector2.left,0, playerLayer);
      if (hit2D.collider != null)
         _playerHealth = hit2D.transform.GetComponent<Health>();
      
      return hit2D.collider != null;
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
         new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
   }

   private void DamagePlayer()
   {
      if (PlayerInSight())
      {
         _playerHealth.TakeDamage(damage);
      }
   }
}
