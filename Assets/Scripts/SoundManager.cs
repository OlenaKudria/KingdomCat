using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource _source;

    private void Awake()
    {
        
        _source = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance !=this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip audioClip)
    {
        _source.PlayOneShot(audioClip);
    }
}
