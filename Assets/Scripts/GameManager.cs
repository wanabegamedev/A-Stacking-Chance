using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    [Header("Game State Variables")]
    public int pickupCount = 1;
    
    public int round = 1;
    
    public int turn = 0;
    
    public int score = 0;

    public int highScore = 0;
    
    public int scoreMultiplier = 1;
    
    public List<Block> selectedBlocks = new();


   public int scoreUntilNextRound = 100;


   private TowerGenerator generator;


   public List<Upgrade> currentActiveUpgrades = new();

   public List<Upgrade> availableUpgrades;

   private UIManager uiManager;

   public bool inUpgradePhase;

   
   

   [Header("Audio Clips")] 
  
   [SerializeField] private AudioClip loseClip;

   [SerializeField] private AudioClip roundWinClip;
   
   [SerializeField] private AudioClip piecePullOutClp;
   
   [SerializeField] private AudioClip roundStartClip;


 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generator = FindAnyObjectByType<TowerGenerator>();
        uiManager = FindAnyObjectByType<UIManager>();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= scoreUntilNextRound)
        {
            EndRound();
        }
    }

    public bool SelectBlock(Block selectedBlock)
    {
        //Check in block is already in list and deselect it
        if (selectedBlocks.Contains(selectedBlock))
        {
            print("Block already selected, deselecting block");
            selectedBlocks.Remove(selectedBlock);
            return false;
           
        }
        
        //If outside limit then reject
        if (selectedBlocks.Count > pickupCount - 1)
        {
            print("Too Many Blocks Selected");
            return false;
        }
        
            selectedBlocks.Add(selectedBlock);
        
            
            print("Block Added");
            return true;
            
    }

  public void DeselectAllBlocks()
    {
        foreach (var block in selectedBlocks)
        {
            block.selected = false;
        }
    }

    public void DeselectChosenBlock(Block b)
    {
        try
        {
            selectedBlocks.Remove(b);

        }
        catch (Exception e)
        {
            Debug.Log("Error: Can't Remove Block!");
        }
       
    }

    public void AddToMultiplier(int amount)
    {

        scoreMultiplier += amount;
    }


    public void TurnEnd()
    {
        turn += 1;

        scoreMultiplier = 1;

        //reset the selected blocks
        DeselectAllBlocks();
        selectedBlocks = new List<Block>();
        
        
        for (int i = 0; i < generator.transform.childCount; i++)
        {
            generator.transform.GetChild(i).GetComponent<Block>().ActivateOnTurnEndModifiers();
        }
        
        //Add a delay before a new turn starts
        
        Invoke(nameof(StartTurn), 0.7f);
  
        
    }

    void StartTurn()
    {
        AudioManager.instance.PlaySound(roundStartClip);
        
        for (int i = 0; i < generator.transform.childCount; i++)
        {
           generator.transform.GetChild(i).GetComponent<Block>().ActivateTurnStartModifiers();
        }
    }


    public void AddToScore(int amount)
    {
        score += amount * scoreMultiplier;
    }



    public void EndGame()
    { AudioManager.instance.PlaySound(loseClip);
        print("Game Over");
        uiManager.ShowEndUI();
        Time.timeScale = 0;
        
    }

    public void EndRound()
    {
        highScore += score;
        score = 0;
        scoreUntilNextRound = scoreUntilNextRound * 2;
        
        AudioManager.instance.PlaySound(roundWinClip);
       
        DeselectAllBlocks();
        selectedBlocks = new();

        turn = 0;
        round += 1;

        StartCoroutine(UpgradeLoop());
        
    }
    
    IEnumerator UpgradeLoop()
    {
        Time.timeScale = 0;
        uiManager.DisplayUpgradeUI();

        while (inUpgradePhase)
        {
            yield return null;
        }
        
        //after upgrade phase generate new tower
        
        
        generator.ResetTower();
        
        generator.GenerateTower(5 * round);
        
        uiManager.HideUpgradeUI();
        Time.timeScale = 1;
        
        yield return null;

       
    }

    public List<Upgrade> SelectUpgrades()
    {
        List<Upgrade> selectedUpgrades = new();

        for (int i = 0; i < 3; i++)
        {
            var rand = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[rand]);
           


        }
        
        return selectedUpgrades;
    }


    public void SetupUpgrade(Upgrade selectedUpgrade)
    {
        if (!selectedUpgrade.upgradePermanent)
        {
            selectedUpgrade.TriggerUpgrade();
            return;
            
        }
        
        //TODOD: Add upgrade to list
        selectedUpgrade.TriggerUpgrade();
    }


 
}

