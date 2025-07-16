using System;
using System.Collections;
using UnityEngine;

public class PointBoundary : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private float gracePeriodTime = 5;

    private bool gracePeriodActive = false;

    private int blocksOut = 0;

    private int gracePeriodScoreHolder = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        
        if (!gracePeriodActive && other.TryGetComponent<Block>(out Block firstBlock))
        {
            blocksOut += 1;
            StartCoroutine(StartPieceRemovalGracePeriod());
            
            //TRIGGER ON REMOVE BLOCK EVENTS (With any local modifiers adjusting the individual block's score)
            
            gracePeriodScoreHolder += firstBlock.blockScoreValue;


            manager.selectedBlocks.Remove(firstBlock);
            Destroy(firstBlock.gameObject);
           
        }
        else if (other.TryGetComponent<Block>(out Block otherBlock))
        {
            blocksOut += 1;
           
            //TRIGGER ON REMOVE BLOCK EVENTS (With any local modifiers adjusting the individual block's score)

            gracePeriodScoreHolder += otherBlock.blockScoreValue;
            
            manager.selectedBlocks.Remove(otherBlock);
            Destroy(otherBlock.gameObject);
        }
      
    }


    IEnumerator StartPieceRemovalGracePeriod()
    {
        gracePeriodActive = true;
        float timePassed = 0f;

        var lastNumberOfBlocksOut = blocksOut;
        
        //add initial blocks out to the multiplier
        manager.AddToMultiplier(blocksOut);
       
        while (timePassed <= gracePeriodTime)
        {
            //if blocks out is higher than a new block must have left
            if (blocksOut > lastNumberOfBlocksOut)
            {
                manager.AddToMultiplier(1);
                
                //reset check
                lastNumberOfBlocksOut = blocksOut;
            }
            
            
            timePassed += Time.deltaTime;
            print(timePassed);
            
            

            yield return null;
            
          
        }


        gracePeriodActive = false;
        blocksOut = 0;
        manager.AddToScore(gracePeriodScoreHolder);

        gracePeriodScoreHolder = 0;
        
        manager.TurnEnd();
        yield return null;
    }
}
