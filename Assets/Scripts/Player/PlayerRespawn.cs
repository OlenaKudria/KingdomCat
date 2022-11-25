using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UiManager _uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        _uiManager = FindObjectOfType<UiManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            _uiManager.GameOVer();
            return;
        }
        
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
}
