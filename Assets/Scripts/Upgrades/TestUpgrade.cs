using UnityEngine;

public class TestUpgrade : Upgrade
{
  
    
    
    public override void TriggerUpgrade()
    {
        FindAnyObjectByType<GameManager>().pickupCount += 2;
    }
}
