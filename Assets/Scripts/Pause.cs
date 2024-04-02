using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused;
    public GameObject PausePanel;
    public AudioSource LevelAudioSource;
    public void PauseGame()
    {
        if (paused)
        {
            LevelAudioSource.Play();
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            LevelAudioSource.Pause();
            PausePanel.SetActive(true);
            Time.timeScale = 0; 
        }

        paused=!paused;
    }

}
