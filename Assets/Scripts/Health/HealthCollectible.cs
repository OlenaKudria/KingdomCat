using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip healthCollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        collision.GetComponent<Health>().AddHealth(healthValue);
        SoundManager.instance.PlaySound(healthCollectSound);
        Destroy(gameObject);
    }
}
