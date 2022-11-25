using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField]private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    public void GameOVer()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   
    public void MainMenu() => SceneManager.LoadScene(0);

    public void Quit() => Application.Quit();

}
