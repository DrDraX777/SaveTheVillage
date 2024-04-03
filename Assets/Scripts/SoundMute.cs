using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMute : MonoBehaviour
{
    public Button button; 
    public Sprite sprite1; 
    public Sprite sprite2; 
    private bool isSprite1 = true; 
    public AudioSource LevelAudioSource;

    void Start()
    {
        
    }

   public void ToggleSprite()
    {
        if(LevelAudioSource.isPlaying)
            LevelAudioSource.Pause();
        else LevelAudioSource.Play();
        
        if (isSprite1)
        {
            
            button.image.sprite = sprite2;
        }
        else
        {
            
            button.image.sprite = sprite1;
        }
        isSprite1 = !isSprite1;
    }
}
