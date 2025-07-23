using System;
using UnityEngine;
using TMPro;

public class UpgradeHolder : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    public Upgrade heldUpgrade;
    private GameManager manager;

    private void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

    public void SetupHolder(Upgrade selectedUpgrade)
    {
        heldUpgrade = selectedUpgrade;

        title.text = heldUpgrade.upgradeName;
        description.text = heldUpgrade.upgradeDesc;
        
        
    }

    public void AcceptUpgrade()
    {
        manager.SetupUpgrade(heldUpgrade);
        manager.inUpgradePhase = false;
    }
    
}
