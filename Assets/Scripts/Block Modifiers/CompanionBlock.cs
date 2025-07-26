using UnityEngine;

public class CompanionBlock : BlockModifier
{
    [SerializeField] private AudioClip multiplyClip;
    [SerializeField] private AudioClip divideClip;
    
    public override void ActivateModifier()
    {
        GameManager manager = FindAnyObjectByType<GameManager>();

        if (manager.selectedBlocks.Count % 2 == 0)
        {
            AudioManager.instance.PlaySound(multiplyClip);
     
            print("high");
            manager.score *= 2;
            
        }
        else
        {
            AudioManager.instance.PlaySound(divideClip);
            print("low");
            manager.score = Mathf.FloorToInt(manager.score / 2);
            
        }
        
    }
}
