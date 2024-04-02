using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image peasantTimer;
    [SerializeField] private Image warriorTimer;
    [SerializeField] private ImageTimer enemyTimer;
    [SerializeField] private Button hirePeasantButton;
    [SerializeField] private Button hireWarriorButton;
    [SerializeField] private float peasantHireTime = 5f;
    [SerializeField] private float warriorHireTime = 10f;
    
    public float peasant;
    public float warrior;
    public float enemy;
    public float peasantHarvestPower;
    public float warriorMilletEat;
    public float peasantPrice;
    public float warriorPrice;
    private float currentTimepeasant;
    private float currentTimewarrior;
    private bool isHiring = false;
    private bool isHiringWarrior = false;
    public float millet;
    public ImageTimer harvestTimer;
    public foodTimer FoodTimer;
    public TMP_Text milletcount;
    public TMP_Text peasantcount;
    public TMP_Text warriorcount;
    public TMP_Text enemytcount;
    public TMP_Text gameStatus;
    public GameObject GameStatusPanel;
    public float enemyinvasioncycles = 3;
    private int cyclecounter=1;
    public float peasanttowin;
    public float millettowin;
    public AudioSource HarvestAudioSource;
    public AudioSource EnemyAudioSource;

    private void Start()
    {

       
        UpdateText();
        currentTimepeasant = peasantHireTime;
        currentTimewarrior = warriorHireTime;
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

            
            peasantTimer.fillAmount = Mathf.Clamp(currentTimepeasant / peasantHireTime, 0f, 1f);
        }

        if (isHiringWarrior)
        {
            currentTimewarrior -= Time.deltaTime;

            if (currentTimewarrior <= 0)
            {
                currentTimewarrior = warriorHireTime;
                isHiringWarrior = false;
                hirePeasantButton.interactable = true;
                warrior++;
                UpdateText();
            }

            
            warriorTimer.fillAmount = Mathf.Clamp(currentTimewarrior / warriorHireTime, 0f, 1f);
        }





        if (harvestTimer.Tick)
        {
            HarvestAudioSource.Play();
            millet += peasantHarvestPower*peasant;
            UpdateText();
        }

        if (enemyTimer.Tick)
        {
            cyclecounter++;

            if (cyclecounter > enemyinvasioncycles)
            {
               
                warrior -= enemy;
                enemy += 1;
                if(enemy>1) { EnemyAudioSource.Play(); }

                if (warrior < 0)
                {
                    
                    GameLose();
                }

                UpdateText();
            }
        }

        if (warrior > 0)
        {
            FoodTimer.StartEating();
        }
        else
        {
            FoodTimer.StopEating();
        }



        if (FoodTimer.Tick)
        {
            millet -= warriorMilletEat*warrior;
            UpdateText();
        }

        if ((millet >= millettowin) && (peasant >= peasanttowin))
        {
            GameWin();
        }

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        // Обновляем состояние кнопок на основе текущих условий
        hirePeasantButton.interactable = !isHiring && millet >= peasantPrice;
        hireWarriorButton.interactable = !isHiringWarrior && millet >= warriorPrice;
    }
    public void HirePeasant()
    {
        hirePeasantButton.interactable = false;
        isHiring = true;
        millet -= peasantPrice;
        UpdateText();
        UpdateButtonStates();
    }

    public void HireWarrior()
    {
        hireWarriorButton.interactable = false;
        isHiringWarrior = true;
        millet -= warriorPrice;
        UpdateText();
        UpdateButtonStates();
    }

    private void UpdateText()
    {
        milletcount.text = millet.ToString();
        peasantcount.text = peasant.ToString();
        warriorcount.text = warrior.ToString();
        enemytcount.text = enemy.ToString();
    }

    private void GameLose()
    {
        Time.timeScale = 0;
        GameStatusPanel.SetActive(true);
        gameStatus.text = "Ты проиграл!";

    }

    private void GameWin()
    {
        Time.timeScale = 0;
        GameStatusPanel.SetActive(true);
        gameStatus.text = "Ты Выиграл!";

    }
}