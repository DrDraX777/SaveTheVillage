using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image peasantTimer;
    [SerializeField] private Image warriorTimer;
    [SerializeField] private Button hirePeasantButton;
    [SerializeField] private Button hireWarriorButton;
    [SerializeField] private float peasantHireTime = 5f;
    [SerializeField] private float warriorHireTime = 10f;
    public float peasant;
    public float warrior;
    public float peasantHarvestPower;
    public float peasantPrice;
    public float warriorPrice;
    private float currentTimepeasant;
    private float currentTimewarrior;
    private bool isHiring = false;
    private bool isHiringWarrior = false;
    public float millet;
    public ImageTimer harvestTimer;
    public TMP_Text milletcount;
    public TMP_Text peasantcount;
    public TMP_Text warriortcount;

    private void Start()
    {
        UpdateText();
        currentTimepeasant = peasantHireTime;
    }

    private void Update()
    {
        if (isHiring)
        {
            currentTimepeasant -= Time.deltaTime;

            if (currentTimepeasant <= 0)
            {
                currentTimepeasant = peasantHireTime;
                isHiring = false;
                hirePeasantButton.interactable = true;
                
                peasant++;
                UpdateText();
            }

            // Используем Mathf.Clamp для обеспечения безопасности присвоения
            peasantTimer.fillAmount = Mathf.Clamp(currentTimepeasant / peasantHireTime, 0f, 1f);
        }

        if (isHiringWarrior)
        {
            currentTimewarrior -= Time.deltaTime;

            if (currentTimewarrior <= 0)
            {
                currentTimewarrior = warriorHireTime;
                isHiring = false;
                hirePeasantButton.interactable = true;
                warrior++;
                UpdateText();
            }

            // Используем Mathf.Clamp для обеспечения безопасности присвоения
            warriorTimer.fillAmount = Mathf.Clamp(currentTimewarrior / warriorHireTime, 0f, 1f);
        }





        if (harvestTimer.Tick)
        {
            millet += peasantHarvestPower*peasant;
            UpdateText();
        }

        if (peasantPrice > millet)
        {
            hirePeasantButton.interactable = false;
        }
        else if(!isHiring)
        {
            hirePeasantButton.interactable = true;
        }

        if (warriorPrice > millet)
        {
            hireWarriorButton.interactable = false;
        }
        else if (!isHiringWarrior)
        {
            hireWarriorButton.interactable = true;
        }
    }

    // Метод для начала "найма"
    public void HirePeasant()
    {
        hirePeasantButton.interactable = false;
        isHiring = true;
        millet -= peasantPrice;
        UpdateText();
    }

    public void HireWarrior()
    {
        hireWarriorButton.interactable = false;
        isHiringWarrior = true;
        millet -= warriorPrice;
        UpdateText();
    }

    private void UpdateText()
    {
        milletcount.text = millet.ToString();
        peasantcount.text = peasant.ToString();
    }
}