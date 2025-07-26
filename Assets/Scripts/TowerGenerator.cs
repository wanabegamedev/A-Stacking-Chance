using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerGenerator : MonoBehaviour
{
    [SerializeField] private int towerHeight = 10;
    
    public List<Block> blockTypes;

    public bool rotatedLayer = false;

    private GameObject currentPrefabBlock;
    
   private int yOffset = 0;
    private int xOffset = 0;

    [SerializeField] private int blockWidth = 2;
    [SerializeField] private int blockHeight = 1;
    

    public Dictionary<string, int> blockWeightsDictionary = new Dictionary<string, int>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadWeights();
        GenerateTower(towerHeight);
    }



    public void LoadWeights()
    {
        foreach (var block in blockTypes)
        {
            blockWeightsDictionary.Add(block.blockName, block.blockWeight);
        }
    }
    public void GenerateTower(int height)
    {
    
        //height
        for (int h = 0; h < height; h++)
        {
            //width
            for (int w = 0; w < 3; w++)
            {
                Transform blockToInstantiate = null;
                while (blockToInstantiate == null) 
                {
                     blockToInstantiate = SelectBlock();
                   
                }
                
                var block = Instantiate(blockToInstantiate);
            
                
                //if it is a rotated layer, rotate the block
                if (rotatedLayer)
                {

                    //for some reason requires manual offset by width
                    block.transform.rotation = Quaternion.Euler(0, 90, 0);
                    block.transform.position = new Vector3(2, yOffset , xOffset - 2);
                }
                else
                {
                     block.transform.position = new Vector3(xOffset, yOffset, 0);
                 
                }

                if (h == height - 1)
                {
                    block.GetComponent<Block>().blockLocked = true;
                }

                block.parent = transform;

                xOffset += 2;
                
            }

        
            //flips the rotated layer around
            rotatedLayer = !rotatedLayer;
            
            
            yOffset += blockHeight;
            xOffset = 0;



        }

        xOffset = 0;
        yOffset = 0;


    }


    Transform SelectBlock()
    {

        int total = 0;

        foreach (var block in blockWeightsDictionary)
        {
            total += block.Value;
        }
        
        var randWeight = Random.Range(1, total);

        print("random weight: " + randWeight);

        foreach (var block in blockTypes)
        {
            if (randWeight <= block.blockWeight) 
            {
                return block.transform;
            }
            else
            {
                randWeight -= block.blockWeight;
            }
            
                
        }

        return null;
    }


   public void ResetTower()
    {
        //Destroy tower
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    public void ChangeBlockWeight(string blockName, int amount)
    {
        blockWeightsDictionary[blockName] += amount;

        blockWeightsDictionary[blockName] = Mathf.Clamp(blockWeightsDictionary[blockName], 0, 100);

    }
}
