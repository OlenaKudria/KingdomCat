using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private UiManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UiManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _uiManager.Restart();
        }
    }
}
