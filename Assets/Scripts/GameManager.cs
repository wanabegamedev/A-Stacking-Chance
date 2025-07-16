using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [Header("Game State Variables")]
    public int pickupCount = 1;
    
    public int round = 0;
    
    public int turn = 0;
    
    public int score = 0;
    
    public int scoreMultiplier = 1;
    
    public List<Block> selectedBlocks = new();


    [SerializeField] private int scoreUntilNextRound = 100;



 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }


    public void AddToScore(int amount)
    {
        score += amount * scoreMultiplier;
    }
}

