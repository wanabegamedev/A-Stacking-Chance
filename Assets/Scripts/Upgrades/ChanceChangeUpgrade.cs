using UnityEngine;

public class ChanceChangeUpgrade : Upgrade
{
    [SerializeField] private int amount;
    [SerializeField] private Block block;
    
    private TowerGenerator towerGen;
    
    public override void TriggerUpgrade()
    {
        towerGen = FindAnyObjectByType<TowerGenerator>();

         print("original : " + towerGen.blockWeightsDictionary[block.blockName]);

        towerGen.ChangeBlockWeight(block.blockName, amount);
        
        print("new : " +towerGen.blockWeightsDictionary[block.blockName]);
    }
}
