using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   public Animator animator;
   public PlayerMovement playerMovement;
   public Transform attackPoint;
   private float _attackRange = 0.95f;
   public LayerMask enemyLayer;

   public int attackDamage;

   private float _attackRate = 2f;
   private float _nextAttackTime = 0f;

   private void Start()
   {
      animator = GetComponent<Animator>();
      playerMovement = GetComponent<PlayerMovement>();
   }
   
   private void Update()
   {
      if (Time.time >= _nextAttackTime)
      {
          if (Input.GetMouseButtonDown(0) && !playerMovement.isOnWall())
          {
             Attack();
             _nextAttackTime = Time.time + 1f / _attackRate;
          }
         
      }
   }

   void Attack()
   {
      //Play attack anim
      animator.SetTrigger("Attack");
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayer);
      //Damage enemies
      foreach (Collider2D enemy in hitEnemies)
      {
         enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
      }
   }
   
   private void OnDrawGizmosSelected()
   {
      if(attackPoint == null)
         return;
      Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
   }
}
