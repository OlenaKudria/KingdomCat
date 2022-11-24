using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int _currenthealth;

    [SerializeField]private Behaviour[] _components;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    void Start()
    {
        _currenthealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currenthealth -= damage;
        
        animator.SetTrigger("Hurt");
        SoundManager.instance.PlaySound(hurtSound);

        if (_currenthealth <= 0)
        {
            SoundManager.instance.PlaySound(deathSound);
            StartCoroutine(Die());
        }
    }
    
    IEnumerator Die(){
        animator.SetBool("isDead", true);
        foreach (Behaviour component in _components)
            component.enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
