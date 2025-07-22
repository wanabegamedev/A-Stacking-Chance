using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [Header("Game State Variables")]
    public int pickupCount = 1;
    
    public int round = 1;
    
    public int turn = 0;
    
    public int score = 0;
    
    public int scoreMultiplier = 1;
    
    public List<Block> selectedBlocks = new();


   public int scoreUntilNextRound = 100;


   private TowerGenerator generator;


   public List<Upgrade> currentActiveUpgrades = new();

   public List<Upgrade> availableUpgrades;

   private UIManager uiManager;

 
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

    public void AddToMultiplier(int amount)
    {

        scoreMultiplier += amount;
    }


    public void TurnEnd()
    {
        turn += 1;

        scoreMultiplier = 1;

        //reset the selected blocks
        selectedBlocks = new List<Block>();
        
        StartTurn();
        
    }

    void StartTurn()
    {
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
    {
        print("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndRound()
    {
        score = 0;
        scoreUntilNextRound = scoreUntilNextRound * 2;

        turn = 0;
        round += 1;

        
        //Load upgrade UI
        
        uiManager.DisplayUpgradeUI();
        

        //Reset tower
        generator.ResetTower();
        
        generator.GenerateTower(10 * round);
        
        
   
        
        
    }

    public List<Upgrade> SelectUpgrades()
    {
        List<Upgrade> selectedUpgrades = new();

        for (int i = 0; i < 2; i++)
        {
            var rand = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[rand]);
           


        }
        
        return selectedUpgrades;
    }
}

