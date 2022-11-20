using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int _currenthealth;

    void Start()
    {
        _currenthealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currenthealth -= damage;
        
        animator.SetTrigger("Hurt");

        if (_currenthealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
    
    IEnumerator Die(){
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2); 
        Destroy(gameObject);
    }
    
}
