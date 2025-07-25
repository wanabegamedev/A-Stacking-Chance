
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

   private GameManager manager;

   private PointBoundary boundary; 
   
   
   
   
   [SerializeField]  private TextMeshProUGUI score;
   [SerializeField]  private TextMeshProUGUI progressBar;
   [SerializeField] private TextMeshProUGUI multiplier;
   
   [SerializeField] private TextMeshProUGUI timer;


   [Header("Upgrade System")] 
   [SerializeField] private UpgradeHolder selection1;
   [SerializeField] private UpgradeHolder selection2;
   [SerializeField] private UpgradeHolder selection3;


   [Header("Game Over UI")] [SerializeField]
   private TextMeshProUGUI highscoreText;
   

   [Header("Canvas")]
   [SerializeField] private GameObject upgradeCanvas;
   [SerializeField] private GameObject gameUICanvas;
   [SerializeField] private GameObject endUICanvas;

   
   
   
    void Start()
    {

        manager = FindAnyObjectByType<GameManager>();
        boundary = FindAnyObjectByType<PointBoundary>();


        upgradeCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        score.text = boundary.gracePeriodScoreHolder.ToString();
        progressBar.text = manager.score + "/" + manager.scoreUntilNextRound;

        if (boundary.gracePeriodActive)
        {
            timer.gameObject.SetActive(true);
            multiplier.gameObject.SetActive(true);

            timer.text = boundary.timePassed.ToString();
            
            multiplier.text = manager.scoreMultiplier +"X";
        }
        else
        {
            timer.gameObject.SetActive(false);
            multiplier.gameObject.SetActive(false);
            
            
        }

    }

    public void DisplayUpgradeUI()
    {
        manager.inUpgradePhase = true;
        gameUICanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        //uses the root element to make sure no upgrade UI is displayed
        
        
        List<Upgrade> selectedUpgrades = manager.SelectUpgrades();

        var increment = 0;

        foreach (var upgrade in selectedUpgrades)
        {

            switch (increment)
            {
                case 0:
                    selection1.SetupHolder(upgrade);
                    break;
                case 1:
                    selection2.SetupHolder(upgrade);
                    break;
                case 2:
                    selection3.SetupHolder(upgrade);
                    break;
            }

            increment++;

        }
        
        
        
    }

    public void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
        gameUICanvas.SetActive(true);
    }

    public void ShowEndUI()
    {
        endUICanvas.SetActive(true);
        
        upgradeCanvas.SetActive(false);
        gameUICanvas.SetActive(false);

        highscoreText.text = "High Score: " + manager.score;
    }
}
