using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public bool upgradePermanent = false;

    public string upgradeName = "Upgrade 1";
    
    public string upgradeDesc = "Upgrade 1";


    public abstract void TriggerUpgrade();

}
