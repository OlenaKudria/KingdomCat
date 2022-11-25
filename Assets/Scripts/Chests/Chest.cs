using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioClip chestOpenSound;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        _animator.SetTrigger("OpenChest");
        SoundManager.instance.PlaySound(chestOpenSound);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
