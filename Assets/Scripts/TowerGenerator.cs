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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTower(towerHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTower(int height)
    {
    
        //height
        for (int h = 0; h < height; h++)
        {
            //width
            for (int w = 0; w < 3; w++)
            {
                var blockToInstantiate = SelectBlock();
                var block = Instantiate(blockToInstantiate);
                //if it is a rotated layer, rotate the block
                if (rotatedLayer)
                {

                    //for some reason requires manual offset by width
                    block.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    block.transform.localPosition = new Vector3(2, yOffset , xOffset - 2);
                }
                else
                {
                     block.transform.localPosition = new Vector3(xOffset, yOffset, 0);
                 
                }

                xOffset += 2;
                
            }

        
            //flips the rotated layer around
            rotatedLayer = !rotatedLayer;
            
            
            yOffset += blockHeight;
            xOffset = 0;



        }
  
        
    }


    Transform SelectBlock()
    {
        var randNumber = Random.Range(0, blockTypes.Count);

        var selectedBlock = blockTypes[randNumber];

        return selectedBlock.transform;
    }
}
