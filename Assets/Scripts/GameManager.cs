using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public Image peasantTimer;
    public Image warriorTimer;
    public ImageTimer enemyTimer;
    public Button hirePeasantButton;
    public Button hireWarriorButton;
    public float peasantHireTime = 5f;
    public float warriorHireTime = 10f;
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
    public TMP_Text gameStatusResult;
    public GameObject GameStatusPanel;
    public float enemyinvasioncycles = 3;
    private int cyclecounter=1;
    public float peasanttowin;
    public float millettowin;
    public AudioSource HarvestAudioSource;
    public AudioSource EnemyAudioSource;
    public AudioSource EatAudioSource;
    public AudioSource PeasantHireSource;
    public AudioSource WarriorHireSource;
    private float allmillet;
    private float allenemies;

    private void Start()
    {
        Time.timeScale = 1;
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
                PeasantHireSource.Play();
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

                WarriorHireSource.Play();
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
            allmillet += peasantHarvestPower * peasant;
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
                allenemies += enemy;
                if (enemy>1) { EnemyAudioSource.Play(); }

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
            EatAudioSource.Play();
            millet -= warriorMilletEat*warrior;
            UpdateText();
        }

        if ((millet >= millettowin) && (peasant >= peasanttowin))
        {
            GameWin();
        }

        if (millet < 0)
        {
            GameLoseMillet();
        }

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
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
        gameStatusResult.text = "Количество пережитых волн:" + cyclecounter  
                                                             + "\n" + "Всего собрано зерна:"+ allmillet 
                                                             + "\n" + "Всего убито врагов:"+ allenemies;
    }

    private void GameWin()
    {
        Time.timeScale = 0;
        GameStatusPanel.SetActive(true);
        gameStatus.text = "Ты Выиграл!";
        gameStatusResult.text = "Количество пережитых волн:" + cyclecounter 
                                                             + "\n" + "Всего собрано зерна:" + allmillet
                                                             + "\n" + "Всего убито врагов:" + allenemies;

    }

    private void GameLoseMillet()
    {
        Time.timeScale = 0;
        GameStatusPanel.SetActive(true);
        gameStatus.text = "Ты проиграл, воины сьели всю еду!";
        gameStatusResult.text = "Количество пережитых волн:" + cyclecounter 
                                                             + "\n" + "Всего собрано зерна:" + allmillet
                                                             + "\n" + "Всего убито врагов:" + allenemies;
    }
}