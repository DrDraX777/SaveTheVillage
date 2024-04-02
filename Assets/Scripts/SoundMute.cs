using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMute : MonoBehaviour
{
    public Button button; // Ссылка на вашу кнопку
    public Sprite sprite1; // Первый спрайт
    public Sprite sprite2; // Второй спрайт

    private bool isSprite1 = true; // Флаг, указывающий, какой спрайт сейчас активен
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
            // Если сейчас активен sprite1, меняем на sprite2
            button.image.sprite = sprite2;
        }
        else
        {
            // Если сейчас активен sprite2, меняем на sprite1
            button.image.sprite = sprite1;
        }
        isSprite1 = !isSprite1; // Переключаем флаг
    }
}
