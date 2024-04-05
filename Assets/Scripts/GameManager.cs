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
        HandlePeasantHiringTimer();

        HandleWarriorHiringTimer();

        HandleHarvest();

        HandleEnemyAttacks();

        FoodTimerCheck();
        
        HandleFoodTimer();

        WinLoseCheck();

        UpdateButtonStates();
    }


    private void HandlePeasantHiringTimer()
    {
        if (!isHiring) return;

        currentTimepeasant -= Time.deltaTime;
        if (currentTimepeasant <= 0)
        {
            CompletePeasantHiring();
        }
        peasantTimer.fillAmount = Mathf.Clamp(currentTimepeasant / peasantHireTime, 0f, 1f);
    }
    private void CompletePeasantHiring()
    {
        PeasantHireSource.Play();
        currentTimepeasant = peasantHireTime;
        isHiring = false;
        hirePeasantButton.interactable = true;
        peasant++;
        UpdateText();
    }

    private void HandleWarriorHiringTimer()
    {
        if (!isHiringWarrior) return;

        currentTimewarrior -= Time.deltaTime;
        if (currentTimewarrior <= 0)
        {
            CompleteWarriorHiring();
        }
        warriorTimer.fillAmount = Mathf.Clamp(currentTimewarrior / warriorHireTime, 0f, 1f);
    }
    private void CompleteWarriorHiring()
    {
        WarriorHireSource.Play();
        currentTimewarrior = warriorHireTime;
        isHiringWarrior = false;
        hireWarriorButton.interactable = true;
        warrior++;
        UpdateText();
    }

    private void HandleHarvest()
    {
        if (!harvestTimer.Tick) return;

        allmillet += peasantHarvestPower * peasant;
        HarvestAudioSource.Play();
        millet += peasantHarvestPower * peasant;
        UpdateText();
    }

    private void HandleEnemyAttacks()
    {
        if (!enemyTimer.Tick) return;

        cyclecounter++;
        if (cyclecounter > enemyinvasioncycles)
        {
            ProcessEnemyAttack();
        }
    }

    private void ProcessEnemyAttack()
    {
        warrior -= enemy;
        enemy += 1;
        allenemies += enemy;
        if (enemy > 1) { EnemyAudioSource.Play(); }

        if (warrior < 0)
        {
            GameLose();
        }

        UpdateText();
    }

    private void FoodTimerCheck()
    {
        if (warrior > 0)
        {
            FoodTimer.StartEating();
        }
        else
        {
            FoodTimer.StopEating();
        }
    }

    private void HandleFoodTimer()
    {
        if (!FoodTimer.Tick) return;

        EatAudioSource.Play();
        millet -= warriorMilletEat * warrior;
        UpdateText();
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

    private void WinLoseCheck()
    {
        if ((millet >= millettowin) && (peasant >= peasanttowin))
        {
            GameWin();
        }

        if (millet < 0)
        {
            GameLoseMillet();
        }
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