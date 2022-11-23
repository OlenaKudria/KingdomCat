using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int _currenthealth;

    [SerializeField]private Behaviour[] _components;

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
        foreach (Behaviour component in _components)
            component.enabled = false;
        gameObject.SetActive(false);
    }
}
