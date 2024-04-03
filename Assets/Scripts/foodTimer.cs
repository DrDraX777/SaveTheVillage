using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class foodTimer : MonoBehaviour
{
    public float MaxTime;
    private Image img;
    private float currentTime;
    public bool Tick;
    public bool startEat=false;

    void Start()
    {
        img = GetComponent<Image>();
        currentTime = MaxTime;

    }
    void Update()
    {
        if(startEat) {
            Tick = false;
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                Tick = true;
                currentTime = MaxTime;
            }

            img.fillAmount = currentTime / MaxTime;
        }
        
    }

    public void StartEating()
    {
        startEat = true;
    }

    public void StopEating()
    {
        currentTime = MaxTime;
        startEat = false;
    }
}
