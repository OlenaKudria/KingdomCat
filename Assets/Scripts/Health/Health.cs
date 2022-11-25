using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator _animator;
    private bool _dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip playerHurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            _animator.SetTrigger("TakeDamage");
            SoundManager.instance.PlaySound(playerHurtSound);
            StartCoroutine(Invunerability());
        }
        else
        {
            if (_dead) return;
            _animator.SetTrigger("Die");
            SoundManager.instance.PlaySound(playerDeathSound);
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            StartCoroutine(Die());
            _dead = true;
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn()
    {
        _dead = false;
        AddHealth(startingHealth);
        _animator.ResetTrigger("Die");
        _animator.Play("Knight_Idle");
        StartCoroutine(Invunerability());
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;

    }

    private IEnumerator Die(){
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(2);
    }

    public IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(18, 17, true);
        for (int i = 0; i < numberOfFlashes; i++)
            yield return new WaitForSeconds(iFramesDuration/numberOfFlashes);
        
        Physics2D.IgnoreLayerCollision(18, 17, false);
    }
}
