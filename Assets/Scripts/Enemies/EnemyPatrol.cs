using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
     [SerializeField]private Transform leftEdge;
     [SerializeField]private Transform rightEdge;
     [SerializeField]private float speed;
     private Vector3 initScale;
     [SerializeField]private Transform enemy;
     private bool _movingLeft;

     [SerializeField]private float idleDuration;
     private float _idleTimer;

     [SerializeField]private Animator _animator;

     private void OnDisable()
     {
          _animator.SetBool("isMoving", false);
     }

     private void Awake()
     {
          initScale = enemy.localScale;
     }

     private void Update()
     {
          if (_movingLeft)
          { 
               if(enemy.position.x >= leftEdge.position.x)
                    MoveInDirection(-1);
               else
               {
                    ChangeDirection();
               }
          }
          else
          {
               if(enemy.position.x <= rightEdge.position.x)
                    MoveInDirection(1);
               else
               {
                    ChangeDirection();
               }
          }
     }

     private void ChangeDirection()
     {
          _animator.SetBool("isMoving",false);
          _idleTimer += Time.deltaTime;
          
          if(_idleTimer > idleDuration)
                _movingLeft = !_movingLeft;
     }

     private void MoveInDirection(int direction)
     {
          _idleTimer = 0;
          _animator.SetBool("isMoving",true);
          enemy.localScale = new Vector3(Mathf.Abs(initScale.x)  * direction, initScale.y, initScale.z);
          
          enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y,
               enemy.position.z);
     }
     
}
